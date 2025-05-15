using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public Transform heartContainer;

    private List<Image> hearts = new();

    public void UpdateHearts(float currentHealth, float maxHealth)
    {
        Debug.Log($"하트 UI 업데이트:");

        int heartCount = Mathf.CeilToInt(maxHealth / 20f);

        // 하트 개수 맞추기
        while (hearts.Count < heartCount)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            Image img = newHeart.GetComponent<Image>();
            hearts.Add(img);
        }

        // 남는 하트 비활성화
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < heartCount)
                hearts[i].gameObject.SetActive(true);
            else
                hearts[i].gameObject.SetActive(false);
        }

        // 각 하트 상태 갱신
        for (int i = 0; i < heartCount; i++)
        {
            float heartHealth = currentHealth - (i * 20);

            if (heartHealth >= 20)
                hearts[i].sprite = fullHeart;
            else if (heartHealth >= 10)
                hearts[i].sprite = halfHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}
