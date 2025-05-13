using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 메인 메뉴의 버튼 클릭 이벤트를 처리합니다.
/// - New Game, Continue, Stats, Shop, Options, Credits, Exit
/// - Stats 버튼으로 StatsScene 로딩
/// - Shop 버튼으로 ShopScene 로딩
/// </summary>
public class MainMenuController : MonoBehaviour
{
    //────────────────────────────────────────────────────────────────────────
    // 1) Inspector 설정 필드
    //────────────────────────────────────────────────────────────────────────

    [Header("패널")]
    [Tooltip("메인 메뉴 버튼 5개가 포함된 패널")]
    public GameObject mainMenuPanel;
    [Tooltip("옵션 메뉴(볼륨 슬라이더)가 포함된 패널")]
    public GameObject optionsPanel;

    //────────────────────────────────────────────────────────────────────────
    // 2) Unity 생명주기 콜백
    //────────────────────────────────────────────────────────────────────────

    private void Start()
    {
        // 시작 시 옵션 패널은 숨기기
        optionsPanel.SetActive(false);
    }

    //────────────────────────────────────────────────────────────────────────
    // 3) 버튼 클릭 이벤트 핸들러
    //────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// New Game 버튼 클릭 시 IntroScene 로드
    /// </summary>
    public void OnNewGame()
    {
        SceneManager.LoadScene("IntroScene");
    }

    /// <summary>
    /// Continue 버튼 클릭 시 이어하기 기능 (미구현)
    /// </summary>
    public void OnContinue()
    {
        // TODO: 이어하기 기능 추후 구현
    }

    /// <summary>
    /// Stats 버튼 클릭 시 StatsScene 로드
    /// </summary>
    public void OnStats()
    {
        SceneManager.LoadScene("StatsScene");
    }

    /// <summary>
    /// Shop 버튼 클릭 시 ShopScene 로드
    /// </summary>
    public void OnShop()
    {
        SceneManager.LoadScene("ShopScene");
    }

    /// <summary>
    /// Options 버튼 클릭 시 메인 메뉴 숨기고 옵션 패널 표시
    /// </summary>
    public void OnOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    /// <summary>
    /// Credits 버튼 클릭 시 CreditsScene 로드
    /// </summary>
    public void OnCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    /// <summary>
    /// 옵션화면 뒤로가기 버튼: 옵션 패널 숨기고 메인 메뉴 표시
    /// </summary>
    public void OnOptionsBack()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    /// <summary>
    /// Exit 버튼 클릭 시 애플리케이션 종료
    /// </summary>
    public void OnExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("Application.Quit() 호출");
#endif
    }
}
