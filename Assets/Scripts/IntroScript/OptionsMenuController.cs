using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    [Header("패널")]
    public GameObject mainMenuPanel;   // MainMenuPanel 오브젝트
    public GameObject optionsPanel;    // OptionsPanel (이 스크립트가 붙은 오브젝트)

    [Header("볼륨 슬라이더")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("버튼")]
    public Button backButton;
    public Button exitButton;

    void Awake()
    {
        backButton.onClick.AddListener(OnOptionsBack);
        exitButton.onClick.AddListener(OnGameExit);
    }

    void OnEnable()
    {
        // 옵션창 열릴 때 초기화 (필요 시)
        musicSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }

    // 뒤로가기: 옵션 패널 끄고 메인 메뉴 켜기
    public void OnOptionsBack()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // 게임 종료
    public void OnGameExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("Application.Quit() 호출");
#endif
    }
}