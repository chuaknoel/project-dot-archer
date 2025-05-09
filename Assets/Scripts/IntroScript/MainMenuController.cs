using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // New Game 버튼 클릭 시 IntroScene(컷씬)으로 이동
    public void OnNewGame()
    {
        SceneManager.LoadScene("IntroScene");
    }

    // 나중에 이어하기, 통계, 상점, 옵션 등도 여기에 메서드 추가
    public void OnContinue()
    {
        // TODO: 이어하기 로직
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