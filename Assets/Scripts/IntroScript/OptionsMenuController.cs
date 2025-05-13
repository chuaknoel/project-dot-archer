using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenuController : MonoBehaviour
{
    //────────────────────────────────────────────────────────────────────────
    // 1) Inspector에 드래그&드롭할 필드
    //────────────────────────────────────────────────────────────────────────

    [Header("패널")]
    [Tooltip("메인 메뉴 패널")]
    public GameObject mainMenuPanel;
    [Tooltip("옵션 메뉴 패널")]
    public GameObject optionsPanel;

    [Header("AudioMixer")]
    [Tooltip("프로젝트에 있는 AudioMixer를 드래그&드롭")]
    public AudioMixer masterMixer;

    [Header("볼륨 슬라이더")]
    [Tooltip("BGM 볼륨 조절용 슬라이더")]
    public Slider musicSlider;
    [Tooltip("SFX 볼륨 조절용 슬라이더")]
    public Slider sfxSlider;

    [Header("버튼")]
    [Tooltip("뒤로가기 버튼")]
    public Button backButton;
    [Tooltip("게임 종료 버튼")]
    public Button exitButton;

    //────────────────────────────────────────────────────────────────────────
    // 2) PlayerPrefs 키 & AudioMixer 파라미터 명
    //────────────────────────────────────────────────────────────────────────

    private const string BGM_KEY = "BGMVolume";
    private const string SFX_KEY = "SFXVolume";
    private const string BGM_PARAM = "MusicVolume"; // AudioMixer에서 Expose한 파라미터 이름
    private const string SFX_PARAM = "SFXVolume";

    //────────────────────────────────────────────────────────────────────────
    // 3) 초기화
    //────────────────────────────────────────────────────────────────────────

    void Awake()
    {
        // 버튼 콜백 연결
        backButton.onClick.AddListener(OnOptionsBack);
        exitButton.onClick.AddListener(OnGameExit);

        // 슬라이더 값 변경 시 호출될 메서드 연결
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void OnEnable()
    {
        // 옵션창을 열 때마다, 저장된 값을 불러와 슬라이더와 믹서에 반영
        float musicVal = PlayerPrefs.GetFloat(BGM_KEY, 0.5f);
        float sfxVal = PlayerPrefs.GetFloat(SFX_KEY, 0.5f);

        musicSlider.value = musicVal;
        sfxSlider.value = sfxVal;

        // 믹서에 즉시 적용
        SetMusicVolume(musicVal);
        SetSFXVolume(sfxVal);
    }

    //────────────────────────────────────────────────────────────────────────
    // 4) 버튼 핸들러
    //────────────────────────────────────────────────────────────────────────

    /// <summary>뒤로가기: 옵션 메뉴 닫고 메인 메뉴 열기</summary>
    public void OnOptionsBack()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    /// <summary>게임 종료 버튼</summary>
    public void OnGameExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("Application.Quit() 호출");
#endif
    }

    //────────────────────────────────────────────────────────────────────────
    // 5) 볼륨 조절 메서드
    //────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// BGM 볼륨 조절: 슬라이더 값(0~1) → dB(-80~0)로 변환 후 AudioMixer와 PlayerPrefs에 저장
    /// </summary>
    private void SetMusicVolume(float sliderValue)
    {
        // dB 계산 (0 → -80dB, 1 → 0dB)
        float dB = Mathf.Lerp(-80f, 0f, sliderValue);
        masterMixer.SetFloat(BGM_PARAM, dB);

        // 변경된 슬라이더 값 저장
        PlayerPrefs.SetFloat(BGM_KEY, sliderValue);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// SFX 볼륨 조절: 슬라이더 값(0~1) → dB(-80~0)로 변환 후 AudioMixer와 PlayerPrefs에 저장
    /// </summary>
    private void SetSFXVolume(float sliderValue)
    {
        float dB = Mathf.Lerp(-80f, 0f, sliderValue);
        masterMixer.SetFloat(SFX_PARAM, dB);

        PlayerPrefs.SetFloat(SFX_KEY, sliderValue);
        PlayerPrefs.Save();
    }
}
