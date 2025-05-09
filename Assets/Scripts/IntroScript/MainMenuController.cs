using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("패널")]
    public GameObject mainMenuPanel;   // 메인 메뉴 버튼 5개
    public GameObject optionsPanel;    // 옵션 패널 (볼륨 슬라이더)


    void Start()
    {
        // 시작 시 옵션 패널은 꺼두기
        optionsPanel.SetActive(false);

        // 슬라이더는 UI용으로만 남겨두고, 기능은 이후 구현
        // 예: musicSlider.onValueChanged.AddListener(...) 생략
    }

    // New Game 버튼 클릭
    public void OnNewGame()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void OnContinue()
    {
        // TODO: 이어하기 기능 추후 구현
    }

    public void OnStats()
    {
        // TODO: 스탯 화면 추후 구현
    }

    public void OnShop()
    {
        // TODO: 샵 화면 추후 구현
    }

    // Options 버튼 클릭
    public void OnOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    // 옵션화면 → 뒤로가기 버튼
    public void OnOptionsBack()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
