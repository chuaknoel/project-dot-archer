using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public GameObject[] storyImages;
    public GameObject[] storyTexts;
    public float typingSpeed = 0.15f;

    public GameObject tapToContinue; // �ν����Ϳ��� ����

    private int index = 0;
    private string fullText = "";
    private UnityEngine.UI.Text currentTextComponent;
    private bool isTyping = false;
    private bool textFullyDisplayed = false;

    void Start()
    {
        ShowCurrentSlide();
        tapToContinue.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // Ÿ���� �� Ŭ�� �� ��ü ���
                StopAllCoroutines();
                currentTextComponent.text = fullText;
                isTyping = false;
                textFullyDisplayed = true;
            }
            else if (textFullyDisplayed)
            {
                index++;
                if (index >= storyImages.Length)
                {
                    // �ƾ� ���� �� CreditsScene���� �̵�
                    SceneManager.LoadScene("CreditsScene");
                }
                else
                {
                    ShowCurrentSlide();
                }
            }
        }
    }

    void ShowCurrentSlide()
    {
        // ��� �̹��� & �ؽ�Ʈ ��Ȱ��ȭ
        foreach (var img in storyImages) img.SetActive(false);
        foreach (var txt in storyTexts) txt.SetActive(false);

        // ���� �����̵� Ȱ��ȭ
        storyImages[index].SetActive(true);
        storyTexts[index].SetActive(true);

        // Ÿ���� ȿ�� �غ�
        currentTextComponent = storyTexts[index].GetComponentInChildren<UnityEngine.UI.Text>();
        fullText = currentTextComponent.text;
        currentTextComponent.text = "";

        tapToContinue.SetActive(false);
        StartCoroutine(TypeText(fullText));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        textFullyDisplayed = false;

        foreach (char c in text)
        {
            currentTextComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        textFullyDisplayed = true;
        tapToContinue.SetActive(true);
    }
}
