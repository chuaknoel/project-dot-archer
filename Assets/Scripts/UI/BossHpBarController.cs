using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpBarController : HpBarController
{
    // Update is called once per frame
    protected override void Update()
    {
       
    }

    // HpBar 생성
    protected override void CreateHpBar()
    {
        GameObject hpBarPrefab = Resources.Load<GameObject>("UI/BossHpBar");
        hpBar = Instantiate(hpBarPrefab);
        hpSlider = hpBar.GetComponent<UnityEngine.UI.Slider>();

        // 부모를 Canvas로 설정
        hpBar.transform.SetParent(canvas.transform, false);

        // 위치 설정
        rt = hpBar.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, -96f); 
    }
}
