using UnityEngine;

public class StartSceneBGM : MonoBehaviour
{
    private AudioSource bgmSource;

    private void Start()
    {
        bgmSource = GetComponent<AudioSource>();

        if (bgmSource != null && !bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }
}

