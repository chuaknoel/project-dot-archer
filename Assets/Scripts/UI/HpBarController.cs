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

    public float heightOffset = 1.5f;      // �� ���� ���� ����
    public float x  = 77.6f; // HP�� ������
    public float y = 32.2f; 

    // Start is called before the first frame update
    void OnEnable()
    {
        canvas = FindObjectOfType<Canvas>();
      
        // HpBar ����
        CreateHpBar();
    }

    // Update is called once per frame
    void Update()
    { 
        // Ÿ���� ����
        FollowTarget();
    }
    // HpBar ����
    void CreateHpBar()
    {
        GameObject hpBarPrefab = Resources.Load<GameObject>("UI/BossHpBar");
        hpBar = Instantiate(hpBarPrefab);
        hpSlider = hpBar.GetComponent<UnityEngine.UI.Slider>();

        // �θ� Canvas�� ����
        hpBar.transform.SetParent(canvas.transform, false);

        rt = hpBar.GetComponent<RectTransform>();
    }
    // Ÿ���� ����
    void FollowTarget()
    {
        // ���� ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ
        Vector3 worldPos = transform.position + Vector3.up * heightOffset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        rt.position = screenPos;

        rt.sizeDelta = new Vector2(x, y);    // HpBar ũ��
    }
    // ü�� ����
    public void UpdateHP(float current, float max)
    {
        if (hpBar != null)
            hpSlider.value = current / max;
    }
}
