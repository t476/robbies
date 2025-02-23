﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;//单例 singleton模式：单例类

    [Header("环境声音")]
    public AudioClip ambientClip;
    public AudioClip musicClip;

    [Header("FX音效")]
    public AudioClip deathFXClip;
    public AudioClip orbFXClip;
    public AudioClip doorFXClip;
    public AudioClip StartLevelClip;
    public AudioClip WinClip;


    [Header("Robbie音效")]
    public AudioClip[] walkStepClips;
    public AudioClip[] crouchStepClips;
    public AudioClip jumpClip;
    public AudioClip deathClip;


    public AudioClip deathVoiceClip;
    public AudioClip jumpVoiceClip;
    public AudioClip orbVoiceClip;

    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource fxSource;
    AudioSource playerSource;
    AudioSource voiceSource;

    public AudioMixerGroup ambientGroup, musicGroup, FXGroup, playerGroup, voiceGroup;


    private void Awake()
    {///死亡不重复添加audiomanager (因为dontdestroyon load)singleton模式：单例类
        if (current != null)
        {
            Destroy(gameObject);
            return ;

        }
        current = this;
        //场景加载时不销毁声音
        DontDestroyOnLoad(gameObject);
        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        fxSource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();

        ambientSource.outputAudioMixerGroup = ambientGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        musicSource.outputAudioMixerGroup = musicGroup;
        fxSource.outputAudioMixerGroup = FXGroup;
        voiceSource.outputAudioMixerGroup = voiceGroup;


        StartLevelAudio();


    }

    public static void PlayDoorOpenAudio()
    {
        current.fxSource.clip = current.doorFXClip;
        current.fxSource.PlayDelayed(1f);
    }
    void StartLevelAudio()
    {
        current.ambientSource.clip = current.ambientClip;
        current.ambientSource.loop = true;
        current.ambientSource.Play();

        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();
        current.fxSource.clip = current.StartLevelClip;
        current.fxSource.Play();
        current.playerSource.Stop();

    }
    public static void PlayerWonAudio()
    {
        current.fxSource.clip = current.WinClip;
        current.fxSource.Play();
    }
    public static void PlayFootStepAudio()
    {
        int index = Random.Range(0, current.walkStepClips.Length);
        current.playerSource.clip = current.walkStepClips[index];
        current.playerSource.Play();
    }
    public static void PlayCrouchFootStepAudio()
    {
        int index = Random.Range(0, current.crouchStepClips.Length);
        current.playerSource.clip = current.crouchStepClips[index];
        current.playerSource.Play();
    }
    public static void PlayJumpAudio()
    {
        current.playerSource.clip = current.jumpClip;
        current.playerSource.Play();

        current.voiceSource.clip = current.jumpVoiceClip;
        current.voiceSource.Play();
    }
    public static void PlayDeathAudio()
    {
        current.playerSource.clip = current.deathClip;
        current.playerSource.Play();

        current.voiceSource.clip = current.deathVoiceClip;
        current.voiceSource.Play();

        current.fxSource.clip = current.deathFXClip;
        current.fxSource.Play();
    }
    public static void PlayOrbAudio()
    {
        current.playerSource.clip = current.orbFXClip;
        current.playerSource.Play();

        current.voiceSource.clip = current.orbVoiceClip;
        current.voiceSource.Play();

    }


}
