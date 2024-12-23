using UnityEngine;
using UnityEngine.UI;

public class FCP_SceneScript : MonoBehaviour
{
    public Toggle landToggle; // Land 오브젝트 색상 변경 토글
    public Toggle cameraToggle; // 카메라 색상 변경 토글
    public GameObject landObject; // Land 오브젝트
    public Camera mainCamera; // 현재 사용하는 카메라

    private Material landMaterial;
    private Color lastLandColor = Color.white; // Land 오브젝트의 마지막으로 설정된 색상
    private Color lastCameraColor = Color.black; // 카메라 Clear Flag의 마지막 색상
    private FlexibleColorPicker fcp; // FlexibleColorPicker 참조

    private void Start()
    {
        // FlexibleColorPicker 찾기
        fcp = GameObject.FindWithTag("FCP").GetComponent<FlexibleColorPicker>();

        // Land 오브젝트의 Material 가져오기
        if (landObject != null)
        {
            landMaterial = landObject.GetComponent<Renderer>().material;
            // Land 초기 색상 설정 (흰색)
            landMaterial.color = Color.white;
            lastLandColor = Color.white;
        }

        // 카메라 초기 색상 설정 (검은색)
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = Color.black;
            lastCameraColor = Color.black;
        }

        // Land Toggle 이벤트 연결
        if (landToggle != null)
        {
            landToggle.onValueChanged.AddListener(OnLandToggleValueChanged);
        }

        // Camera Toggle 이벤트 연결
        if (cameraToggle != null)
        {
            cameraToggle.onValueChanged.AddListener(OnCameraToggleValueChanged);
        }
    }

    private void OnLandToggleValueChanged(bool isOn)
    {
        if (landMaterial != null)
        {
            if (isOn)
            {
                // FlexibleColorPicker의 색상 변경 이벤트 등록
                fcp.onColorChange.AddListener(UpdateLandColor);
                // 현재 FlexibleColorPicker 색상을 Land 오브젝트에 바로 적용
                landMaterial.color = fcp.color;
            }
            else
            {
                // FlexibleColorPicker의 색상 변경 이벤트 해제
                fcp.onColorChange.RemoveListener(UpdateLandColor);
                // 마지막으로 설정된 색상 유지
                lastLandColor = landMaterial.color;
            }
        }
    }

    private void UpdateLandColor(Color newColor)
    {
        // FlexibleColorPicker의 색상에 따라 Land 오브젝트 색상 변경
        if (landMaterial != null)
        {
            landMaterial.color = newColor;
            lastLandColor = newColor; // 마지막 색상 업데이트
        }
    }

    private void OnCameraToggleValueChanged(bool isOn)
    {
        if (mainCamera != null)
        {
            if (isOn)
            {
                // FlexibleColorPicker의 색상 변경 이벤트 등록
                fcp.onColorChange.AddListener(UpdateCameraColor);
                // 현재 FlexibleColorPicker 색상을 카메라에 바로 적용
                mainCamera.backgroundColor = fcp.color;
            }
            else
            {
                // FlexibleColorPicker의 색상 변경 이벤트 해제
                fcp.onColorChange.RemoveListener(UpdateCameraColor);
                // 마지막으로 설정된 색상 유지
                lastCameraColor = mainCamera.backgroundColor;
            }
        }
    }

    private void UpdateCameraColor(Color newColor)
    {
        // FlexibleColorPicker의 색상에 따라 카메라 Clear Flag 색상 변경
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = newColor;
            lastCameraColor = newColor; // 마지막 색상 업데이트
        }
    }
}
