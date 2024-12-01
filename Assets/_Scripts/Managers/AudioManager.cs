using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Components")]
    public AudioSource exploreBgmPlayer;
    public AudioSource combatBgmPlayer;
    public GameObject sfxObject;
    public AudioSource[] sfxPlayers;

    [Header("__________ BGM _______________________________________________________________")]
    public List<BgmSO> bgmDatas;
    public float bgmVolume;

    private Queue<BgmSO> exploreBgmQueue = new Queue<BgmSO>();
    private bool isCombatActive = false;

    // public ExploreBgmPlayer exploreBgmPlayer;

    [Header("__________ SFX _______________________________________________________________")]
    public List<SfxSO> sfxDatas;
    public float sfxVolume;
    public int channels;
    int channelIndex;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        exploreBgmPlayer.volume = bgmVolume;

        combatBgmPlayer.volume = 0f;

        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }

        PlayExploreBgm();
    }

    public void PlayExploreBgm()
    {
        var introTrack = bgmDatas.FirstOrDefault(bgm => bgm.name == "Intro");
        {
            exploreBgmQueue.Enqueue(introTrack);
        }

        var exploreTracks = bgmDatas.Where(bgm => bgm.name.StartsWith("Explore_")).OrderBy(_ => Random.value).ToList();
        foreach (var track in exploreTracks)
        {
            exploreBgmQueue.Enqueue(track);
        }

        PlayNextExploreBgm();
    }

    private void PlayNextExploreBgm()
    {
        if (isCombatActive)
        {
            return;
        }

        if (exploreBgmQueue.Count == 0)
        {
            Debug.Log("Explore BGM Queue is empty. Reshuffling tracks...");
            ReshuffleExploreBgmQueue();
        }

        var nextBgm = exploreBgmQueue.Dequeue();
        exploreBgmPlayer.clip = nextBgm.bgmClip;
        exploreBgmPlayer.Play();
        exploreBgmPlayer.volume = bgmVolume;

        exploreBgmPlayer.SetScheduledEndTime(AudioSettings.dspTime + exploreBgmPlayer.clip.length);
        StartCoroutine(WaitForExploreBgmEnd());
    }

    private void ReshuffleExploreBgmQueue()
    {
        var exploreTracks = bgmDatas.Where(bgm => bgm.name.StartsWith("Explore_")).OrderBy(_ => Random.value).ToList();
        foreach (var track in exploreTracks)
        {
            exploreBgmQueue.Enqueue(track);
        }
    }

    private IEnumerator WaitForExploreBgmEnd()
    {
        yield return new WaitUntil(() => !exploreBgmPlayer.isPlaying);
        PlayNextExploreBgm();
    }

    public void StartCombatBgm(string fileName)
    {
        if (isCombatActive)
        {
            return;
        }

        isCombatActive = true;

        var matchingCombatBgms = bgmDatas
        .Where(bgm => bgm.name.StartsWith(fileName + "_"))
        .ToList();

        if (matchingCombatBgms.Count == 0)
        {
            Debug.LogWarning($"No Combat BGM matches the key '{fileName}'!");
            return;
        }

        var selectedCombatBgm = matchingCombatBgms[Random.Range(0, matchingCombatBgms.Count)];
        combatBgmPlayer.clip = selectedCombatBgm.bgmClip;

        StartCoroutine(FadeOutExploreAndPlayCombat());
    }

    private IEnumerator FadeOutExploreAndPlayCombat()
    {
        float startVolume = exploreBgmPlayer.volume;

        while (exploreBgmPlayer.volume > 0f)
        {
            exploreBgmPlayer.volume -= startVolume * Time.deltaTime / 0.5f;
            yield return null;
        }
        exploreBgmPlayer.Pause();

        combatBgmPlayer.Play();
        float targetVolume = bgmVolume;
        combatBgmPlayer.volume = 0f;

        while (combatBgmPlayer.volume < targetVolume)
        {
            combatBgmPlayer.volume += targetVolume * Time.deltaTime / 0.5f;
            yield return null;
        }

        combatBgmPlayer.volume = targetVolume;
    }

    public void EndCombatBgm()
    {
        if (!isCombatActive)
        {
            return;
        }

        isCombatActive = false;

        StartCoroutine(FadeOutCombatAndResumeExplore());
    }

    private IEnumerator FadeOutCombatAndResumeExplore()
    {
        float startVolume = combatBgmPlayer.volume;

        while (combatBgmPlayer.volume > 0f)
        {
            combatBgmPlayer.volume -= startVolume * Time.deltaTime / 5f;
            yield return null;
        }
        combatBgmPlayer.Stop();

        float targetVolume = bgmVolume;
        exploreBgmPlayer.Play();

        while (exploreBgmPlayer.volume < targetVolume)
        {
            exploreBgmPlayer.volume += targetVolume * Time.deltaTime / 5f;
            yield return null;
        }

        exploreBgmPlayer.volume = targetVolume;
    }



    // SFX //



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
            var randomPitch = Random.Range(-0.1f, +0.1f);
            sfxPlayers[loopIndex].pitch = 1f + randomPitch;
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}