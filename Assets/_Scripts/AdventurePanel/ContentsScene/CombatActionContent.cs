using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class CombatActionContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Action Components")]
    [SerializeField] private RectTransform actionCanvas = null;
    [SerializeField] private RectTransform actionBackgroundRect = null;
    [SerializeField] private RectTransform actionTitleRect = null;
    [SerializeField] private TextMeshProUGUI actionTitleTMPro = null;
    [SerializeField] private RectTransform slotParentRect = null;
    [SerializeField] private CombatOptionButton combatOptionsButton = null;

    [Header("DiceSlot Layout")]
    private int diceSlotSeat1Row = 0;
    private int diceSlotSeat2Row = 0;

    [Header("Prefab Sources")]
    [SerializeField] private GameObject uiCombatOptionPrefab;

    private void OnEnable()
    {
        GameManager.Instance.OnStagePhaseChanged += HandleStagePhaseChange;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStagePhaseChanged -= HandleStagePhaseChange;
    }

    // Settings //

    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        contentData = _contentData;

        actionBackgroundRect.GetComponent<Image>().sprite = _questData.questBackgroundImage;
        actionTitleRect.GetComponent<Image>().sprite = _questData.questBeltImage;
        actionTitleTMPro.text = contentData.actionTitle;

        // DiceSlot Setting //

        for (int i = 0; i < contentData.actionRequestSlots1Row.Count; i++)
        {
            GameObject requestDiceSlot = Instantiate(contentData.actionRequestSlots1Row[i].RequestSlot.slotPrefab);
            requestDiceSlot.transform.SetParent(slotParentRect, false);
            requestDiceSlot.name = $"requestDiceSlot_1Row_({i + 1})";

            requestDiceSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-138 + (diceSlotSeat1Row * 92), 26);

            if (contentData.actionRequestSlots1Row[i].DiceSlotType == DiceSlotType.Multi)
            {
                requestDiceSlot.GetComponent<RectTransform>().anchoredPosition = requestDiceSlot.GetComponent<RectTransform>().anchoredPosition + new Vector2(46, 0);
                diceSlotSeat1Row += 2;
            }
            else
            {
                diceSlotSeat1Row += 1;
            }

            requestDiceSlot.GetComponent<DiceSlot>().SetSlotComponents(contentData, contentData.actionRequestSlots1Row[i].RequestValue, contentData.actionRequestSlots1Row[i].RequestSlot);

            if (requestDiceSlot.GetComponent<DiceSlot>() != null)
            {
                requestDiceSlot.GetComponent<DiceSlot>().OnConfirmed += CheckAllConfirmed;
            }
        }

        for (int i = 0; i < contentData.actionRequestSlots2Row.Count; i++)
        {
            GameObject requestDiceSlot = Instantiate(contentData.actionRequestSlots2Row[i].RequestSlot.slotPrefab);
            requestDiceSlot.transform.SetParent(slotParentRect, false);
            requestDiceSlot.name = $"requestDiceSlot_2Row_({i + 1})";

            requestDiceSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-138 + (diceSlotSeat2Row * 92), -70);

            if (contentData.actionRequestSlots2Row[i].DiceSlotType == DiceSlotType.Multi)
            {
                requestDiceSlot.GetComponent<RectTransform>().anchoredPosition = requestDiceSlot.GetComponent<RectTransform>().anchoredPosition + new Vector2(46, 0);
                diceSlotSeat2Row += 2;
            }
            else
            {
                diceSlotSeat2Row += 1;
            }

            requestDiceSlot.GetComponent<DiceSlot>().SetSlotComponents(contentData, contentData.actionRequestSlots2Row[i].RequestValue, contentData.actionRequestSlots2Row[i].RequestSlot);

            if (requestDiceSlot.GetComponent<DiceSlot>() != null)
            {
                requestDiceSlot.GetComponent<DiceSlot>().OnConfirmed += CheckAllConfirmed;
            }
        }

        // CombatOption Setting //

        for (int i = 0; i < contentData.combatOptionSets.Count; i++)
        {
            GameObject combatOption = Instantiate(uiCombatOptionPrefab);
            combatOption.transform.SetParent(combatOptionsButton.transform, false);
            combatOption.name = $"CombatOption_({i + 1})";

            CombatOptionSet thisOptionSet = contentData.combatOptionSets[i];

            combatOptionsButton.combatOptionLists.Add(combatOption.GetComponent<CombatOption>());
            combatOption.GetComponent<CombatOption>().SetOptionComponents(thisOptionSet.combatOptionType, thisOptionSet.optionModifyType, thisOptionSet.optionAmount);
        }

        GameManager.Instance.UpdateStagePhase(StagePhase.Beginning);
    }



    // Combat - Turn Management //

    private void HandleStagePhaseChange(StagePhase stagePhase)
    {
        if (stagePhase == StagePhase.DiceHolding)
        {
            List<DiceSlot> diceSlots = new List<DiceSlot>(GetComponentsInChildren<DiceSlot>());
            foreach (DiceSlot slot in diceSlots)
            {
                slot.DeleteKeepingDices();
            }
        }
    }



    // Action //

    public void FlipOnContent()
    {
        RectTransform actionCard = actionCanvas.GetComponent<RectTransform>();

        actionCard.GetComponent<CanvasGroup>().alpha = 0f;
        actionCard.localScale = new Vector3(0.75f, 0.5f, 0.5f);
        actionCard.localEulerAngles = new Vector3(-90f, 12f, 12f);

        actionCard.DOKill();
        actionCard.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f);
        actionCard.DORotate(new Vector3(4f, 12f, -2f), 0.25f);
        actionCard.GetComponent<CanvasGroup>().DOFade(1f, 0.25f).OnComplete(() =>
        {
            actionCard.DOKill();
            actionCard.DOScale(Vector3.one, 0.25f);
            actionCard.DORotate(Vector3.zero, 0.25f);
        });
    }

    private void CheckAllConfirmed()
    {
        List<DiceSlot> diceSlots = new List<DiceSlot>(slotParentRect.GetComponentsInChildren<DiceSlot>());

        if (diceSlots.All(slot => slot.CheckConfirmed()))
        {
            ProceedActivate();
        }
        else
        {
            ProceedDeActivate();
        }
    }

    private void ProceedActivate()
    {
        combatOptionsButton.ActivateButton();
    }
    private void ProceedDeActivate()
    {
        combatOptionsButton.DeActivateButton();
    }



    // OFF //

    public void FlipOffContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.localEulerAngles = Vector3.zero;
        contentCanvas.localScale = Vector3.one;

        contentCanvas.DOKill();
        contentCanvas.DORotate(new Vector3(-75f, -12f, -6f), 0.25f);
        contentCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.25f);
        contentCanvas.GetComponent<CanvasGroup>().DOFade(0, 0.25f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
