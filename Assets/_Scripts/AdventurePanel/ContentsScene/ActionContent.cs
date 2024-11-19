using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class ActionContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Action Components")]
    [SerializeField] private RectTransform actionCanvas = null;
    [SerializeField] private RectTransform actionBackgroundRect = null;
    [SerializeField] private RectTransform actionTitleRect = null;
    [SerializeField] private TextMeshProUGUI actionTitleTMPro = null;
    [SerializeField] private RectTransform slotParentRect = null;
    [SerializeField] private RectTransform proceedButtonRect = null;

    [Header("DiceSlot Layout")]
    private int diceSlotSeat1Row = 0;
    private int diceSlotSeat2Row = 0;



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

        proceedButtonRect.GetComponent<ProceedButton>().currentQuestData = _questData;
        proceedButtonRect.GetComponent<ProceedButton>().currentActionContentData = contentData;

        proceedButtonRect.gameObject.SetActive(true);
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
        proceedButtonRect.GetComponent<ProceedButton>().ActivateButton();
    }
    private void ProceedDeActivate()
    {
        proceedButtonRect.GetComponent<ProceedButton>().DeActivateButton();
    }



    // Complete Management //

    public void DeliverRewards()
    {
        transform.parent.GetComponentInChildren<ImageContent>().FlipOnCompleted(contentData.eventEpilogueImage, contentData.eventEpilogue, contentData.eventRewards);

        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.eulerAngles = Vector3.zero;
        contentCanvas.localScale = Vector3.one;

        contentCanvas.DORotate(new Vector3(-4f, -12f, 2f), 0.1f);
        contentCanvas.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() =>
        {
            contentCanvas.DORotate(Vector3.zero, 0.2f);
            contentCanvas.DOScale(Vector3.one, 0.2f);
        });
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
