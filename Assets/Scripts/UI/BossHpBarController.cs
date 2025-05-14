using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpBarController : HpBarController
{
    // Update is called once per frame
    protected override void Update()
    {
       
    }

    // HpBar ����
    protected override void CreateHpBar()
    {
        GameObject hpBarPrefab = Resources.Load<GameObject>("UI/BossHpBar");
        hpBar = Instantiate(hpBarPrefab);
        hpSlider = hpBar.GetComponent<UnityEngine.UI.Slider>();

        // �θ� Canvas�� ����
        hpBar.transform.SetParent(canvas.transform, false);

        // ��ġ ����
        rt = hpBar.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, -96f); 
    }
}
