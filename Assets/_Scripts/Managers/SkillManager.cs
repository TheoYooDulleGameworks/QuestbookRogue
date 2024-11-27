using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

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
    [SerializeField] private RectTransform skillInfo;
    [SerializeField] private RectTransform skillConfirmButton;
    private bool isFaded;

    [Header("Skill Check")]
    [SerializeField] private SkillSO currentSkill;
    [SerializeField] private RectTransform currentSkillPosition;
    [SerializeField] private int goalSelection;
    [SerializeField] private int currentSelection;

    [SerializeField] private PlayerDiceSO playerDices;
    [SerializeField] private RectTransform rollDicePanel;

    public List<RollDice> rollDices;
    public List<RollDice> clickableDices;
    public List<RollDice> nonClickalbeDices;
    public List<RollDice> castDices;

    [SerializeField] private bool isRefundable = false;
    [SerializeField] private int refundValue = 0;
    [SerializeField] private List<RefundDiceData> refundDices;



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

    public void SkillCostPhase(SkillSO skillData, RectTransform skillPosition)
    {
        rollDices.Clear();
        clickableDices.Clear();
        nonClickalbeDices.Clear();
        castDices.Clear();

        currentSkill = skillData;
        currentSkillPosition = skillPosition;
        currentSelection = 0;

        if (currentSkill.costType == SkillCostType.DiceCost)
        {
            goalSelection = currentSkill.costValue;

            rollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());

            foreach (var dice in rollDices)
            {
                if (dice.IsAboveValue(currentSkill.costDiceTypes, currentSkill.aboveConditionValue))
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

            if (clickableDices.Count <= 0)
            {
                CancelSkill();
            }
            else
            {
                FadeInScene();
            }
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
                isRefundable = true;
                refundValue = currentSkill.costValue;
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
                isRefundable = true;
                refundValue = currentSkill.costValue;
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
        if (currentSkill.costType == SkillCostType.DiceCost)
        {
            foreach (var dice in rollDices)
            {
                dice.SkillSpend();
            }

            AudioManager.Instance.PlaySfx("DiceVanishing");

            isRefundable = true;
        }

        if (currentSkill.skillCooldownType != SkillCooldownType.None)
        {
            currentSkill.isCooldown = true;
        }

        if (currentSkill.castType == SkillCastType.NewDice)
        {
            CastNewDice();
        }
        else if (currentSkill.castType == SkillCastType.FixedDice)
        {
            CastFixedDice();
        }
        else if (currentSkill.castType == SkillCastType.ReRoll)
        {
            SkillCastPhase();
        }
        else if (currentSkill.castType == SkillCastType.Modify)
        {
            SkillCastPhase();
        }
        else if (currentSkill.castType == SkillCastType.StaminaPoint)
        {
            staminaPoint.AddClampedValue(currentSkill.castValue, 0, maxStamina);
            isRefundable = false;
            refundValue = 0;
            refundDices.Clear();
            CancelSkill();
        }
        else if (currentSkill.castType == SkillCastType.SignaturePoint)
        {
            signaturePoint.AddClampedValue(currentSkill.castValue, 0, maxSignature);
            isRefundable = false;
            refundValue = 0;
            refundDices.Clear();
            CancelSkill();
        }
        else if (currentSkill.castType == SkillCastType.Effect)
        {
            isRefundable = false;
            refundValue = 0;
            refundDices.Clear();
        }
    }

    public void SkillCastPhase()
    {
        rollDices.Clear();
        clickableDices.Clear();
        nonClickalbeDices.Clear();
        castDices.Clear();

        currentSelection = 0;
        goalSelection = currentSkill.castValue;

        rollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());

        if (currentSkill.castType == SkillCastType.ReRoll)
        {
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

            if (clickableDices.Count <= 0)
            {
                CancelSkill();
                return;
            }
        }
        else if (currentSkill.castType == SkillCastType.Modify)
        {
            foreach (var dice in rollDices)
            {
                if (dice.IsPossibleToModify(currentSkill.castDiceType))
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

            if (clickableDices.Count <= 0)
            {
                CancelSkill();
                return;
            }
        }

        if (isFaded)
        {
            skillPanel.GetComponentInChildren<TextMeshProUGUI>().text = currentSkill.castDescription;
            skillConfirmButton.gameObject.SetActive(true);
        }
        else
        {
            FadeInScene();
            skillPanel.GetComponentInChildren<TextMeshProUGUI>().text = currentSkill.castDescription;
            skillConfirmButton.gameObject.SetActive(true);
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
            currentSelection += castCount;
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

            CastSkill();
        }
        else if (currentSelection < goalSelection && currentSelection > 0)
        {
            skillConfirmButton.GetComponent<SkillConfirmButton>().ActivateButton();
        }
        else if (currentSelection <= 0)
        {
            skillConfirmButton.GetComponent<SkillConfirmButton>().DeActivateButton();
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
            dice.GetComponent<RollDice>().SkillGenerate();
        }

        AudioManager.Instance.PlaySfx("DiceGenerating");

        dicePrefabs.Clear();

        isRefundable = false;
        refundValue = 0;
        refundDices.Clear();

        CancelSkill();
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
            dice.GetComponent<RollDice>().SkillGenerate();
        }

        AudioManager.Instance.PlaySfx("DiceGenerating");

        dicePrefabs.Clear();

        isRefundable = false;
        refundValue = 0;
        refundDices.Clear();

        CancelSkill();
    }

    public void CastRefundDice()
    {
        List<GameObject> dicePrefabs = new List<GameObject>();

        for (int i = 0; i < refundDices.Count; i++)
        {
            switch (refundDices[i].diceType)
            {
                case DiceType.Strength:
                    GameObject strDicePrefab = Instantiate(playerDices.StrDice_Roll);
                    strDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(strDicePrefab);

                    strDicePrefab.GetComponent<RollDice>().SkillFix(refundDices[i].dieValue);

                    break;
                case DiceType.Agility:
                    GameObject agiDicePrefab = Instantiate(playerDices.AgiDice_Roll);
                    agiDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(agiDicePrefab);

                    agiDicePrefab.GetComponent<RollDice>().SkillFix(refundDices[i].dieValue);

                    break;
                case DiceType.Intelligence:
                    GameObject intDicePrefab = Instantiate(playerDices.IntDice_Roll);
                    intDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(intDicePrefab);

                    intDicePrefab.GetComponent<RollDice>().SkillFix(refundDices[i].dieValue);

                    break;
                case DiceType.Willpower:
                    GameObject wilDicePrefab = Instantiate(playerDices.WilDice_Roll);
                    wilDicePrefab.transform.SetParent(rollDicePanel, false);
                    dicePrefabs.Add(wilDicePrefab);

                    wilDicePrefab.GetComponent<RollDice>().SkillFix(refundDices[i].dieValue);

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
            dice.GetComponent<RollDice>().SkillGenerate();
        }

        AudioManager.Instance.PlaySfx("DiceGenerating");

        dicePrefabs.Clear();
    }

    public void CastSkill()
    {
        if (currentSkill.castType == SkillCastType.ReRoll)
        {
            CastReRoll();
        }
        else if (currentSkill.castType == SkillCastType.Modify)
        {
            CastModify();
        }
    }

    public void CastReRoll()
    {
        foreach (RollDice dice in castDices)
        {
            dice.SkillReRoll();
        }

        AudioManager.Instance.PlaySfx("DiceThrow");

        isRefundable = false;
        refundValue = 0;
        refundDices.Clear();

        CancelSkill();
    }

    public void CastModify()
    {
        foreach (RollDice dice in castDices)
        {
            dice.SkillModify(currentSkill.modifyValue);
        }

        isRefundable = false;
        refundValue = 0;
        refundDices.Clear();

        AudioManager.Instance.PlaySfx("DiceModifying");

        CancelSkill();
    }



    // CANCEL //

    public void CancelSkill()
    {
        if (isRefundable)
        {
            if (currentSkill.costType == SkillCostType.DiceCost)
            {
                CastRefundDice();
            }
            else if (currentSkill.costType == SkillCostType.StaminaPointCost)
            {
                staminaPoint.AddClampedValue(refundValue, 0, maxStamina);
            }
            else if (currentSkill.costType == SkillCostType.SignaturePointCost)
            {
                signaturePoint.AddClampedValue(refundValue, 0, maxStamina);
            }

            isRefundable = false;
            refundValue = 0;
            refundDices.Clear();
        }

        currentSkill = null;
        currentSkillPosition = null;
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
        castDices.Clear();

        if (isFaded)
        {
            FadeOutScene();
        }
    }

    // REFUND DICES //

    public void RegisterRefundDice(DiceType diceType, int dieValue)
    {
        RefundDiceData refundDice = new RefundDiceData(diceType, dieValue);
        refundDices.Add(refundDice);
    }



    // TRANSITIONS //

    private void FadeInScene()
    {
        isFaded = true;

        playerPanel.blocksRaycasts = false;
        adventurePanel.blocksRaycasts = false;

        skillPanel.gameObject.SetActive(true);
        skillPanel.GetComponentInChildren<TextMeshProUGUI>().text = currentSkill.costDescription;
        skillInfo.GetComponent<Image>().sprite = currentSkillPosition.GetComponent<Image>().sprite;

        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            playerPanel.GetComponent<RectTransform>() as RectTransform,
            currentSkillPosition.position,
            null,
            out localPosition);

        skillPanel.anchoredPosition = localPosition + new Vector2(-104, -490);

        skillFadeFilter.GetComponent<CanvasGroup>().alpha = 0.75f;
    }

    private void FadeOutScene()
    {
        isFaded = false;

        playerPanel.blocksRaycasts = true;
        adventurePanel.blocksRaycasts = true;

        if (skillConfirmButton.gameObject.activeSelf)
        {
            skillConfirmButton.gameObject.SetActive(false);
        }

        skillPanel.gameObject.SetActive(false);
        skillPanel.GetComponentInChildren<TextMeshProUGUI>().text = null;
        skillInfo.GetComponent<Image>().sprite = null;

        skillFadeFilter.GetComponent<CanvasGroup>().alpha = 0f;
    }
}

[System.Serializable]
public class RefundDiceData
{
    public DiceType diceType;
    public int dieValue;

    public RefundDiceData(DiceType diceType, int dieValue)
    {
        this.diceType = diceType;
        this.dieValue = dieValue;
    }
}