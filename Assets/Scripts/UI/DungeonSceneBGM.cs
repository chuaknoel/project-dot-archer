using UnityEngine;

public class DungeonSceneBGM : MonoBehaviour
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
