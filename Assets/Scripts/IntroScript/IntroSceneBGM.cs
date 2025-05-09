using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class IntroSceneBGM : MonoBehaviour
{
    [Range(0f, 1f)]
    public float volume = 0.3f; // 기본값 0.3

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.loop = false; // 필요시 true로 변경 가능
        audioSource.Play(); // 씬 시작과 동시에 자동 재생
    }
}
