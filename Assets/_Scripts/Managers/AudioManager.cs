using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class AudioManager : Singleton<AudioManager>
{
    [Header("__________ BGM _______________________________________________________________")]
    public List<BgmSO> bgmDatas;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("__________ SFX _______________________________________________________________")]
    public List<SfxSO> sfxDatas;
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

        PlayBgm("Intro");
    }

    public void PlayBgm(string fileName)
    {
        var matchingClips = bgmDatas
            .Where(bgm => bgm.name.StartsWith(fileName + "_"))
            .ToList();

        if (matchingClips.Count == 0)
        {
            Debug.LogWarning($"No BGM matches the key '{fileName}'!");
            return;
        }

        BgmSO selectedBgm = matchingClips[Random.Range(0, matchingClips.Count)];

        if (bgmPlayer.isPlaying)
        {
            StartCoroutine(FadeBgm(selectedBgm));
        }
        else
        {
            bgmPlayer.clip = selectedBgm.bgmClip;
            bgmPlayer.volume = bgmVolume;
            bgmPlayer.Play();
        }
    }

    private IEnumerator FadeBgm(BgmSO BgmData)
    {
        float startVolume = bgmPlayer.volume;

        while (bgmPlayer.volume > 0f)
        {
            bgmPlayer.volume -= startVolume * Time.deltaTime / 1f;
            yield return null;
        }

        bgmPlayer.Stop();
        bgmPlayer.clip = BgmData.bgmClip;

        float targetVolume = bgmVolume;
        bgmPlayer.volume = 0f;
        bgmPlayer.Play();

        while (bgmPlayer.volume < targetVolume)
        {
            bgmPlayer.volume += targetVolume * Time.deltaTime / 1f;
            yield return null;
        }
    }

    public void PlaySfx(string fileName)
    {
        var matchingClips = sfxDatas
            .Where(sfx => sfx.name.StartsWith(fileName + "_"))
            .ToList();

        if (matchingClips.Count == 0)
        {
            Debug.LogWarning($"No SFX matches the key '{fileName}'!");
            return;
        }

        SfxSO selectedSfx = matchingClips[Random.Range(0, matchingClips.Count)];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].pitch = 1f;
            sfxPlayers[loopIndex].clip = selectedSfx.sfxClip;
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void PlaySfxWithPitch(string fileName)
    {
        var matchingClips = sfxDatas
            .Where(sfx => sfx.name.StartsWith(fileName + "_"))
            .ToList();

        if (matchingClips.Count == 0)
        {
            Debug.LogWarning($"No SFX matches the key '{fileName}'!");
            return;
        }


        SfxSO selectedSfx = matchingClips[Random.Range(0, matchingClips.Count)];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            channelIndex = loopIndex;

            sfxPlayers[loopIndex].clip = selectedSfx.sfxClip;
            sfxPlayers[loopIndex].pitch = 1f;
            var randomPitch = Random.Range(-0.15f, +0.15f);
            sfxPlayers[loopIndex].pitch = 1f + randomPitch;
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}