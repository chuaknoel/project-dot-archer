using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // New Game ��ư Ŭ�� �� IntroScene(�ƾ�)���� �̵�
    public void OnNewGame()
    {
        SceneManager.LoadScene("IntroScene");
    }

    // ���߿� �̾��ϱ�, ���, ����, �ɼ� � ���⿡ �޼��� �߰�
    public void OnContinue()
    {
        // TODO: �̾��ϱ� ����
    }

    public void OnStats()
    {
        // TODO
    }

    public void OnShop()
    {
        // TODO
    }

    public void OnOptions()
    {
        // TODO
    }
}