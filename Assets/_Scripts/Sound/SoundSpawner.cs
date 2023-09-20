using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpawner : Spawner
{
    private static SoundSpawner instance;
    public static SoundSpawner Instance { get => instance; }

    const string EFFECT_NAME = "EffectSource";
    const string MUSIC_NAME = "MusicSource";

    protected override void Awake()
    {
        base.Awake();
        if (SoundSpawner.instance != null) return;
        SoundSpawner.instance = this;
    }

    public virtual AudioSource PlayEffect(AudioClip audioClip, Vector3 pos, Quaternion rot)
    {
        AudioSource audioSource = this.Play(SoundSpawner.EFFECT_NAME, audioClip, pos, rot);
        //SoundManager.Instance.AddEffect(audioSource);
        return audioSource;
    }

    public virtual AudioSource PlayMusic(AudioClip audioClip, Vector3 pos, Quaternion rot)
    {
        AudioSource audioSource = this.Play(SoundSpawner.MUSIC_NAME, audioClip, pos, rot);
        //SoundManager.Instance.AddEffect(audioSource);
        return audioSource;
    }

    public virtual AudioSource Play(string audioName, AudioClip audioClip, Vector3 pos, Quaternion rot)
    {
        Transform audioObject = this.Spawn(audioName, pos, rot);
        StartCoroutine(DespawnBytime(audioObject));
        AudioSource audioSource = audioObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        return audioSource;
    }

    IEnumerator DespawnBytime(Transform transform)
    {
        yield return new WaitForSeconds(2f);
        Despawn(transform);
    }
}
