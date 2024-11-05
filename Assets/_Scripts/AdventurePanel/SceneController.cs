using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SceneController : Singleton<SceneController>
{
    [Header("Playing Data")]
    [SerializeField] private int currentQuestIndex;

    [Header("Components")]
    [SerializeField] private RectTransform selectsScene;
    [SerializeField] private RectTransform contentsScene;
    [SerializeField] private RectTransform rollDicePanel;
    [SerializeField] private Image transitionImage;

    [Header("Player Assets")]
    [SerializeField] private PlayerDiceSO playerDiceData;

    protected override void Awake()
    {
        base.Awake();

        if (!selectsScene.gameObject.activeSelf)
        {
            selectsScene.gameObject.SetActive(true);
        }
        if (contentsScene.gameObject.activeSelf)
        {
            contentsScene.gameObject.SetActive(false);
        }
        if (rollDicePanel.gameObject.activeSelf)
        {
            rollDicePanel.gameObject.SetActive(false);
        }

        ResetContentsScene();
        ResetRollDicePanel();
    }



    // PUBLICS //

    public void TransitionToContents(QuestSO _questData)
    {
        StartCoroutine(FadeAndContents(_questData));
    }

    public void TransitionToRewards(QuestSO _questData, ContentSO _contentData)
    {
        StartCoroutine(FadeAndRewards(_questData, _contentData));
    }

    public void TransitionToSelects(QuestSO _questData)
    {
        StartCoroutine(FadeAndSelects(_questData));
    }



    // PRIVATES //

    private IEnumerator FadeAndRewards(QuestSO _questData, ContentSO _contentData)
    {
        yield return StartCoroutine(FadeIn());

        ResetContentsScene();
        ResetRollDicePanel();
        SetRewardsScene(_questData, _contentData);

        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeAndContents(QuestSO _questData)
    {

        yield return StartCoroutine(FadeIn());

        DeActivateSelectsScene();
        SetContentsScene(_questData);
        SetRollDicePanel();

        selectsScene.gameObject.SetActive(false);
        contentsScene.gameObject.SetActive(true);
        rollDicePanel.gameObject.SetActive(true);

        InfoTabController.Instance.HandleSkillTabActivate();

        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeAndSelects(QuestSO _questData)
    {
        yield return StartCoroutine(FadeIn());

        ResetContentsScene();
        ResetRollDicePanel();
        ActivateSelectsScene();

        contentsScene.gameObject.SetActive(false);
        rollDicePanel.gameObject.SetActive(false);
        selectsScene.gameObject.SetActive(true);

        InfoTabController.Instance.HandleDiceTabActivate();

        yield return StartCoroutine(FadeOut());
    }



    // SELECTS SCENE //

    private void ActivateSelectsScene()
    {
        List<Quest> selectableQuests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in selectableQuests)
        {
            quest.ActivateQuestCard();
        }
    }

    private void DeActivateSelectsScene()
    {
        List<Quest> selectableQuests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in selectableQuests)
        {
            quest.DeActivateQuestCard();
        }
    }

    // CONTENTS/REWARDS SCENE //

    private void SetContentsScene(QuestSO _questData)
    {
        for (int i = 0; i < _questData.questContents.Count; i++)
        {
            GameObject content = Instantiate(_questData.questContents[i].contentTemplate);
            content.transform.SetParent(contentsScene, false);
            content.name = $"content_({i + 1})";

            content.GetComponent<IContent>().SetContentComponents(_questData, _questData.questContents[i]);
        }
    }

    private void SetRewardsScene(QuestSO _questData, ContentSO _contentData)
    {
        for (int i = 0; i < _contentData.rewardContents.Count; i++)
        {
            GameObject content = Instantiate(_contentData.rewardContents[i].contentTemplate);
            content.transform.SetParent(contentsScene, false);
            content.name = $"reward Content_({i + 1})";

            content.GetComponent<IContent>().SetContentComponents(_questData, _contentData.rewardContents[i]);
        }
    }

    private void ResetContentsScene()
    {
        List<IContent> completedContents = new List<IContent>(contentsScene.GetComponentsInChildren<IContent>());
        foreach (var content in completedContents)
        {
            content.DestroyContent();
        }
    }



    // ROLL DICE PANEL //

    private void SetRollDicePanel()
    {
        int StrAdvancedDiceAmount = playerDiceData.StrAdvancedDice.Value;
        int DexAdvancedDiceAmount = playerDiceData.DexAdvancedDice.Value;
        int IntAdvancedDiceAmount = playerDiceData.IntAdvancedDice.Value;
        int WilAdvancedDiceAmount = playerDiceData.WilAdvancedDice.Value;

        int StrNormalDiceAmount = playerDiceData.StrNormalDice.Value;
        int DexNormalDiceAmount = playerDiceData.DexNormalDice.Value;
        int IntNormalDiceAmount = playerDiceData.IntNormalDice.Value;
        int WilNormalDiceAmount = playerDiceData.WilNormalDice.Value;

        List<GameObject> dicePrefabs = new List<GameObject>();

        for (int i = 0; i < StrAdvancedDiceAmount; i++)
        {
            GameObject StrAdvancedDicePrefab = Instantiate(playerDiceData.StrAdvancedDice_Roll);
            StrAdvancedDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(StrAdvancedDicePrefab);
        }

        for (int i = 0; i < StrNormalDiceAmount; i++)
        {
            GameObject StrNormalDicePrefab = Instantiate(playerDiceData.StrNormalDice_Roll);
            StrNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(StrNormalDicePrefab);
        }

        for (int i = 0; i < DexAdvancedDiceAmount; i++)
        {
            GameObject DexAdvancedDicePrefab = Instantiate(playerDiceData.DexAdvancedDice_Roll);
            DexAdvancedDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(DexAdvancedDicePrefab);
        }

        for (int i = 0; i < DexNormalDiceAmount; i++)
        {
            GameObject DexNormalDicePrefab = Instantiate(playerDiceData.DexNormalDice_Roll);
            DexNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(DexNormalDicePrefab);
        }

        for (int i = 0; i < IntAdvancedDiceAmount; i++)
        {
            GameObject IntAdvancedDicePrefab = Instantiate(playerDiceData.IntAdvancedDice_Roll);
            IntAdvancedDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(IntAdvancedDicePrefab);
        }

        for (int i = 0; i < IntNormalDiceAmount; i++)
        {
            GameObject IntNormalDicePrefab = Instantiate(playerDiceData.IntNormalDice_Roll);
            IntNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(IntNormalDicePrefab);
        }

        for (int i = 0; i < WilAdvancedDiceAmount; i++)
        {
            GameObject WilAdvancedDicePrefab = Instantiate(playerDiceData.WilAdvancedDice_Roll);
            WilAdvancedDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(WilAdvancedDicePrefab);
        }

        for (int i = 0; i < WilNormalDiceAmount; i++)
        {
            GameObject WilNormalDicePrefab = Instantiate(playerDiceData.WilNormalDice_Roll);
            WilNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(WilNormalDicePrefab);
        }

        int rowCount = Mathf.CeilToInt(dicePrefabs.Count / 12f);
        int rowHeight = 100;
        int baseYPosition = -470 + ((rowCount - 1) * rowHeight);

        for (int i = 0; i < dicePrefabs.Count; i++)
        {
            int currentRow = i / 12;
            int indexInRow = i % 12;

            float xPosition = -275 + (indexInRow * 100);
            float yPosition = baseYPosition - (currentRow * rowHeight);

            dicePrefabs[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);
        }
    }

    private void ResetRollDicePanel()
    {
        List<RollDice> previousRollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());
        foreach (var rollDices in previousRollDices)
        {
            rollDices.DestroyRollDice();
        }
    }



    // TRANSITIONS //

    private IEnumerator FadeIn()
    {
        float duration = 0.2f;
        float elapsedTime = 0f;

        Color color = transitionImage.color;
        color.a = 0;
        transitionImage.color = color;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / duration);
            transitionImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        transitionImage.color = color;
    }

    private IEnumerator FadeOut()
    {
        float duration = 0.2f;
        float elapsedTime = 0f;

        Color color = transitionImage.color;
        color.a = 1;
        transitionImage.color = color;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / duration);
            transitionImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        transitionImage.color = color;
    }
}