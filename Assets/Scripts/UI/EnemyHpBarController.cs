using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHpBarController : MonoBehaviour
{
    Canvas canvas; // �ν����Ϳ� �Ҵ��ϰų� Find�� ��������
    GameObject target;
    GameObject hpBar;
    RectTransform rt;

    public string targetTag; // Ÿ���� tag
    public float x  = 77.6f; // HP�� ������
    public float y = 32.2f; 

    // Start is called before the first frame update
    void OnEnable()
    {
        canvas = FindObjectOfType<Canvas>();
        target = GameObject.FindGameObjectWithTag(targetTag);
      
        //   ������Ʈ ����
        GameObject hpBarPrefab = Resources.Load<GameObject>("UI/BossHpBar");
        hpBar = Instantiate(hpBarPrefab);

        // �θ� Canvas�� ����
        hpBar.transform.SetParent(canvas.transform, false);

         rt = hpBar.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        CreateImage();
    }
    void CreateImage()
    {
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Camera.main.WorldToScreenPoint(target.transform.position),
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out uiPos
        );

        rt.anchoredPosition = uiPos + new Vector2(0, 55f); // ���� 50 �ȼ� ����
        rt.sizeDelta = new Vector2(x, y);    // ũ��
    }
}
