using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;

    void Start()
    {
        // 설정창 끄고 시작
        settingsPanel.SetActive(false);

        // 저장된 볼륨 불러오기
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        AudioListener.volume = savedVolume;
        volumeSlider.value = savedVolume;

        // 슬라이더 변경 이벤트 연결
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    public void OnStartButton()
    {
        if (PlayerPrefs.GetInt("HasPlayedCutscene", 0) == 1)
            SceneManager.LoadScene("SampleScene");
        else
            SceneManager.LoadScene("CutsceneScene");
    }

    public void OnExitButton()
    {
        Application.Quit();
        Debug.Log("게임 종료 요청 (에디터에서는 무시됨)");
    }

    public void OnOptionsButton()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("BGMVolume", value);
    }
}
