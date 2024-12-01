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

        questIndexes[0].FlipOnQuest();
    }



    // StagePhase Management //

    private void HandleStagePhaseChange(StagePhase stagePhase)
    {
        if (stagePhase == StagePhase.DiceUsing)
        {
            InfoTabController.Instance.SetActiveTab(InfoTab.SkillTab);
        }
        if (stagePhase == StagePhase.DiceHolding)
        {
            CancelButton cancelButton = GetComponentInChildren<CancelButton>();
            cancelButton.DeActivateTarget();

            ResetRollDicePanel();

            enemyTurnScroll.gameObject.SetActive(true);
            AudioManager.Instance.PlaySfx("EnemyTurn");

        }
        if (stagePhase == StagePhase.DiceWaiting)
        {
            CancelButton cancelButton = GetComponentInChildren<CancelButton>();
            cancelButton.WaitAndActivateTarget();

            SetRollDicePanel();

            playerTurnScroll.gameObject.SetActive(true);
            AudioManager.Instance.PlaySfx("PlayerTurn");

        }
        if (stagePhase == StagePhase.Finishing)
        {
            CancelButton cancelButton = GetComponentInChildren<CancelButton>();
            cancelButton.DeActivateTarget();

            ResetRollDicePanel();

            completeScroll.gameObject.SetActive(true);
            AudioManager.Instance.PlaySfx("QuestComplete");

            if (GetComponentInChildren<CombatImageContent>() != null)
            {
                GetComponentInChildren<CombatDescriptionContent>().FlipOffContent();
                AudioManager.Instance.PlaySfxWithPitch("CardFlip");
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
                AudioManager.Instance.PlaySfxWithPitch("CardFlip");
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
        if (stagePhase == StagePhase.None)
        {
            AudioManager.Instance.EndCombatBgm();
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
        AudioManager.Instance.PlaySfx("Page");

        yield return new WaitForSeconds(1f);

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
                AudioManager.Instance.PlaySfxWithPitch("CardFlip");
                content.GetComponent<IContent>().FlipOnContent();
            }
            yield return new WaitForSeconds(0.25f);
        }

        GameManager.Instance.UpdateStagePhase(StagePhase.Beginning);
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
        AudioManager.Instance.PlaySfx("QuestFailed");
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

        StartCoroutine(ResetContentsRoutine());
        yield return StartCoroutine(ActivateSelectsRoutine());

        if (currentQuestIndex != -1)
        {
            if (currentQuestIndex == 8)
            {
                foreach (var quest in questIndexes)
                {
                    quest.DeleteQuest();
                }

                yield return null;
            }
            else
            {
                List<int> nearbyQuests = GetNearbyQuest(currentQuestIndex);
                if (nearbyQuests != null)
                {
                    foreach (int index in nearbyQuests)
                    {
                        questIndexes[index].FlipOnQuest();
                    }
                }

                yield return new WaitForSeconds(0.75f);
            }
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
        AudioManager.Instance.PlaySfx("Page");
        contentsScene.gameObject.SetActive(false);
    }

    private IEnumerator ActivateSelectsRoutine()
    {
        yield return new WaitForSeconds(0.6f);
        selectsScene.gameObject.SetActive(true);

        if (currentQuestIndex != -1)
        {
            questIndexes[currentQuestIndex].ResolvedQuest();
        }

        List<Quest> quests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in quests)
        {
            quest.DeActivateQuestCard();
        }

        yield return new WaitForSeconds(1f);

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
                return new List<int> { 2, 4 };
            case 2:
                return new List<int> { 5 };
            case 3:
                return new List<int> { 4, 6 };
            case 4:
                return new List<int> { 5, 7 };
            case 5:
                return new List<int> { 8 };
            case 6:
                return new List<int> { 7 };
            case 7:
                return new List<int> { 8 };
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
        int StrDiceAmount = playerDices.StrDice.Value;
        int AgiDiceAmount = playerDices.AgiDice.Value;
        int IntDiceAmount = playerDices.IntDice.Value;
        int WilDiceAmount = playerDices.WilDice.Value;

        List<GameObject> dicePrefabs = new List<GameObject>();

        for (int i = 0; i < StrDiceAmount; i++)
        {
            GameObject StrDicePrefab = Instantiate(playerDices.StrDice_Roll);
            StrDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(StrDicePrefab);
        }
        for (int i = 0; i < AgiDiceAmount; i++)
        {
            GameObject AgiDicePrefab = Instantiate(playerDices.AgiDice_Roll);
            AgiDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(AgiDicePrefab);
        }

        for (int i = 0; i < IntDiceAmount; i++)
        {
            GameObject IntDicePrefab = Instantiate(playerDices.IntDice_Roll);
            IntDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(IntDicePrefab);
        }

        for (int i = 0; i < WilDiceAmount; i++)
        {
            GameObject WilDicePrefab = Instantiate(playerDices.WilDice_Roll);
            WilDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(WilDicePrefab);
        }

        int rowCount = Mathf.CeilToInt(dicePrefabs.Count / 14f);
        int rowHeight = 96;
        int baseYPosition = -464 + ((rowCount - 1) * rowHeight);

        for (int i = 0; i < dicePrefabs.Count; i++)
        {
            int currentRow = i / 14;
            int indexInRow = i % 14;

            float xPosition = -358 + (indexInRow * 96);
            float yPosition;

            if (dicePrefabs.Count <= 14)
            {
                yPosition = -424;
            }
            else
            {
                yPosition = baseYPosition - (currentRow * rowHeight);
            }

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
