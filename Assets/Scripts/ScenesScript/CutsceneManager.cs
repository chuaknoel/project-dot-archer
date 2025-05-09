using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("테스트용 컷씬 강제 실행")]
    public bool forcePlayCutscene = false;

    public GameObject[] storyImages;
    public GameObject[] storyTexts;
    public float typingSpeed = 0.1f;

    public GameObject tapToContinue; // 인스펙터에서 연결

    private int index = 0;
    private string fullText = "";
    private UnityEngine.UI.Text currentTextComponent;
    private bool isTyping = false;
    private bool textFullyDisplayed = false;

    void Start()
    {
        if (!forcePlayCutscene && PlayerPrefs.GetInt("HasPlayedCutscene", 0) == 1)
        {
            SceneManager.LoadScene("SampleScene");
            return;
        }

        ShowCurrentSlide();

        tapToContinue.SetActive(false); // 시작 시 꺼두기
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
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
                    PlayerPrefs.SetInt("HasPlayedCutscene", 1);
                    SceneManager.LoadScene("SampleScene");
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
        foreach (var img in storyImages) img.SetActive(false);
        foreach (var txt in storyTexts) txt.SetActive(false);

        storyImages[index].SetActive(true);
        storyTexts[index].SetActive(true);

        currentTextComponent = storyTexts[index].GetComponentInChildren<UnityEngine.UI.Text>();
        fullText = currentTextComponent.text;
        currentTextComponent.text = "";

        tapToContinue.SetActive(false); // 매 컷 시작마다 꺼두기
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


