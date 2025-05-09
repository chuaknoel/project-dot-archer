using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    [Header("시작 UI")]
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
        // 1) 시작 화면 요소 끄기
        backgroundImage.SetActive(false);
        pressAnyText.SetActive(false);

        // 2) 메인 메뉴 버튼들 켜기
        mainMenuPanel.SetActive(true);
    }
}
