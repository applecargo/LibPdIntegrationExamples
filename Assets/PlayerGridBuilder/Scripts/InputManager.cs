using UnityEngine;

namespace PlayerGridBuilder
{
    /// <summary>
    /// A sample mapper of controls to available BuildSystem actions
    /// 
    /// Right Click toggles whether the system is enabled
    /// 
    /// Left Click builds or deletes depending on the current mode
    /// 
    /// Z and C rotate the build ghost
    /// 
    /// Q and E cycle through selected materials
    /// 
    /// Delete toggles deletion mode
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public BuildSystem buildSystem;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                buildSystem.ToggleBuildUI();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                buildSystem.PreviousBuildMaterial();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                buildSystem.NextBuildMaterial();
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                buildSystem.RotateCCW();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                buildSystem.RotateCW();
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                buildSystem.ToggleDeletionMode();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                buildSystem.BuildCurrentGhost();
            }
        }
    }
}