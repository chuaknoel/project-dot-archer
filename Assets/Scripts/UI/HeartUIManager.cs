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
        Debug.Log($"��Ʈ UI ������Ʈ:");

        int heartCount = Mathf.CeilToInt(maxHealth / 20f);

        // ��Ʈ ���� ���߱�
        while (hearts.Count < heartCount)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            Image img = newHeart.GetComponent<Image>();
            hearts.Add(img);
        }

        // ���� ��Ʈ ��Ȱ��ȭ
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < heartCount)
                hearts[i].gameObject.SetActive(true);
            else
                hearts[i].gameObject.SetActive(false);
        }

        // �� ��Ʈ ���� ����
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
