using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHpBarController : MonoBehaviour
{
    Canvas canvas; // 인스펙터에 할당하거나 Find로 가져오기
    GameObject target;
    GameObject hpBar;
    RectTransform rt;

    public string targetTag; // 타겟의 tag
    public float x  = 77.6f; // HP바 사이즈
    public float y = 32.2f; 

    // Start is called before the first frame update
    void OnEnable()
    {
        canvas = FindObjectOfType<Canvas>();
        target = GameObject.FindGameObjectWithTag(targetTag);
      
        //   오브젝트 생성
        GameObject hpBarPrefab = Resources.Load<GameObject>("UI/BossHpBar");
        hpBar = Instantiate(hpBarPrefab);

        // 부모를 Canvas로 설정
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

        rt.anchoredPosition = uiPos + new Vector2(0, 55f); // 위로 50 픽셀 띄우기
        rt.sizeDelta = new Vector2(x, y);    // 크기
    }
}
