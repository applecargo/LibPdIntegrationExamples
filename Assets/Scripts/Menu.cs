using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject popup; // 팝업창 GameObject (Inspector에서 설정)
    private bool isPopupActive = false; // 팝업창 활성화 상태 추적

    private void Start()
    {
        popup.SetActive(false); // 처음에는 팝업창 비활성화
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TogglePopup(); 
        }
    }

    // Info 버튼에 연결
    public void ShowPopup()
    {
        if (popup != null)
        {
            popup.SetActive(true);
            isPopupActive = true;
        }
    }

    // Close 버튼에 연결
    public void ClosePopup()
    {
        if (popup != null)
        {
            popup.SetActive(false);
            isPopupActive = false;
        }
    }

    // 팝업창 상태를 토글
    private void TogglePopup()
    {
        if (popup != null)
        {
            isPopupActive = !isPopupActive;
            popup.SetActive(isPopupActive);
        }
    }
}
