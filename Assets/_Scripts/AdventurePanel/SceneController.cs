using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class SceneController : Singleton<SceneController>
{
    [Header("Playing Data")]
    [SerializeField] private int currentQuestIndex;

    [Header("Components")]
    [SerializeField] private RectTransform selectsScene;
    [SerializeField] private RectTransform contentsScene;
    [SerializeField] private RectTransform rollDicePanel;

    [Header("Player Assets")]
    [SerializeField] private PlayerDiceSO playerDiceData;

    [Header("Tweening")]
    [SerializeField] private float rollDicePanelFadeTime = 0.5f;


    // SETTINGS //

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
    }



    // LOGICS //

    public void TransitionToContents(QuestSO _questData)
    {
        StartCoroutine(DeActivateSelectsRoutine());
        StartCoroutine(SetContentsRoutine(_questData));
        InfoTabController.Instance.HandleSkillTabActivate();
        SetRollDicePanel();
    }

    public void TransitionToSelects(QuestSO _questData)
    {
        StartCoroutine(ResetContentsRoutine());
        StartCoroutine(ActivateSelectsRoutine());
        InfoTabController.Instance.HandleDiceTabActivate();
        ResetRollDicePanel();
    }



    // SELECTS //

    private IEnumerator DeActivateSelectsRoutine()
    {
        List<Quest> quests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in quests)
        {
            quest.DeActivateQuestCard();
        }

        for (int i = 0; i < quests.Count; i++)
        {
            RectTransform questCanvas = quests[i].GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

            questCanvas.localScale = Vector3.one;
            questCanvas.localEulerAngles = Vector3.zero;

            questCanvas.DOKill();
            questCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.2f);
            questCanvas.DORotate(new Vector3(2f, 90f, 2f), 0.2f);

            yield return new WaitForSeconds(0.2f);
        }

        selectsScene.gameObject.SetActive(false);

        List<CancelButton> cancelButtons = new List<CancelButton>(contentsScene.GetComponentsInChildren<CancelButton>());
        foreach (var button in cancelButtons)
        {
            button.ActivateTarget();
        }
    }

    private IEnumerator ActivateSelectsRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        selectsScene.gameObject.SetActive(true);

        List<Quest> quests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in quests)
        {
            quest.DeActivateQuestCard();
        }

        for (int i = 0; i < quests.Count; i++)
        {
            RectTransform questCanvas = quests[i].GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

            questCanvas.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            questCanvas.localEulerAngles = new Vector3(-2f, -90f, -2f);

            questCanvas.DOKill();
            questCanvas.DOScale(Vector3.one, 0.2f);
            questCanvas.DORotate(Vector3.zero, 0.2f);

            yield return new WaitForSeconds(0.2f);
        }

        foreach (var quest in quests)
        {
            quest.ActivateQuestCard();
        }
    }



    // CONTENTS //

    private IEnumerator SetContentsRoutine(QuestSO _questData)
    {
        yield return new WaitForSeconds(0.2f);
        contentsScene.gameObject.SetActive(true);

        for (int i = 0; i < _questData.questContents.Count; i++)
        {
            GameObject content = Instantiate(_questData.questContents[i].contentTemplate);
            content.transform.SetParent(contentsScene, false);
            content.name = $"content_({i + 1})";
            content.GetComponent<IContent>().SetContentComponents(_questData, _questData.questContents[i]);
            content.GetComponent<IContent>().FlipOnContent();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator ResetContentsRoutine()
    {
        List<IContent> contents = new List<IContent>(contentsScene.GetComponentsInChildren<IContent>());
        for (int i = 0; i < contents.Count; i++)
        {
            contents[i].FlipOffContent();
            yield return new WaitForSeconds(0.2f);
        }

        foreach (IContent content in contents)
        {
            content.DestroyContent();
        }

        contentsScene.gameObject.SetActive(false);
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

        rollDicePanel.gameObject.SetActive(true);

        rollDicePanel.GetComponent<CanvasGroup>().alpha = 0f;
        rollDicePanel.GetComponent<RectTransform>().transform.localPosition = new Vector3(0f, -120f, 0f);
        rollDicePanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), rollDicePanelFadeTime, false).SetEase(Ease.OutCubic);
        rollDicePanel.GetComponent<CanvasGroup>().DOFade(1, rollDicePanelFadeTime);
    }

    private void ResetRollDicePanel()
    {
        rollDicePanel.GetComponent<CanvasGroup>().alpha = 1f;
        rollDicePanel.GetComponent<RectTransform>().localPosition = Vector3.zero;
        rollDicePanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, -120f), rollDicePanelFadeTime, false).SetEase(Ease.InCubic);
        rollDicePanel.GetComponent<CanvasGroup>().DOFade(0, rollDicePanelFadeTime).OnComplete(() =>
        {
            foreach (Transform child in rollDicePanel)
            {
                RollDice rollDice = child.GetComponent<RollDice>();
                if (rollDice != null)
                {
                    rollDice.DestroyRollDice();
                }
            }

            rollDicePanel.gameObject.SetActive(false);
        });
    }
}
