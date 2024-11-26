using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class SkillManager : Singleton<SkillManager>
{
    [Header("Points")]
    public StatusValue staminaPoint;
    public StatusValue signaturePoint;
    public int maxStamina = 6;
    public int maxSignature = 6;

    [Header("Transition")]
    [SerializeField] private CanvasGroup playerPanel;
    [SerializeField] private CanvasGroup adventurePanel;
    [SerializeField] private RectTransform skillFadeFilter;
    [SerializeField] private RectTransform skillPanel;
    private bool isFaded;

    [Header("Skill Check")]
    [SerializeField] private SkillSO currentSkill;
    [SerializeField] private int goalSelection;
    [SerializeField] private int currentSelection;

    [SerializeField] private PlayerDiceSO playerDices;
    [SerializeField] private RectTransform rollDicePanel;

    public List<RollDice> rollDices;
    public List<RollDice> clickableDices;
    public List<RollDice> nonClickalbeDices;
    public List<RollDice> castDices;



    // MANAGEMENT //

    private void Start()
    {
        GameManager.Instance.OnStagePhaseChanged += HandleStagePhaseChange;

        staminaPoint.Value = 0;
        signaturePoint.Value = 0;
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
        currentSelection = 0;

        if (currentSkill.costType == SkillCostType.SingleDiceCost)
        {
            goalSelection = 1;

            rollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());

            foreach (var dice in rollDices)
            {
                if (dice.IsAboveValue(currentSkill.diceCostSets[0].diceTypes, currentSkill.diceCostSets[0].aboveConditionValue))
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
                dice.SkillCostCheck();
            }
            foreach (var dice in clickableDices)
            {
                dice.SkillActivate();
            }
            foreach (var dice in nonClickalbeDices)
            {
                dice.SkillDeActivate();
            }

            FadeInScene();
        }
        else if (currentSkill.costType == SkillCostType.StaminaPointCost)
        {
            if (currentSkill.costValue == -1)
            {
                // # => # Logic
            }

            if (staminaPoint.Value < currentSkill.costValue)
            {
                CancelSkill();
                return;
            }
            else
            {
                staminaPoint.RemoveClampedValue(currentSkill.costValue, 0, maxStamina);
                ConfirmSkill();
            }
        }
        else if (currentSkill.costType == SkillCostType.SignaturePointCost)
        {
            if (currentSkill.costValue == -1)
            {
                // # => # Logic
            }

            if (signaturePoint.Value < currentSkill.costValue)
            {
                CancelSkill();
                return;
            }
            else
            {
                signaturePoint.RemoveClampedValue(currentSkill.costValue, 0, maxSignature);
                ConfirmSkill();
            }
        }
    }

    public void SkillCostCount(int costCount)
    {
        currentSelection += costCount;
        CheckCostCount();
    }

    private void CheckCostCount()
    {
        if (currentSelection >= goalSelection)
        {
            goalSelection = 0;
            currentSelection = 0;
            ConfirmSkill();
        }
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
            CancelSkill();
        }
        if (currentSkill.castType == SkillCastType.FixedDice)
        {
            CastFixedDice();
            CancelSkill();
        }
        if (currentSkill.castType == SkillCastType.ReRoll)
        {
            SkillCastPhase();
        }
        if (currentSkill.castType == SkillCastType.Modify)
        {
            SkillCastPhase();
        }
        if (currentSkill.castType == SkillCastType.StaminaPoint)
        {
            staminaPoint.AddClampedValue(currentSkill.castValue, 0, maxStamina);
            CancelSkill();
        }
        if (currentSkill.castType == SkillCastType.SignaturePoint)
        {
            signaturePoint.AddClampedValue(currentSkill.castValue, 0, maxSignature);
            CancelSkill();
        }
        if (currentSkill.castType == SkillCastType.Effect)
        {

        }

        if (currentSkill.skillCooldownType != SkillCooldownType.None)
        {
            currentSkill.isCooldown = true;
        }
    }

    public void SkillCastPhase()
    {
        currentSelection = 0;
        goalSelection = currentSkill.castValue;

        rollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());

        foreach (var dice in rollDices)
        {
            if (dice.IsSameType(currentSkill.castDiceType))
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
            dice.SkillCastCheck();
        }
        foreach (var dice in clickableDices)
        {
            dice.SkillActivate();
        }
        foreach (var dice in nonClickalbeDices)
        {
            dice.SkillDeActivate();
        }

        if (isFaded)
        {

        }
        else
        {
            FadeInScene();
        }
    }

    public void SkillCastCount(int castCount, RollDice castRollDice)
    {
        if (castCount > 0)
        {
            currentSelection += castCount;
            castDices.Add(castRollDice);
        }
        else
        {
            currentSelection -= castCount;
            castDices.Remove(castRollDice);
        }

        CheckCastCount();
    }

    private void CheckCastCount()
    {
        if (currentSelection >= goalSelection)
        {
            goalSelection = 0;
            currentSelection = 0;
            if (currentSkill.castType == SkillCastType.ReRoll)
            {
                CastReRoll();
            }
            if (currentSkill.castType == SkillCastType.Modify)
            {
                CastModify();
            }
        }
    }

    public void CastNewDice()
    {
        List<GameObject> dicePrefabs = new List<GameObject>();

        for (int i = 0; i < currentSkill.diceCastSets.Count; i++)
        {
            switch (currentSkill.diceCastSets[i].diceType)
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

        for (int i = 0; i < currentSkill.diceCastSets.Count; i++)
        {
            switch (currentSkill.diceCastSets[i].diceType)
            {
                case DiceType.Strength:
                    GameObject strDicePrefab = Instantiate(playerDices.StrDice_Roll);
                    strDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(strDicePrefab);

                    strDicePrefab.GetComponent<RollDice>().SkillFix(currentSkill.diceCastSets[i].diceValue);

                    break;
                case DiceType.Agility:
                    GameObject agiDicePrefab = Instantiate(playerDices.AgiDice_Roll);
                    agiDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(agiDicePrefab);

                    agiDicePrefab.GetComponent<RollDice>().SkillFix(currentSkill.diceCastSets[i].diceValue);

                    break;
                case DiceType.Intelligence:
                    GameObject intDicePrefab = Instantiate(playerDices.IntDice_Roll);
                    intDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(intDicePrefab);

                    intDicePrefab.GetComponent<RollDice>().SkillFix(currentSkill.diceCastSets[i].diceValue);

                    break;
                case DiceType.Willpower:
                    GameObject wilDicePrefab = Instantiate(playerDices.WilDice_Roll);
                    wilDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(wilDicePrefab);

                    wilDicePrefab.GetComponent<RollDice>().SkillFix(currentSkill.diceCastSets[i].diceValue);

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

    public void CastReRoll()
    {
        foreach (RollDice dice in castDices)
        {
            // ReRoll
        }

        CancelSkill();
    }

    public void CastModify()
    {
        foreach (RollDice dice in castDices)
        {
            // Modify(int)
        }

        CancelSkill();
    }



    // CANCEL //

    public void CancelSkill()
    {
        currentSkill = null;
        goalSelection = 0;
        currentSelection = 0;

        rollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());
        foreach (var dice in rollDices)
        {
            dice.SkillCancel();
        }

        rollDices.Clear();
        clickableDices.Clear();
        nonClickalbeDices.Clear();

        if (isFaded)
        {
            FadeOutScene();
        }
    }



    // TRANSITIONS //

    private void FadeInScene()
    {
        isFaded = true;

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
        isFaded = false;

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
    }
}