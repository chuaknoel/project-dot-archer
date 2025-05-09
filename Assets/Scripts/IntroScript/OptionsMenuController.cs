using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    [Header("�г�")]
    public GameObject mainMenuPanel;   // MainMenuPanel ������Ʈ
    public GameObject optionsPanel;    // OptionsPanel (�� ��ũ��Ʈ�� ���� ������Ʈ)

    [Header("���� �����̴�")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("��ư")]
    public Button backButton;
    public Button exitButton;

    void Awake()
    {
        backButton.onClick.AddListener(OnOptionsBack);
        exitButton.onClick.AddListener(OnGameExit);
    }

    void OnEnable()
    {
        // �ɼ�â ���� �� �ʱ�ȭ (�ʿ� ��)
        musicSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }

    // �ڷΰ���: �ɼ� �г� ���� ���� �޴� �ѱ�
    public void OnOptionsBack()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // ���� ����
    public void OnGameExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("Application.Quit() ȣ��");
#endif
    }
}