using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScroller : MonoBehaviour
{
    public RectTransform creditContent;

    [Header("��ũ�� �ð� ���� (��)")]
    public float scrollDuration = 40f; // �� ��ũ�� �ð�

    public float endY = 700f;

    private bool transitioned = false;
    private float startY;
    private float elapsedTime = 0f;

    void Start()
    {
        startY = creditContent.anchoredPosition.y;
    }

    void Update()
    {
        if (transitioned) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / scrollDuration);
        float newY = Mathf.Lerp(startY, endY, t);
        creditContent.anchoredPosition = new Vector2(creditContent.anchoredPosition.x, newY);

        if (t >= 1f)
        {
            transitioned = true;
            SceneManager.LoadScene("StartScene");
        }
    }
}