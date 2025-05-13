using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���� �޴��� ��ư Ŭ�� �̺�Ʈ�� ó���մϴ�.
/// - New Game, Continue, Stats, Shop, Options, Credits, Exit
/// - Stats ��ư���� StatsScene �ε�
/// - Shop ��ư���� ShopScene �ε�
/// </summary>
public class MainMenuController : MonoBehaviour
{
    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 1) Inspector ���� �ʵ�
    //������������������������������������������������������������������������������������������������������������������������������������������������

    [Header("�г�")]
    [Tooltip("���� �޴� ��ư 5���� ���Ե� �г�")]
    public GameObject mainMenuPanel;
    [Tooltip("�ɼ� �޴�(���� �����̴�)�� ���Ե� �г�")]
    public GameObject optionsPanel;

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 2) Unity �����ֱ� �ݹ�
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private void Start()
    {
        // ���� �� �ɼ� �г��� �����
        optionsPanel.SetActive(false);
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 3) ��ư Ŭ�� �̺�Ʈ �ڵ鷯
    //������������������������������������������������������������������������������������������������������������������������������������������������

    /// <summary>
    /// New Game ��ư Ŭ�� �� IntroScene �ε�
    /// </summary>
    public void OnNewGame()
    {
        SceneManager.LoadScene("IntroScene");
    }

    /// <summary>
    /// Continue ��ư Ŭ�� �� �̾��ϱ� ��� (�̱���)
    /// </summary>
    public void OnContinue()
    {
        // TODO: �̾��ϱ� ��� ���� ����
    }

    /// <summary>
    /// Stats ��ư Ŭ�� �� StatsScene �ε�
    /// </summary>
    public void OnStats()
    {
        SceneManager.LoadScene("StatsScene");
    }

    /// <summary>
    /// Shop ��ư Ŭ�� �� ShopScene �ε�
    /// </summary>
    public void OnShop()
    {
        SceneManager.LoadScene("ShopScene");
    }

    /// <summary>
    /// Options ��ư Ŭ�� �� ���� �޴� ����� �ɼ� �г� ǥ��
    /// </summary>
    public void OnOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    /// <summary>
    /// Credits ��ư Ŭ�� �� CreditsScene �ε�
    /// </summary>
    public void OnCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    /// <summary>
    /// �ɼ�ȭ�� �ڷΰ��� ��ư: �ɼ� �г� ����� ���� �޴� ǥ��
    /// </summary>
    public void OnOptionsBack()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    /// <summary>
    /// Exit ��ư Ŭ�� �� ���ø����̼� ����
    /// </summary>
    public void OnExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("Application.Quit() ȣ��");
#endif
    }
}
