using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public GameObject[] storyImages;
    public GameObject[] storyTexts;
    public float typingSpeed = 0.15f;

    public GameObject tapToContinue; // 인스펙터에서 연결

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
                // 타이핑 중 클릭 → 전체 출력
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
                    // 컷씬 종료 → CreditsScene으로 이동
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
        // 모든 이미지 & 텍스트 비활성화
        foreach (var img in storyImages) img.SetActive(false);
        foreach (var txt in storyTexts) txt.SetActive(false);

        // 현재 슬라이드 활성화
        storyImages[index].SetActive(true);
        storyTexts[index].SetActive(true);

        // 타이핑 효과 준비
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
