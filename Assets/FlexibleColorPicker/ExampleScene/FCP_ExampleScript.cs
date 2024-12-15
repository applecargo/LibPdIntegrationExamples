using UnityEngine;

public class FCP_ExampleScript : MonoBehaviour
{
    public Material material;

    private Color defaultColor; // 초기 색상 저장
    private Color highlightColor = new Color(0, 0, 0, 0.5f); // 반투명 검정색 (50% 투명도)
    private static FCP_ExampleScript currentlySelected; // 현재 선택된 오브젝트
    private FlexibleColorPicker fcp; // FlexibleColorPicker 참조
    private float fluidAlpha = 0.72f; // Fluid 태그의 투명도

    private void Start()
    {
        // FlexibleColorPicker 찾기
        fcp = GameObject.FindWithTag("FCP").GetComponent<FlexibleColorPicker>();

        // 초기 색상 설정
        if (gameObject.CompareTag("Fluid"))
        {
            // Fluid 태그는 항상 투명도 72% 유지
            defaultColor = material.GetColor("_Color"); // Fluid의 초기 색상
            SetFluidColor(defaultColor);
        }
        else
        {
            // 일반 오브젝트는 불투명 흰색으로 초기화
            defaultColor = Color.white;
            SetOpaqueMaterial();
            material.color = defaultColor;
        }

        // FlexibleColorPicker 초기화
        if (currentlySelected == this)
        {
            fcp.color = defaultColor;
        }

        // FlexibleColorPicker의 색상 변경 이벤트 등록
        fcp.onColorChange.AddListener(OnChangeColor);
    }

    private void Update()
    {
        // 마우스 클릭 처리
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 트리거도 감지하도록 설정
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Collide))
            {
                // 선택된 오브젝트가 현재 스크립트를 가진 오브젝트인지 확인
                if (hit.collider.gameObject == gameObject)
                {
                    SelectObject();
                }
            }
        }
    }

    private void SelectObject()
    {
        // 현재 선택된 오브젝트 상태 복구
        if (currentlySelected != null && currentlySelected != this)
        {
            currentlySelected.Deselect();
        }

        // 현재 오브젝트를 선택
        currentlySelected = this;

        // FlexibleColorPicker 색상 동기화
        fcp.color = defaultColor;

        // 하이라이트 색상 적용
        if (gameObject.CompareTag("Fluid"))
        {
            SetFluidColor(highlightColor); // Fluid 태그의 경우 tint 컬러 변경
        }
        else
        {
            SetTransparentMaterial();
            material.color = highlightColor; // 일반 오브젝트의 하이라이트 적용
        }
    }

    private void Deselect()
    {
        // 선택 해제 시 기본 상태 복구
        if (gameObject.CompareTag("Fluid"))
        {
            SetFluidColor(defaultColor); // Fluid 태그의 tint 컬러 복구
        }
        else
        {
            SetOpaqueMaterial();
            material.color = defaultColor; // 일반 오브젝트의 색상 복구
        }
    }

    private void OnChangeColor(Color co)
    {
        // FlexibleColorPicker에서 선택된 색상을 적용
        if (currentlySelected == this)
        {
            if (gameObject.CompareTag("Fluid"))
            {
                SetFluidColor(co);
                defaultColor = co; // 기본 색상 업데이트
            }
            else
            {
                SetOpaqueMaterial(); // 불투명 모드로 전환
                material.color = co;
                defaultColor = co; // 기본 색상 업데이트
            }
        }
    }

    private void SetFluidColor(Color baseColor)
    {
        // Fluid 태그의 컬러와 투명도를 설정
        Color fluidColor = baseColor;
        fluidColor.a = fluidAlpha; // 투명도 72% 유지
        material.SetColor("_Color", fluidColor);
    }

    private void SetTransparentMaterial()
    {
        // Material을 투명 모드로 설정
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000; // Transparent Queue
    }

    private void SetOpaqueMaterial()
    {
        // Material을 불투명 모드로 설정
        material.SetOverrideTag("RenderType", "Opaque");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 2000; // Opaque Queue
    }
}
