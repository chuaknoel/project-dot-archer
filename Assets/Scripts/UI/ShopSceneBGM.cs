using UnityEngine;

public class ShopSceneBGM : MonoBehaviour
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