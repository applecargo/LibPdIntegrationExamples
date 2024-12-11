using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnDistance = 20.0f;
    public float minSpacing = 5.0f; // 생성 간 최소 거리

    private List<int> remainingIndices = new List<int>();
    private List<Vector3> recentSpawnPositions = new List<Vector3>(); // 최근 생성 위치들
    public int maxRecentPositions = 5; // 겹치지 않도록 추적할 위치 개수

    private void Start()
    {
        InitializeIndices();
    }

    public void SpawnObject()
    {
        // 남은 인덱스가 없으면 초기화
        if (remainingIndices.Count == 0)
        {
            InitializeIndices();
        }

        // 무작위 인덱스 선택
        int randomIndex = remainingIndices[Random.Range(0, remainingIndices.Count)];
        GameObject prefab = prefabs[randomIndex];

        // 인덱스 제거
        remainingIndices.Remove(randomIndex);

        // 메인 카메라 가져오기
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
            return;
        }

        Transform cameraTransform = mainCamera.transform;

        // 생성 위치 계산
        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * spawnDistance;
        spawnPosition.y = 18.0f;

        // 겹치지 않도록 위치 확인
        spawnPosition = AdjustForSpacing(spawnPosition);

        // 오브젝트 생성
        Instantiate(prefab, spawnPosition, Quaternion.identity);

        // 생성된 위치 기록
        recentSpawnPositions.Add(spawnPosition);
        if (recentSpawnPositions.Count > maxRecentPositions)
        {
            recentSpawnPositions.RemoveAt(0); // 오래된 위치 제거
        }
    }

    private void InitializeIndices()
    {
        remainingIndices.Clear();
        for (int i = 0; i < prefabs.Length; i++)
        {
            remainingIndices.Add(i);
        }
    }

    private Vector3 AdjustForSpacing(Vector3 spawnPosition)
    {
        foreach (Vector3 recentPosition in recentSpawnPositions)
        {
            if (Vector3.Distance(spawnPosition, recentPosition) < minSpacing)
            {
                // 위치를 조금 이동하여 겹침 방지
                spawnPosition += new Vector3(Random.Range(-minSpacing, minSpacing), 0, Random.Range(-minSpacing, minSpacing));
            }
        }
        return spawnPosition;
    }
}
