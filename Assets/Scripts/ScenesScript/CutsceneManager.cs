using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("�׽�Ʈ�� �ƾ� ���� ����")]
    public bool forcePlayCutscene = false;

    public GameObject[] storyImages;
    public GameObject[] storyTexts;
    public float typingSpeed = 0.1f;

    public GameObject tapToContinue; // �ν����Ϳ��� ����

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

        tapToContinue.SetActive(false); // ���� �� ���α�
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

        tapToContinue.SetActive(false); // �� �� ���۸��� ���α�
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


