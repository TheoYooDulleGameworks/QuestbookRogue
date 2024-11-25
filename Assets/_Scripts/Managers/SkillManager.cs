using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class SkillManager : Singleton<SkillManager>
{
    [Header("Transition")]
    [SerializeField] private CanvasGroup playerPanel;
    [SerializeField] private CanvasGroup adventurePanel;
    [SerializeField] private RectTransform skillFadeFilter;
    [SerializeField] private RectTransform skillPanel;
    [SerializeField] private RectTransform skillConfirmButton;

    [Header("Skill Check")]
    // [SerializeField] private int staminaPoint = 0;
    // [SerializeField] private int signaturePoint = 0;
    [SerializeField] private RectTransform rollDicePanel;

    [Header("Dataset")]
    [SerializeField] private PlayerDiceSO playerDices;

    public List<RollDice> rollDices;
    public List<RollDice> clickableDices;
    public List<RollDice> nonClickalbeDices;
    private SkillSO currentSkill;
    private int goalCost;
    private int currentCost;



    // MANAGEMENT //

    private void Start()
    {
        GameManager.Instance.OnStagePhaseChanged += HandleStagePhaseChange;
    }

    private void HandleStagePhaseChange(StagePhase stagePhase)
    {
        if (stagePhase == StagePhase.DiceUsing)
        {

        }
    }



    // COST CHECK //

    public void SkillCostPhase(SkillSO skillData)
    {
        currentSkill = skillData;
        currentCost = 0;

        if (currentSkill.costType == SkillCostType.SingleDiceCost)
        {
            goalCost = 1;

            rollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());

            foreach (var dice in rollDices)
            {
                if (dice.IsAboveValue(currentSkill.singleDiceTypes, currentSkill.aboveConditionValue))
                {
                    clickableDices.Add(dice);
                }
                else
                {
                    nonClickalbeDices.Add(dice);
                }
            }

            foreach (var dice in rollDices)
            {
                dice.SkillCheck();
            }
            foreach (var dice in clickableDices)
            {
                dice.SkillActivate();
            }
            foreach (var dice in nonClickalbeDices)
            {
                dice.SkillExcept();
            }

            FadeInScene();
        }
    }

    public void SkillCostCount(int costAmount, RectTransform rectTransform)
    {
        currentCost += costAmount;
        CheckCostCount(rectTransform);
    }

    private void CheckCostCount(RectTransform rectTransform)
    {
        if (currentCost >= goalCost)
        {
            CostReady(rectTransform);
        }
        else
        {
            CostNoReady();
        }
    }

    private void CostReady(RectTransform rectTransform)
    {
        foreach (var dice in clickableDices)
        {
            dice.SkillDeActivate();
        }

        skillConfirmButton.gameObject.SetActive(true);

        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            skillConfirmButton.parent as RectTransform,
            rectTransform.position,
            null,
            out localPosition);

        skillConfirmButton.anchoredPosition = localPosition + new Vector2(64, 64);

        skillConfirmButton.GetComponent<CanvasGroup>().alpha = 0;
        skillConfirmButton.GetComponent<CanvasGroup>().DOFade(1, 0.25f).OnComplete(() =>
        {
            skillConfirmButton.GetComponent<SkillConfirmButton>().ActiavteConfirmButton();
        });
    }

    private void CostNoReady()
    {
        foreach (var dice in clickableDices)
        {
            dice.SkillActivate();
        }

        skillConfirmButton.gameObject.SetActive(false);
    }



    // CAST CHECK //

    public void ConfirmSkill()
    {
        if (currentSkill.costType == SkillCostType.SingleDiceCost)
        {
            foreach (var dice in rollDices)
            {
                dice.SkillSpend();
            }
        }

        if (currentSkill.castType == SkillCastType.NewDice)
        {
            CastNewDice();
        }
        if (currentSkill.castType == SkillCastType.FixedDice)
        {
            CastFixedDice();
        }
        if (currentSkill.castType == SkillCastType.ReRoll)
        {

        }
        if (currentSkill.castType == SkillCastType.Modify)
        {

        }
        if (currentSkill.castType == SkillCastType.StaminaPoint)
        {

        }
        if (currentSkill.castType == SkillCastType.SignaturePoint)
        {

        }
        if (currentSkill.castType == SkillCastType.Effect)
        {

        }

        currentSkill.isCooldown = true;
        CancelSkill();
    }

    public void CastNewDice()
    {
        List<GameObject> dicePrefabs = new List<GameObject>();

        for (int i = 0; i < currentSkill.newDiceSets.Count; i++)
        {
            switch (currentSkill.newDiceSets[i])
            {
                case DiceType.Strength:
                    GameObject strDicePrefab = Instantiate(playerDices.StrDice_Roll);
                    strDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(strDicePrefab);
                    break;
                case DiceType.Agility:
                    GameObject agiDicePrefab = Instantiate(playerDices.AgiDice_Roll);
                    agiDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(agiDicePrefab);
                    break;
                case DiceType.Intelligence:
                    GameObject intDicePrefab = Instantiate(playerDices.IntDice_Roll);
                    intDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(intDicePrefab);
                    break;
                case DiceType.Willpower:
                    GameObject wilDicePrefab = Instantiate(playerDices.WilDice_Roll);
                    wilDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(wilDicePrefab);
                    break;
            }
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

        foreach (var dice in dicePrefabs)
        {
            dice.GetComponent<Image>().raycastTarget = true;
        }
        dicePrefabs.Clear();
    }

    public void CastFixedDice()
    {
        List<GameObject> dicePrefabs = new List<GameObject>();

        for (int i = 0; i < currentSkill.fixedDiceSets.Count; i++)
        {
            switch (currentSkill.fixedDiceSets[i].fixedDiceType)
            {
                case DiceType.Strength:
                    GameObject strDicePrefab = Instantiate(playerDices.StrDice_Roll);
                    strDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(strDicePrefab);

                    strDicePrefab.GetComponent<RollDice>().SkillFix(currentSkill.fixedDiceSets[i].fixedDiceValue);

                    break;
                case DiceType.Agility:
                    GameObject agiDicePrefab = Instantiate(playerDices.AgiDice_Roll);
                    agiDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(agiDicePrefab);

                    agiDicePrefab.GetComponent<RollDice>().SkillFix(currentSkill.fixedDiceSets[i].fixedDiceValue);

                    break;
                case DiceType.Intelligence:
                    GameObject intDicePrefab = Instantiate(playerDices.IntDice_Roll);
                    intDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(intDicePrefab);

                    intDicePrefab.GetComponent<RollDice>().SkillFix(currentSkill.fixedDiceSets[i].fixedDiceValue);

                    break;
                case DiceType.Willpower:
                    GameObject wilDicePrefab = Instantiate(playerDices.WilDice_Roll);
                    wilDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(wilDicePrefab);

                    wilDicePrefab.GetComponent<RollDice>().SkillFix(currentSkill.fixedDiceSets[i].fixedDiceValue);

                    break;
            }
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

        foreach (var dice in dicePrefabs)
        {
            dice.GetComponent<Image>().raycastTarget = true;
        }
        dicePrefabs.Clear();
    }



    // CANCEL //

    public void CancelSkill()
    {
        currentSkill = null;
        goalCost = 0;
        currentCost = 0;

        rollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());
        foreach (var dice in rollDices)
        {
            dice.SkillCancel();
        }

        rollDices.Clear();
        clickableDices.Clear();
        nonClickalbeDices.Clear();

        FadeOutScene();
    }



    // TRANSITIONS //

    private void FadeInScene()
    {
        playerPanel.blocksRaycasts = false;
        adventurePanel.blocksRaycasts = false;
        skillPanel.gameObject.SetActive(true);

        skillPanel.GetComponent<CanvasGroup>().DOKill();
        skillFadeFilter.GetComponent<CanvasGroup>().DOKill();

        skillPanel.GetComponent<CanvasGroup>().alpha = 0f;
        skillFadeFilter.GetComponent<CanvasGroup>().alpha = 0f;

        skillFadeFilter.GetComponent<CanvasGroup>().DOFade(0.65f, 0.25f);
        skillPanel.GetComponent<CanvasGroup>().DOFade(1f, 0.25f).OnComplete(() =>
        {
            skillPanel.GetComponentInChildren<SkillCancelButton>().ActiavteCancelButton();
        });
    }

    private void FadeOutScene()
    {
        skillFadeFilter.GetComponent<CanvasGroup>().DOKill();
        skillPanel.GetComponent<CanvasGroup>().DOKill();

        skillFadeFilter.GetComponent<CanvasGroup>().alpha = 0.65f;
        skillPanel.GetComponent<CanvasGroup>().alpha = 1f;

        skillPanel.GetComponent<CanvasGroup>().DOFade(0f, 0.25f);
        skillFadeFilter.GetComponent<CanvasGroup>().DOFade(0f, 0.25f).OnComplete(() =>
        {
            skillPanel.gameObject.SetActive(false);
            playerPanel.blocksRaycasts = true;
            adventurePanel.blocksRaycasts = true;
        });

        if (skillConfirmButton.gameObject.activeSelf)
        {
            skillConfirmButton.GetComponent<CanvasGroup>().DOKill();
            skillConfirmButton.GetComponent<CanvasGroup>().alpha = 1;
            skillConfirmButton.GetComponent<CanvasGroup>().DOFade(0, 0.25f).OnComplete(() =>
            {
                skillConfirmButton.gameObject.SetActive(false);
            });
        }
    }
}