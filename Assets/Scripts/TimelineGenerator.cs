using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UI.UILineRenderer; 


public class TimelineGenerator : MonoBehaviour
{
    private List<RawImage> rawImages = new List<RawImage>();  // RawImage 리스트
    private List<float> speeds = new List<float>();  // 각 RawImage의 속도 리스트
    private List<float> amplitudes = new List<float>();  // 각 RawImage의 진폭 리스트
    private List<Vector2> initialPositions = new List<Vector2>();  // 각 RawImage의 초기 위치 리스트

    // private LineRenderer lineRenderer;  // LineRenderer 컴포넌트
    public LineRenderer lineRenderer;
    public Canvas canvas; // UI가 있는 Canvas   

    // RawImage가 생성될 위치에 있는 게임오브젝트들 (씬에 배치되어 있어야 함)
    public List<RectTransform> spawnLocations = new List<RectTransform>(9);

    private int currentSpawnIndex = 0;


    void Start()
    {
        // LineRenderer 초기 설정
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        // LineRenderer 컴포넌트를 이 오브젝트에 추가
        // lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 1.5f;  // 선의 시작 두께
        lineRenderer.endWidth = 1.5f;  // 선의 끝 두께
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));  // 기본 머티리얼 설정
        lineRenderer.startColor = Color.red;  // 선 색상
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 0;

        // Sorting Layer와 Order 설정
        lineRenderer.sortingLayerName = "UI";  // 원하는 Sorting Layer로 변경 가능
        lineRenderer.sortingOrder = 10;            // RawImage 위로 보이도록 설정
        
    }

    void Update()
    {
        for (int i = 0; i < rawImages.Count; i++)
        {
            // 각 RawImage의 움직임 계산 (Sin, Cos 기반)
            float x = Mathf.Sin(Time.time * speeds[i]) * amplitudes[i];
            float y = Mathf.Cos(Time.time * speeds[i] * 1.5f) * amplitudes[i];

            // 새로운 위치 계산 및 적용
            Vector2 newPosition = new Vector2(initialPositions[i].x + x, initialPositions[i].y + y);
            rawImages[i].GetComponent<RectTransform>().anchoredPosition = newPosition;
        }

        // LineRenderer의 포인트 갱신
        UpdateLineRenderer();
    }

    // 특정 트리거로 RawImage를 추가하는 메서드
    public void AddRawImage(GameObject newGameObject)
    {

        Debug.Log("stair!!");
        Debug.Log(currentSpawnIndex);
        
        // 새롭게 추가된 GameObject의 RectTransform을 가져옴
        RectTransform rectTransform = spawnLocations[currentSpawnIndex].GetComponent<RectTransform>();

        // 프리팹을 해당 위치에 생성
        GameObject rawImageInstance = Instantiate(newGameObject, spawnLocations[currentSpawnIndex].transform);

        // RectTransform에서 초기 위치를 가져옴
        Vector2 initialPosition = rawImageInstance.GetComponent<RectTransform>().anchoredPosition;

        // 속도와 진폭 값을 임의로 설정 (원하는 대로 조정 가능)
        float speed = Random.Range(1.0f, 2.0f);
        float amplitude = Random.Range(20.0f, 50.0f);

        // 리스트에 각각 추가
        rawImages.Add(rawImageInstance.GetComponent<RawImage>());
        speeds.Add(speed);
        amplitudes.Add(amplitude);
        initialPositions.Add(initialPosition);

        // LineRenderer에 새로운 포인트 추가
        // lineRenderer.positionCount = rawImages.Count; 
        
        // 다음 위치에 배치하기 위한 인덱스를 증가
        currentSpawnIndex++;


        // LineRenderer 갱신
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        if (lineRenderer == null || rawImages.Count == 0 || canvas == null) return;

        // LineRenderer의 점 개수를 RawImage 개수로 설정
        lineRenderer.positionCount = rawImages.Count;

        for (int i = 0; i < rawImages.Count; i++)
        {
            // RectTransform의 Screen Space 좌표를 직접 사용
            Vector3 screenPosition = rawImages[i].rectTransform.position;

            // LineRenderer에 UI 좌표 그대로 설정
            screenPosition.z = 0.0f; // LineRenderer가 UI와 동일한 Z축에 위치하도록 조정
            lineRenderer.SetPosition(i, screenPosition);
        }
    }

    //     // LineRenderer 갱신 메서드
    // private void UpdateLineRenderer()
    // {
    //     // LineRenderer의 포인트 수가 RawImage 수와 일치하는지 확인
    //     if (lineRenderer.positionCount != rawImages.Count)
    //     {
    //         lineRenderer.positionCount = rawImages.Count;  // LineRenderer 포인트 수를 업데이트
    //     }

    //     for (int i = 0; i < rawImages.Count; i++)
    //     {
    //         // 각 RawImage의 현재 위치를 World Position으로 변환하여 LineRenderer에 설정
    //         RectTransform rectTransform = rawImages[i].GetComponent<RectTransform>();

    //         // UI의 ScreenPoint를 World Position으로 변환
    //         Vector3 worldPosition;
    //         RectTransformUtility.ScreenPointToWorldPointInRectangle(
    //             rectTransform, rectTransform.anchoredPosition, Camera.main, out worldPosition);

    //         lineRenderer.SetPosition(i, worldPosition);  // LineRenderer에 World Position 설정
    //     }
    // }
}
