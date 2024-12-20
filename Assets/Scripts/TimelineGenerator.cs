using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimelineGenerator : MonoBehaviour
{
    public Image image1;  // 첫 번째 이미지
    public Image image2;  // 두 번째 이미지
    public LineRenderer lineRenderer;  // LineRenderer 컴포넌트
    public Canvas canvas;  // 캔버스

    void Start()
    {
        // LineRenderer 초기 설정
        lineRenderer.positionCount = 2;  // 두 점을 그릴 것이므로 2로 설정
        lineRenderer.useWorldSpace = true;  // 월드 좌표계 사용
    }

    void Update()
    {
        // 각 이미지의 RectTransform 가져오기
        RectTransform rectTransform1 = image1.GetComponent<RectTransform>();
        RectTransform rectTransform2 = image2.GetComponent<RectTransform>();

        // 이미지들의 화면 좌표를 얻기 (UI 좌표 -> ScreenPoint로 변환)
        Vector3 screenPos1 = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rectTransform1.position);
        Vector3 screenPos2 = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rectTransform2.position);

        // 화면 좌표를 다시 월드 좌표로 변환 (z축에 캔버스의 planeDistance를 설정)
        Vector3 worldPos1 = canvas.worldCamera.ScreenToWorldPoint(new Vector3(screenPos1.x, screenPos1.y, canvas.planeDistance));
        Vector3 worldPos2 = canvas.worldCamera.ScreenToWorldPoint(new Vector3(screenPos2.x, screenPos2.y, canvas.planeDistance));

        // LineRenderer의 두 지점을 설정
        lineRenderer.SetPosition(0, worldPos1);  // 첫 번째 이미지 위치를 선의 시작점으로 설정
        lineRenderer.SetPosition(1, worldPos2);  // 두 번째 이미지 위치를 선의 끝점으로 설정
    }
}
