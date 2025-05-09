using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    [Header("���� UI")]
    public GameObject backgroundImage;  // BackgroundImage
    public GameObject pressAnyText;     // PressAnyText
    public GameObject mainMenuPanel;    // MainMenuPanel

    private bool started = false;

    void Update()
    {
        if (!started && (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            started = true;
            ShowMainMenu();
        }
    }

    void ShowMainMenu()
    {
        // 1) ���� ȭ�� ��� ����
        backgroundImage.SetActive(false);
        pressAnyText.SetActive(false);

        // 2) ���� �޴� ��ư�� �ѱ�
        mainMenuPanel.SetActive(true);
    }
}
