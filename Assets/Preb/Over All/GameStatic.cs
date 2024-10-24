using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public static class GameStatic 
{
    class AudioSrcContext : MonoBehaviour
    {
    }
    static private ObjectPool<AudioSource> AudioPool;

    public static void GameStarted()
    {
        AudioPool = new ObjectPool<AudioSource>(CreatAudioScr, null, null, DestroyAudioScr, false, 5, 10);
    }
    private static void DestroyAudioScr(AudioSource audioSource)
    {
        GameObject.Destroy(audioSource.gameObject);
    }

    private static AudioSource CreatAudioScr()
    {
        GameObject audioSourceGameOBJ = new GameObject("audioSourceGameOBJ", typeof(AudioSource), typeof(AudioSrcContext));
        AudioSource audioSource = audioSourceGameOBJ.GetComponent<AudioSource>();

        audioSource.volume = 1f;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        return audioSource;
    }

    public static void SetGamePause(bool pause)
    {
        Time.timeScale = pause? 0 :1;
    }
    public static void PlayAudioAtLoc(AudioClip audioClip,Vector3 loc, float volume)
    {
        AudioSource newScr = AudioPool.Get();
        newScr.volume = volume;
        newScr.gameObject.transform.position = loc;
        newScr.PlayOneShot(audioClip);

        newScr.GetComponent<AudioSrcContext>().StartCoroutine(ReleaseAudio(newScr, audioClip.length));
    }

    private static IEnumerator ReleaseAudio(AudioSource newScr, float length)
    {
       yield return new WaitForSeconds(length);
        AudioPool.Release(newScr); 
    }

    internal static void PlayAudioAtPlayer(AudioClip abilitySound, float volume)
    {
        PlayAudioAtLoc(abilitySound,Camera.main.transform.position,volume);
    }
}
