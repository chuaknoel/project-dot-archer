using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;

    void Start()
    {
        // ����â ���� ����
        settingsPanel.SetActive(false);

        // ����� ���� �ҷ�����
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        AudioListener.volume = savedVolume;
        volumeSlider.value = savedVolume;

        // �����̴� ���� �̺�Ʈ ����
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
        Debug.Log("���� ���� ��û (�����Ϳ����� ���õ�)");
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
