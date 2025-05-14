using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HpBarController : MonoBehaviour
{
    Canvas canvas;
    [HideInInspector] public GameObject hpBar;
    [HideInInspector] private UnityEngine.UI.Slider hpSlider;
    RectTransform rt;

    public float heightOffset = 1.5f;      // 적 위에 띄우는 높이
    public float x  = 77.6f; // HP바 사이즈
    public float y = 32.2f; 

    // Start is called before the first frame update
    void OnEnable()
    {
        canvas = FindObjectOfType<Canvas>();
      
        // HpBar 생성
        CreateHpBar();
    }

    // Update is called once per frame
    void Update()
    { 
        // 타겟을 따라감
        FollowTarget();
    }
    // HpBar 생성
    void CreateHpBar()
    {
        GameObject hpBarPrefab = Resources.Load<GameObject>("UI/BossHpBar");
        hpBar = Instantiate(hpBarPrefab);
        hpSlider = hpBar.GetComponent<UnityEngine.UI.Slider>();

        // 부모를 Canvas로 설정
        hpBar.transform.SetParent(canvas.transform, false);

        rt = hpBar.GetComponent<RectTransform>();
    }
    // 타겟을 따라감
    void FollowTarget()
    {
        // 적의 월드 좌표를 화면 좌표로 변환
        Vector3 worldPos = transform.position + Vector3.up * heightOffset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        rt.position = screenPos;

        rt.sizeDelta = new Vector2(x, y);    // HpBar 크기
    }
    // 체력 감소
    public void UpdateHP(float current, float max)
    {
        if (hpBar != null)
            hpSlider.value = current / max;
    }
}
