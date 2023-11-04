using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private List<AudioData> audioDataList;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //audioSource.volume = volumeSlider;
    }
    public void PlaySE(AudioType type)
    {
        AudioClip audioClip = FindAudioClip(type);
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayBGM(AudioType type)
    {
        audioSource.Pause();
        AudioClip audioClip = FindAudioClip(type);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private AudioClip FindAudioClip(AudioType type)
    {
        var data = audioDataList.Find(x => x.Type == type);
        if (data is null)
        {
            throw new Exception($"Audio data is null. (type: {type})");
        }

        if (data.Clip is null)
        {
            throw new Exception($"Audio clip is null. (type: {type})");
        }

        return data.Clip;
    }
}
