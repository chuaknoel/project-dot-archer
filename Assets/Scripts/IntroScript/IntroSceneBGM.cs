using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class IntroSceneBGM : MonoBehaviour
{
    [Range(0f, 1f)]
    public float volume = 0.3f; // �⺻�� 0.3

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.loop = false; // �ʿ�� true�� ���� ����
        audioSource.Play(); // �� ���۰� ���ÿ� �ڵ� ���
    }
}
