using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("�г�")]
    public GameObject mainMenuPanel;   // ���� �޴� ��ư 5��
    public GameObject optionsPanel;    // �ɼ� �г� (���� �����̴�)


    void Start()
    {
        // ���� �� �ɼ� �г��� ���α�
        optionsPanel.SetActive(false);

        // �����̴��� UI�����θ� ���ܵΰ�, ����� ���� ����
        // ��: musicSlider.onValueChanged.AddListener(...) ����
    }

    // New Game ��ư Ŭ��
    public void OnNewGame()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void OnContinue()
    {
        // TODO: �̾��ϱ� ��� ���� ����
    }

    public void OnStats()
    {
        // TODO: ���� ȭ�� ���� ����
    }

    public void OnShop()
    {
        // TODO: �� ȭ�� ���� ����
    }

    // Options ��ư Ŭ��
    public void OnOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    // �ɼ�ȭ�� �� �ڷΰ��� ��ư
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
