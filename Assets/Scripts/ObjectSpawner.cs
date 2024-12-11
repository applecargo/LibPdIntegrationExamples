using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab; // 생성할 오브젝트 프리팹
    public float spawnDistance = 100.0f; // 생성 거리

    public void SpawnObject()
    {
        Camera mainCamera = Camera.main;

        Transform cameraTransform = mainCamera.transform;

        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * spawnDistance;

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
