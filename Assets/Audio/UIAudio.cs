using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/UIAudio")]
public class UIAudio : ScriptableObject
{
    [SerializeField] AudioClip ClickAudioClip;
    [SerializeField] AudioClip CommitAudioClip;
    [SerializeField] AudioClip SelectAudioClip;
    [SerializeField] AudioClip WinAudio;

    public void PlayClick()
    {
        PlayAudio(ClickAudioClip);
    }
    public void PlayCommit()
    {
        PlayAudio(CommitAudioClip);
    }
    public void PlaySelect()
    {
        PlayAudio(SelectAudioClip);
    }

    internal void PlayWin()
    {
        PlayAudio(WinAudio);
    }

    void PlayAudio(AudioClip clip)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
