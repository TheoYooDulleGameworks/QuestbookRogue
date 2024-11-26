using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("__________ BGM _______________________________________________________________")]
    public List<AudioClip> bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("__________ SFX _______________________________________________________________")]
    public List<AudioClip> sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = 1f;

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }

        PlayBgm(Bgm.Intro);
    }

    public void PlayBgm(Bgm Bgm)
    {
        if (bgmPlayer.isPlaying)
        {
            StartCoroutine(FadeBgm(Bgm));
        }
        else
        {
            bgmPlayer.clip = bgmClips[(int)Bgm];
            bgmPlayer.Play();
        }
    }

    private IEnumerator FadeBgm(Bgm Bgm)
    {
        float startVolume = bgmPlayer.volume;

        while (bgmPlayer.volume > 0f)
        {
            bgmPlayer.volume -= startVolume * Time.deltaTime / 1f;
            yield return null;
        }

        bgmPlayer.Stop();

        bgmPlayer.clip = bgmClips[(int)Bgm];

        float targetVolume = bgmVolume;
        bgmPlayer.volume = 0f;
        bgmPlayer.Play();

        while (bgmPlayer.volume < targetVolume)
        {
            bgmPlayer.volume += targetVolume * Time.deltaTime / 1f;
            yield return null;
        }
    }

    public void PlaySfx(Sfx Sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)Sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}

public enum Bgm
{
    Intro,
    Combat,
}

public enum Sfx
{

}