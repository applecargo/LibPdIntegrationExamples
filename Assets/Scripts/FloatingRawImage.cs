using UnityEngine;
using UnityEngine.UI;

public class FloatingRawImage : MonoBehaviour
{
    public RawImage rawImage1;  // 첫 번째 RawImage
    public RawImage rawImage2;  // 두 번째 RawImage

    public float speed1 = 1.0f; // 첫 번째 이미지의 이동 속도
    public float amplitude1 = 50.0f; // 첫 번째 이미지의 진폭

    public float speed2 = 1.5f; // 두 번째 이미지의 이동 속도
    public float amplitude2 = 30.0f; // 두 번째 이미지의 진폭

    private RectTransform rectTransform1; // 첫 번째 RawImage의 RectTransform
    private RectTransform rectTransform2; // 두 번째 RawImage의 RectTransform

    private Vector2 initialPosition1;  // 첫 번째 이미지의 초기 위치
    private Vector2 initialPosition2;  // 두 번째 이미지의 초기 위치

    void Start()
    {
        // 첫 번째 RawImage의 RectTransform 가져오기 및 초기 위치 저장
        rectTransform1 = rawImage1.GetComponent<RectTransform>();
        initialPosition1 = rectTransform1.anchoredPosition;

        // 두 번째 RawImage의 RectTransform 가져오기 및 초기 위치 저장
        rectTransform2 = rawImage2.GetComponent<RectTransform>();
        initialPosition2 = rectTransform2.anchoredPosition;
    }

    void Update()
    {
        // 첫 번째 RawImage의 움직임 (Sin, Cos 기반)
        float x1 = Mathf.Sin(Time.time * speed1) * amplitude1;
        float y1 = Mathf.Cos(Time.time * speed1 * 1.5f) * amplitude1;

        // 두 번째 RawImage의 움직임 (다른 속도와 진폭 적용)
        float x2 = Mathf.Sin(Time.time * speed2) * amplitude2;
        float y2 = Mathf.Cos(Time.time * speed2 * 1.5f) * amplitude2;

        // 새로운 위치 계산 및 적용 (첫 번째 이미지)
        Vector2 newPosition1 = new Vector2(initialPosition1.x + x1, initialPosition1.y + y1);
        rectTransform1.anchoredPosition = newPosition1;

        // 새로운 위치 계산 및 적용 (두 번째 이미지)
        Vector2 newPosition2 = new Vector2(initialPosition2.x + x2, initialPosition2.y + y2);
        rectTransform2.anchoredPosition = newPosition2;
    }
}
