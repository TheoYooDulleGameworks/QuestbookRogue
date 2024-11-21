using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class SceneController : Singleton<SceneController>
{
    [Header("Playing Data")]
    [SerializeField] private int currentQuestIndex = -1;

    [Header("Components")]
    [SerializeField] private RectTransform selectsScene;
    [SerializeField] private RectTransform contentsScene;
    [SerializeField] private RectTransform rollDicePanel;

    [Header("Scrolls")]
    [SerializeField] private RectTransform completeScroll;
    [SerializeField] private RectTransform failedScroll;
    [SerializeField] private RectTransform playerTurnScroll;
    [SerializeField] private RectTransform enemyTurnScroll;

    [Header("Transitions")]
    [SerializeField] private RectTransform pageTransition;
    [SerializeField] private RectTransform pageTransitionBack;

    [Header("Player Assets")]
    [SerializeField] private PlayerPathSO playerPaths;
    [SerializeField] private PlayerDiceSO playerDices;

    [Header("Quests Management")]
    [SerializeField] public List<Quest> questIndexes;

    [Header("Sources")]
    [SerializeField] private GameObject questCardPrefab;



    // SETTINGS //

    private void Start()
    {
        GameManager.Instance.OnStagePhaseChanged += HandleStagePhaseChange;
    }

    public void InitiateQuestScene()
    {
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

        StartCoroutine(FirstSetSelectsRoutine());
    }

    private IEnumerator FirstSetSelectsRoutine()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject questCard = Instantiate(questCardPrefab);
            questCard.transform.SetParent(selectsScene, false);
            questIndexes.Add(questCard.GetComponent<Quest>());
            questCard.GetComponent<Quest>().InitiateQuestCard(playerPaths.PickRandomQuestData(), i);

            questCard.GetComponent<Quest>().GenerateQuest();

            yield return new WaitForSeconds(0.25f);
        }

        StartCoroutine(FirstSetQuestRoutine());
    }

    private IEnumerator FirstSetQuestRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 3; i++)
        {
            questIndexes[i].FlipOnQuest();

            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.3f); // 0.25s + additional 0.05s +

        List<Quest> quests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in quests)
        {
            quest.ActivateQuestCard();
        }
    }



    // StagePhase Management //

    private void HandleStagePhaseChange(StagePhase stagePhase)
    {
        if (stagePhase == StagePhase.DiceHolding)
        {
            ResetRollDicePanel();

            enemyTurnScroll.gameObject.SetActive(true);
        }
        if (stagePhase == StagePhase.DiceWaiting)
        {
            SetRollDicePanel();

            playerTurnScroll.gameObject.SetActive(true);
        }
        if (stagePhase == StagePhase.Finishing)
        {
            ResetRollDicePanel();

            completeScroll.gameObject.SetActive(true);

            if (GetComponentInChildren<CombatImageContent>() != null)
            {
                GetComponentInChildren<CombatDescriptionContent>().FlipOffContent();
                List<CombatActionContent> combatActions = new List<CombatActionContent>(GetComponentsInChildren<CombatActionContent>());
                foreach (var action in combatActions)
                {
                    action.FlipOffContent();
                }
                List<BlankContent> balnks = new List<BlankContent>(GetComponentsInChildren<BlankContent>());
                foreach (var blank in balnks)
                {
                    blank.FlipOffContent();
                }

                GetComponentInChildren<CombatImageContent>().SetRewards();
            }

            if (GetComponentInChildren<ImageContent>() != null)
            {
                GetComponentInChildren<DescriptionContent>().FlipOffContent();
                List<ActionContent> combatActions = new List<ActionContent>(GetComponentsInChildren<ActionContent>());
                foreach (var action in combatActions)
                {
                    action.FlipOffContent();
                }
                List<BlankContent> balnks = new List<BlankContent>(GetComponentsInChildren<BlankContent>());
                foreach (var blank in balnks)
                {
                    blank.FlipOffContent();
                }

                GetComponentInChildren<ImageContent>().SetRewards();
            }
        }
    }



    // Selects -> CONTENTS //

    public void DeActivateSelects()
    {
        List<Quest> quests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in quests)
        {
            quest.DeActivateQuestCard();
        }
    }

    public void TransitionToContents(QuestSO _questData, int _questIndexNumber)
    {
        currentQuestIndex = _questIndexNumber;
        StartCoroutine(TransitionToContentsRoutine(_questData));
    }

    private IEnumerator TransitionToContentsRoutine(QuestSO _questData)
    {
        StartCoroutine(DeActivateSelectsRoutine());
        yield return StartCoroutine(SetContentsRoutine(_questData));

        yield return StartCoroutine(ActivateRollDicePanel());
        InfoTabController.Instance.HandleSkillTabActivate();

        yield return new WaitForSeconds(0.1f);

        List<CancelButton> cancelButtons = new List<CancelButton>(contentsScene.GetComponentsInChildren<CancelButton>());
        foreach (var button in cancelButtons)
        {
            button.ActivateTarget();
        }

        ActivateRollDices();
    }

    private IEnumerator DeActivateSelectsRoutine()
    {
        pageTransition.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.02f);

        selectsScene.gameObject.SetActive(false);
    }

    private IEnumerator SetContentsRoutine(QuestSO _questData)
    {
        yield return new WaitForSeconds(1.5f);
        contentsScene.gameObject.SetActive(true);

        for (int i = 0; i < _questData.questContents.Count; i++)
        {
            GameObject content = Instantiate(_questData.questContents[i].contentTemplate);
            content.transform.SetParent(contentsScene, false);
            content.name = $"content_({i + 1})";
            content.GetComponent<IContent>().SetContentComponents(_questData, _questData.questContents[i]);
            if (content.GetComponent<BlankContent>())
            {
                continue;
            }
            else
            {
                content.GetComponent<IContent>().FlipOnContent();
            }
            yield return new WaitForSeconds(0.25f);
        }
    }


    // PREVENT CHEATING //

    public void DontRefundDices()
    {
        List<DiceSlot> diceSlots = new List<DiceSlot>(GetComponentsInChildren<DiceSlot>());
        foreach (DiceSlot diceSlot in diceSlots)
        {
            diceSlot.QuestIsOver = true;
        }
    }

    public void NotPaySlotRefund()
    {
        List<PaySlot> notThisPaySlots = new List<PaySlot>(GetComponentsInChildren<PaySlot>());
        foreach (PaySlot paySlot in notThisPaySlots)
        {
            paySlot.ProceedNotThisPayment();
        }
    }



    // FAILED QUEST //

    public void FailedQuest()
    {
        failedScroll.gameObject.SetActive(true);
    }



    // Contents -> SELECTS //

    public void TransitionToSelects(QuestSO _questData)
    {
        StartCoroutine(TransitionToSelectsRoutine(_questData));
    }

    private IEnumerator TransitionToSelectsRoutine(QuestSO _questData)
    {
        DeActivateRollDices();

        yield return StartCoroutine(DeActivateRollDicePanel());

        InfoTabController.Instance.HandleDiceTabActivate();

        StartCoroutine(ResetContentsRoutine());
        yield return StartCoroutine(ActivateSelectsRoutine());

        if (currentQuestIndex != -1)
        {
            questIndexes[currentQuestIndex].FlipOffQuest();

            yield return new WaitForSeconds(0.5f);

            if (currentQuestIndex == 6 || currentQuestIndex == 7 || currentQuestIndex == 8)
            {
                for (int j = 0; j < 3; j++)
                {
                    questIndexes[j].DeleteQuest();
                }

                for (int k = 3; k < 9; k++)
                {
                    questIndexes[k].MoveUpQuest();
                }


                for (int l = 0; l < 3; l++)
                {
                    GameObject questCard = Instantiate(questCardPrefab);
                    questCard.transform.SetParent(selectsScene, false);
                    questIndexes.Add(questCard.GetComponent<Quest>());
                    questCard.GetComponent<Quest>().InitiateQuestCard(playerPaths.PickRandomQuestData(), l + 6);

                    questCard.GetComponent<Quest>().MoveUpGenerateQuest();
                }

                questIndexes.RemoveRange(0, 3);
                currentQuestIndex -= 3;

                yield return new WaitForSeconds(0.5f);
            }

            List<int> nearbyQuests = GetNearbyQuest(currentQuestIndex);
            if (nearbyQuests != null)
            {
                foreach (int index in nearbyQuests)
                {
                    questIndexes[index].FlipOnQuest();
                }
            }

            yield return new WaitForSeconds(0.5f);

        }
        else
        {
            Debug.LogWarning("Quest Index Error!");
        }

        currentQuestIndex = -1;
        List<Quest> quests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in quests)
        {
            quest.ActivateQuestCard();
        }
    }

    private IEnumerator ResetContentsRoutine()
    {
        List<IContent> contents = new List<IContent>(contentsScene.GetComponentsInChildren<IContent>());
        for (int i = 0; i < contents.Count; i++)
        {
            contents[i].FlipOffContent();
        }

        yield return new WaitForSeconds(0.5f);

        pageTransitionBack.gameObject.SetActive(true);
        contentsScene.gameObject.SetActive(false);
    }

    private IEnumerator ActivateSelectsRoutine()
    {
        yield return new WaitForSeconds(0.6f);
        selectsScene.gameObject.SetActive(true);

        List<Quest> quests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in quests)
        {
            quest.DeActivateQuestCard();
        }

        yield return new WaitForSeconds(1.03f);

        foreach (var quest in quests)
        {
            quest.ActivateQuestCard();
        }
    }

    private List<int> GetNearbyQuest(int questIndex)
    {
        switch (questIndex)
        {
            case 0:
                return new List<int> { 1, 3 };
            case 1:
                return new List<int> { 0, 2, 4 };
            case 2:
                return new List<int> { 1, 5 };
            case 3:
                return new List<int> { 0, 4, 6 };
            case 4:
                return new List<int> { 1, 3, 5, 7 };
            case 5:
                return new List<int> { 2, 4, 8 };
            case 6:
                return new List<int> { 3, 7 };
            case 7:
                return new List<int> { 4, 6, 8 };
            case 8:
                return new List<int> { 5, 7 };
            default:
                return null;
        }
    }



    // ROLL DICE PANEL //

    private IEnumerator ActivateRollDicePanel()
    {
        SetRollDicePanel();

        yield return null;
    }

    private IEnumerator DeActivateRollDicePanel()
    {
        ResetRollDicePanel();

        yield return null;
    }

    public void SetRollDicePanel()
    {
        int StrAdvancedDiceAmount = playerDices.StrAdvancedDice.Value;
        int DexAdvancedDiceAmount = playerDices.DexAdvancedDice.Value;
        int IntAdvancedDiceAmount = playerDices.IntAdvancedDice.Value;
        int WilAdvancedDiceAmount = playerDices.WilAdvancedDice.Value;

        int StrNormalDiceAmount = playerDices.StrNormalDice.Value;
        int DexNormalDiceAmount = playerDices.DexNormalDice.Value;
        int IntNormalDiceAmount = playerDices.IntNormalDice.Value;
        int WilNormalDiceAmount = playerDices.WilNormalDice.Value;

        List<GameObject> dicePrefabs = new List<GameObject>();

        for (int i = 0; i < StrAdvancedDiceAmount; i++)
        {
            GameObject StrAdvancedDicePrefab = Instantiate(playerDices.StrAdvancedDice_Roll);
            StrAdvancedDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(StrAdvancedDicePrefab);
        }

        for (int i = 0; i < StrNormalDiceAmount; i++)
        {
            GameObject StrNormalDicePrefab = Instantiate(playerDices.StrNormalDice_Roll);
            StrNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(StrNormalDicePrefab);
        }

        for (int i = 0; i < DexAdvancedDiceAmount; i++)
        {
            GameObject DexAdvancedDicePrefab = Instantiate(playerDices.DexAdvancedDice_Roll);
            DexAdvancedDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(DexAdvancedDicePrefab);
        }

        for (int i = 0; i < DexNormalDiceAmount; i++)
        {
            GameObject DexNormalDicePrefab = Instantiate(playerDices.DexNormalDice_Roll);
            DexNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(DexNormalDicePrefab);
        }

        for (int i = 0; i < IntAdvancedDiceAmount; i++)
        {
            GameObject IntAdvancedDicePrefab = Instantiate(playerDices.IntAdvancedDice_Roll);
            IntAdvancedDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(IntAdvancedDicePrefab);
        }

        for (int i = 0; i < IntNormalDiceAmount; i++)
        {
            GameObject IntNormalDicePrefab = Instantiate(playerDices.IntNormalDice_Roll);
            IntNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(IntNormalDicePrefab);
        }

        for (int i = 0; i < WilAdvancedDiceAmount; i++)
        {
            GameObject WilAdvancedDicePrefab = Instantiate(playerDices.WilAdvancedDice_Roll);
            WilAdvancedDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(WilAdvancedDicePrefab);
        }

        for (int i = 0; i < WilNormalDiceAmount; i++)
        {
            GameObject WilNormalDicePrefab = Instantiate(playerDices.WilNormalDice_Roll);
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
        rollDicePanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), 0.5f, false);
        rollDicePanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
    }

    public void ResetRollDicePanel()
    {
        rollDicePanel.GetComponent<CanvasGroup>().alpha = 1f;
        rollDicePanel.GetComponent<RectTransform>().localPosition = Vector3.zero;
        rollDicePanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, -120f), 0.5f, false).SetEase(Ease.InCubic);
        rollDicePanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() =>
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

    public void ActivateRollDices()
    {
        List<RollDice> dices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());
        foreach (var dice in dices)
        {
            dice.GetComponent<Image>().raycastTarget = true;
        }
    }

    public void DeActivateRollDices()
    {
        List<RollDice> dices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].GetComponent<Image>().raycastTarget = false;
        }
    }
}
