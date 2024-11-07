using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class ActionContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI actionTitleTMPro = null;
    [SerializeField] private RectTransform bgImageRect = null;
    [SerializeField] private RectTransform actionSealRect = null;
    [SerializeField] private RectTransform slotParentRect = null;
    [SerializeField] private TextMeshProUGUI rewardTitleTMPro = null;
    [SerializeField] private RectTransform proceedButtonRect = null;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultProceedButton;

    [Header("DiceSlot Layout")]
    private int diceSlotSeat = 0;

    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        contentData = _contentData;

        actionTitleTMPro.text = contentData.actionTitle;
        bgImageRect.GetComponent<Image>().sprite = contentData.backgroundImage;
        actionSealRect.GetComponent<Image>().sprite = contentData.actionSeal;
        rewardTitleTMPro.text = contentData.actionRewardText;

        for (int i = 0; i < contentData.actionRequestDiceSlots.Count; i++)
        {
            GameObject requestDiceSlot = Instantiate(contentData.actionRequestDiceSlots[i].slotPrefab);
            requestDiceSlot.transform.SetParent(slotParentRect, false);
            requestDiceSlot.name = $"requestDiceSlot_({i + 1})";

            requestDiceSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-138 + (diceSlotSeat * 92), 26);

            if (contentData.multiValue != null && contentData.multiValue.Count > 0)
            {
                if (contentData.multiValue[i] != 0)
                {
                    requestDiceSlot.GetComponent<RectTransform>().anchoredPosition = requestDiceSlot.GetComponent<RectTransform>().anchoredPosition + new Vector2(46, 0);
                    diceSlotSeat += 2;
                }
                else
                {
                    diceSlotSeat += 1;
                }
            }
            else
            {
                diceSlotSeat += 1;
            }

            requestDiceSlot.GetComponent<DiceSlot>().SetSlotComponents(contentData, i, contentData.actionRequestDiceSlots[i]);

            if (requestDiceSlot.GetComponent<DiceSlot>() != null)
            {
                requestDiceSlot.GetComponent<DiceSlot>().OnConfirmed += CheckAllConfirmed;
            }
        }

        if (contentData.isThereProceedButton)
        {
            proceedButtonRect.GetComponent<ProceedButton>().currentQuestData = _questData;
            proceedButtonRect.GetComponent<ProceedButton>().currentActionContentData = contentData;
            proceedButtonRect.gameObject.SetActive(true);
            proceedButtonRect.GetComponent<Image>().sprite = defaultProceedButton;
        }
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

    public void FlipOnContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        contentCanvas.localEulerAngles = new Vector3(-2f, -90f, -2f);

        contentCanvas.DOKill();
        contentCanvas.DOScale(Vector3.one, 0.2f);
        contentCanvas.DORotate(Vector3.zero, 0.2f);
    }

    public void FlipOffContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();
        
        contentCanvas.localScale = Vector3.one;
        contentCanvas.localEulerAngles = Vector3.zero;

        contentCanvas.DOKill();
        contentCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.2f);
        contentCanvas.DORotate(new Vector3(2f, 90f, 2f), 0.2f);
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
