using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerGridBuilder
{
    /// <summary>
    /// This is the main class of PlayerGridBuilder.
    /// 
    /// It handles determining where the player is pointing, drawing ghosts,
    /// instantiating build materials, and deleting them.
    /// </summary>
    public class BuildSystem : MonoBehaviour
    {

        [Header("Display")]

        /// <summary>
        /// The camera used to determine the location the player is pointing
        /// </summary>
        public Camera playerCamera;

        [Header("UI")]

        /// <summary>
        /// The list of available objects to be built
        /// </summary>
        public List<Transform> buildingObjects;
        /// <summary>
        /// The color given to ghosts while placing them, before building
        /// </summary>
        public Color buildGhostColor = Color.blue;
        /// <summary>
        /// The color given to the highlight on an object selected for deletion
        /// </summary>
        public Color deleteGhostColor = Color.red;

        [Header("Events")]

        /// <summary>
        /// An event called when an object is built
        /// </summary>
        public UnityEvent<Transform> onBuild;
        /// <summary>
        /// An event called when an object is destroyed
        /// </summary>
        public UnityEvent<Transform> onDestroy;

        private RaycastHit? pointerHit;
        private int rotationOffset = 0;
        private Transform floorBuild;
        private Transform floorPrefab;
        private int materialIndex = 0;
        /// <summary>
        /// The current status of deletion mode.
        /// </summary>
        public bool deletionMode
        {
            get; private set;
        } = false;
        private bool active = true;

        private Transform GetTransformParentSafe(RaycastHit hit)
        {
            if (hit.transform == null)
            {
                return hit.transform;
            }
            return hit.transform.parent != null ? hit.transform.parent : hit.transform;
        }

        private Vector3 GetVoxel(Vector3 point)
        {
            float epsilon = 0.0001f;

            return new Vector3(
                Mathf.FloorToInt(point.x + epsilon),
                Mathf.FloorToInt(point.y + epsilon),
                Mathf.FloorToInt(point.z + epsilon)
            );
        }

        void Awake()
        {
            SelectBuildMaterial();
            ToggleBuildUI();
        }

        /// <summary>
        /// Toggle the BuildSystem on or off, toggling any ghosts currently displayed as well
        /// </summary>
        public void ToggleBuildUI()
        {
            active = !active;
            if (floorBuild != null)
            {
                floorBuild.gameObject.SetActive(!floorBuild.gameObject.activeInHierarchy);
            }
        }
        private void SelectBuildMaterial()
        {
            OnBuildingMaterialSelected(buildingObjects[materialIndex]);
        }

        /// <summary>
        /// Select the next build material in the build materials list
        /// </summary>
        public void NextBuildMaterial()
        {
            materialIndex = (materialIndex + 1) % buildingObjects.Count;
            SelectBuildMaterial();
        }

        /// <summary>
        /// Select the previous build material in the build materials list
        /// </summary>
        public void PreviousBuildMaterial()
        {
            materialIndex = (materialIndex - 1) % buildingObjects.Count;
            if (materialIndex < 0)
            {
                materialIndex += buildingObjects.Count;
            }
            SelectBuildMaterial();
        }
        /// <summary>
        /// Rotate the current build ghost counter-clockwise.
        /// </summary>
        public void RotateCCW()
        {
            rotationOffset = (rotationOffset + 90) % 360;
        }

        /// <summary>
        /// Rotate the current build ghost clockwise.
        /// </summary>
        public void RotateCW()
        {
            rotationOffset = (rotationOffset + 90) % 360;
        }

        /// <summary>
        /// Toggle deletion mode. When deletion mode is enabled, previously built items will be highlighted
        /// and deleted when BuildCurrentGhost is called.
        /// </summary>
        public void ToggleDeletionMode()
        {
            this.deletionMode = !this.deletionMode;
        }

        private void UpdatePointerHit(RaycastHit pointerHit)
        {
            if (floorBuild == null)
            {
                return;
            }

            Transform hitTransform = GetTransformParentSafe(pointerHit);

            if (hitTransform == null)
            {
                return;
            }

            Vector3 point = pointerHit.point;
            Vector3 localPoint = transform.InverseTransformPoint(point);

            Vector3 voxel = GetVoxel(localPoint);

            Vector3 localNormal = transform.InverseTransformDirection(pointerHit.normal);

            Vector3 voxelToPlayer = GetVoxel(localPoint + (localNormal / 2));
            Vector3 midpointThree = (new Vector3(0.5f, 0, 0.5f)) + voxelToPlayer;
            Vector3 worldMidpointThree = transform.TransformPoint(midpointThree);

            Buildable buildable = hitTransform.GetComponent<Buildable>();
            Buildable floorBuildable = floorBuild.GetComponent<Buildable>();

            // if floor hit
            if (pointerHit.transform == transform)
            {
                // calculate closest edge midpoint
                Vector3 north = (new Vector3(0.5f, 0, 1));
                Vector3 east = (new Vector3(1, 0, 0.5f));
                Vector3 south = (new Vector3(0.5f, 0, 0));
                Vector3 west = (new Vector3(0, 0, 0.5f));

                Vector3 midpoint = (new Vector3(0.5f, 0, 0.5f)) + voxel;
                Vector3 worldMidpoint = transform.TransformPoint(midpoint);

                float northDistance = Vector3.Distance(localPoint, voxel + north);
                float eastDistance = Vector3.Distance(localPoint, voxel + east);
                float southDistance = Vector3.Distance(localPoint, voxel + south);
                float westDistance = Vector3.Distance(localPoint, voxel + west);

                float minDistance = Mathf.Min(northDistance, eastDistance, southDistance, westDistance);

                floorBuild.localPosition = voxel;
                floorBuild.localRotation = Quaternion.identity;

                // rotate object to snap to nearest edge orientation
                if (minDistance == northDistance)
                {
                    floorBuild.transform.RotateAround(worldMidpoint, hitTransform.up, 180);
                }
                else if (minDistance == eastDistance)
                {
                    floorBuild.transform.RotateAround(worldMidpoint, hitTransform.up, 270);
                }
                else if (minDistance == southDistance)
                {
                    // south is default orientation
                }
                else if (minDistance == westDistance)
                {
                    floorBuild.transform.RotateAround(worldMidpoint, hitTransform.up, 90);
                }
            }
            else if (buildable != null && buildable.buildableType == BuildableType.WALL)
            {
                // if wall hit and wall type 
                // calculate closest edge midpoint
                Vector3 north = (transform.InverseTransformDirection(hitTransform.up));
                Vector3 east = (transform.InverseTransformDirection(hitTransform.right));
                Vector3 south = (transform.InverseTransformDirection(hitTransform.up)) * -1;
                Vector3 west = (transform.InverseTransformDirection(hitTransform.right)) * -1;

                Vector3 midpoint = hitTransform.localPosition + (north + east) / 2;

                float northDistance = Vector3.Distance(localPoint, midpoint + (north / 2));
                float eastDistance = Vector3.Distance(localPoint, midpoint + (east / 2));
                float southDistance = Vector3.Distance(localPoint, midpoint + (south / 2));
                float westDistance = Vector3.Distance(localPoint, midpoint + (west / 2));

                float minDistance = Mathf.Min(northDistance, eastDistance, southDistance, westDistance);

                floorBuild.position = hitTransform.position;
                floorBuild.rotation = hitTransform.rotation;

                if (floorBuildable.buildableType == BuildableType.WALL)
                {
                    // for wall-on-wall, extend the wall in whichever direction is closest to hit
                    if (minDistance == northDistance)
                    {
                        floorBuild.transform.localPosition += north;
                    }
                    else if (minDistance == eastDistance)
                    {
                        floorBuild.transform.localPosition += east;
                    }
                    else if (minDistance == southDistance)
                    {
                        floorBuild.transform.localPosition += south;
                    }
                    else if (minDistance == westDistance)
                    {
                        floorBuild.transform.localPosition += west;
                    }

                    Vector3 worldMidpoint = transform.TransformPoint(floorBuild.transform.localPosition + (north + east) / 2);
                    floorBuild.transform.RotateAround(worldMidpoint, hitTransform.up, rotationOffset * 2);
                }
                else if (floorBuildable.buildableType == BuildableType.CUBE)
                {
                    // for cube-on-wall, build in the voxel on the normal to the player
                    // respecting selected rotation
                    floorBuild.transform.localPosition = voxelToPlayer;
                    floorBuild.rotation = transform.rotation;
                    floorBuild.transform.RotateAround(worldMidpointThree, hitTransform.up, rotationOffset);
                }
            }
            else if (buildable != null && buildable.buildableType == BuildableType.CUBE && floorBuildable.buildableType == BuildableType.CUBE)
            {
                // for cube-on-cube, build in the voxel on the normal to the player
                // respecting selected rotation
                floorBuild.transform.localPosition = voxelToPlayer;
                floorBuild.rotation = transform.rotation;
                floorBuild.transform.RotateAround(worldMidpointThree, hitTransform.up, rotationOffset);
            }
        }

        private void OnBuildingMaterialSelected(Transform buildingMaterial)
        {
            if (floorBuild != null)
            {
                Destroy(floorBuild.gameObject);
            }

            floorPrefab = buildingMaterial;
            floorBuild = Instantiate(buildingMaterial, transform);

            floorBuild.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

            foreach (Renderer renderer in floorBuild.GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = buildGhostColor;
            }

            foreach (Collider collider in floorBuild.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }

            foreach (Transform trans in floorBuild.gameObject.GetComponentsInChildren<Transform>())
            {
                trans.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }

        }

        /// <summary>
        /// Either builds the current ghost in place, or deletes the currently highlighted object.
        /// This depends on the current deletion mode.
        /// </summary>
        public void BuildCurrentGhost()
        {
            if (active && pointerHit != null)
            {
                if (deletionMode)
                {
                    Transform hitTransform = GetTransformParentSafe((RaycastHit)pointerHit);
                    if (hitTransform == null)
                    {
                        return;
                    }

                    Buildable buildable = hitTransform.GetComponent<Buildable>();

                    if (buildable != null)
                    {
                        onDestroy?.Invoke(buildable.transform);
                        Destroy(buildable.gameObject);
                        this.pointerHit = null;
                    }
                }
                else
                {
                    Transform built = Instantiate(floorPrefab, floorBuild.position, floorBuild.rotation, transform);
                    onBuild?.Invoke(built);
                }
            }
        }

        /// <summary>
        /// This methods draws the delete ghost for one frame. To customize the visual behavior of deletion, override this method.
        /// </summary>
        protected void DrawDeleteGhost(Transform transform)
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();

            block.SetColor("_Color", deleteGhostColor);

            foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
            {
                Graphics.DrawMesh(
                    renderer.transform.GetComponent<MeshFilter>().mesh,
                    renderer.transform.localToWorldMatrix,
                    renderer.material,
                    0,
                    null,
                    0,
                    block
                );
            }
        }

        private void UpdateRaycast()
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform != null)
                {
                    pointerHit = hit;
                }
            }
        }

        void Update()
        {
            UpdateRaycast();

            if (floorBuild == null || floorPrefab == null || this.pointerHit == null)
            {
                return;
            }

            RaycastHit pointerHit = (RaycastHit)this.pointerHit;
            UpdatePointerHit(pointerHit);

            if (deletionMode && active)
            {
                floorBuild.gameObject.SetActive(false);

                Transform hitTransform = GetTransformParentSafe(pointerHit);
                if (hitTransform == null)
                {
                    return;
                }

                Buildable buildable = hitTransform.GetComponent<Buildable>();

                if (buildable != null)
                {
                    DrawDeleteGhost(hitTransform);
                }
            }
            else
            {
                floorBuild.gameObject.SetActive(true && active);
            }
        }
    }
}