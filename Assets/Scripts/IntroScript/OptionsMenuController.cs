using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenuController : MonoBehaviour
{
    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 1) Inspector�� �巡��&����� �ʵ�
    //������������������������������������������������������������������������������������������������������������������������������������������������

    [Header("�г�")]
    [Tooltip("���� �޴� �г�")]
    public GameObject mainMenuPanel;
    [Tooltip("�ɼ� �޴� �г�")]
    public GameObject optionsPanel;

    [Header("AudioMixer")]
    [Tooltip("������Ʈ�� �ִ� AudioMixer�� �巡��&���")]
    public AudioMixer masterMixer;

    [Header("���� �����̴�")]
    [Tooltip("BGM ���� ������ �����̴�")]
    public Slider musicSlider;
    [Tooltip("SFX ���� ������ �����̴�")]
    public Slider sfxSlider;

    [Header("��ư")]
    [Tooltip("�ڷΰ��� ��ư")]
    public Button backButton;
    [Tooltip("���� ���� ��ư")]
    public Button exitButton;

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 2) PlayerPrefs Ű & AudioMixer �Ķ���� ��
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private const string BGM_KEY = "BGMVolume";
    private const string SFX_KEY = "SFXVolume";
    private const string BGM_PARAM = "MusicVolume"; // AudioMixer���� Expose�� �Ķ���� �̸�
    private const string SFX_PARAM = "SFXVolume";

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 3) �ʱ�ȭ
    //������������������������������������������������������������������������������������������������������������������������������������������������

    void Awake()
    {
        // ��ư �ݹ� ����
        backButton.onClick.AddListener(OnOptionsBack);
        exitButton.onClick.AddListener(OnGameExit);

        // �����̴� �� ���� �� ȣ��� �޼��� ����
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void OnEnable()
    {
        // �ɼ�â�� �� ������, ����� ���� �ҷ��� �����̴��� �ͼ��� �ݿ�
        float musicVal = PlayerPrefs.GetFloat(BGM_KEY, 0.5f);
        float sfxVal = PlayerPrefs.GetFloat(SFX_KEY, 0.5f);

        musicSlider.value = musicVal;
        sfxSlider.value = sfxVal;

        // �ͼ��� ��� ����
        SetMusicVolume(musicVal);
        SetSFXVolume(sfxVal);
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 4) ��ư �ڵ鷯
    //������������������������������������������������������������������������������������������������������������������������������������������������

    /// <summary>�ڷΰ���: �ɼ� �޴� �ݰ� ���� �޴� ����</summary>
    public void OnOptionsBack()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    /// <summary>���� ���� ��ư</summary>
    public void OnGameExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("Application.Quit() ȣ��");
#endif
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 5) ���� ���� �޼���
    //������������������������������������������������������������������������������������������������������������������������������������������������

    /// <summary>
    /// BGM ���� ����: �����̴� ��(0~1) �� dB(-80~0)�� ��ȯ �� AudioMixer�� PlayerPrefs�� ����
    /// </summary>
    private void SetMusicVolume(float sliderValue)
    {
        // dB ��� (0 �� -80dB, 1 �� 0dB)
        float dB = Mathf.Lerp(-80f, 0f, sliderValue);
        masterMixer.SetFloat(BGM_PARAM, dB);

        // ����� �����̴� �� ����
        PlayerPrefs.SetFloat(BGM_KEY, sliderValue);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// SFX ���� ����: �����̴� ��(0~1) �� dB(-80~0)�� ��ȯ �� AudioMixer�� PlayerPrefs�� ����
    /// </summary>
    private void SetSFXVolume(float sliderValue)
    {
        float dB = Mathf.Lerp(-80f, 0f, sliderValue);
        masterMixer.SetFloat(SFX_PARAM, dB);

        PlayerPrefs.SetFloat(SFX_KEY, sliderValue);
        PlayerPrefs.Save();
    }
}
