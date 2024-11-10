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

    [Header("Components")]
    [SerializeField] private RectTransform actionCanvas = null;
    [SerializeField] private RectTransform actionBackgroundRect = null;
    [SerializeField] private RectTransform actionImageRect = null;
    [SerializeField] private RectTransform actionTitleRect = null;
    [SerializeField] private TextMeshProUGUI actionTitleTMPro = null;
    [SerializeField] private RectTransform actionSealRect = null;
    [SerializeField] private RectTransform slotParentRect = null;
    [SerializeField] private TextMeshProUGUI rewardTextTMPro = null;
    [SerializeField] private RectTransform proceedButtonRect = null;

    [Header("DiceSlot Layout")]
    private int diceSlotSeat1Row = 0;
    private int diceSlotSeat2Row = 0;

    [Header("Reward Components")]
    [SerializeField] private RectTransform rewardCanvas = null;
    [SerializeField] private RectTransform rewardBackgroundRect = null;
    [SerializeField] private RectTransform rewardTitleRect = null;
    [SerializeField] private TextMeshProUGUI rewardTitleTMPro = null;
    [SerializeField] private RectTransform rewardSealRect = null;
    [SerializeField] private RectTransform rewardParentRect = null;
    [SerializeField] private List<GameObject> currentRewards = null;

    [Header("Reward Sources")]
    [SerializeField] private GameObject uiRewardPrefab;
    public bool isThisReward = false;
    public bool isFlippedAlready = false;



    // Settings //

    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        // Reward : 데이터 받아서 프리팹 인스탠시에이트 하고 SetRewardData 세팅 해주기 //

        contentData = _contentData;

        actionBackgroundRect.GetComponent<Image>().sprite = contentData.backgroundImage;
        actionImageRect.GetComponent<Image>().sprite = contentData.actionImage;
        actionTitleRect.GetComponent<Image>().sprite = contentData.actionBelt;
        actionTitleTMPro.text = contentData.actionTitle;
        actionSealRect.GetComponent<Image>().sprite = contentData.actionSeal;
        rewardTextTMPro.text = contentData.actionRewardText;

        rewardBackgroundRect.GetComponent<Image>().sprite = contentData.backgroundImage;
        rewardTitleRect.GetComponent<Image>().sprite = contentData.rewardBelt;
        rewardTitleTMPro.text = contentData.rewardTitle;
        rewardSealRect.GetComponent<Image>().sprite = contentData.rewardSeal;

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

        // Reward Setting //

        for (int i = 0; i < contentData.rewardObjects.Count; i++)
        {
            GameObject rewardObject = Instantiate(uiRewardPrefab);
            rewardObject.transform.SetParent(rewardParentRect, false);
            rewardObject.name = $"Reward_({i + 1})";

            rewardObject.GetComponent<UIReward>().SetRewardData(contentData.rewardObjects[i]);
            currentRewards.Add(rewardObject);

            rewardObject.gameObject.SetActive(false);
        }

        if (contentData.isThereProceedButton)
        {
            proceedButtonRect.GetComponent<ProceedButton>().currentQuestData = _questData;
            proceedButtonRect.GetComponent<ProceedButton>().currentActionContentData = contentData;
            proceedButtonRect.gameObject.SetActive(true);
        }
    }



    // Action //

    public void FlipOnContent()
    {
        RectTransform actionCard = actionCanvas.GetComponent<RectTransform>();

        actionCard.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        actionCard.localEulerAngles = new Vector3(-2f, -90f, -2f);

        actionCard.DOKill();
        actionCard.DOScale(Vector3.one, 0.2f);
        actionCard.DORotate(Vector3.zero, 0.2f);
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

    // Reward //

    public void FlipOnReward()
    {
        if (isFlippedAlready)
        {
            return;
        }

        if (isThisReward)
        {
            StartCoroutine(FlipOnLootRoutine());
        }
        else
        {
            StartCoroutine(FlipOnNoneRoutine());
        }
    }

    private IEnumerator FlipOnLootRoutine()
    {
        isFlippedAlready = true;

        Vector3 rotateVector;
        Vector3 leftRotateVector = new Vector3(-2f, -2f, -2f);
        Vector3 rightRotateVector = new Vector3(2f, 2f, 2f);

        int randomInt = Random.Range(1, 3);
        if (randomInt == 1)
        {
            rotateVector = leftRotateVector;
        }
        else
        {
            rotateVector = rightRotateVector;
        }

        actionCanvas.DOKill();
        actionCanvas.DORotate(rotateVector, 0.1f);
        actionCanvas.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f).OnComplete(() =>
        {
            actionCanvas.DOScale(Vector3.one, 0.2f);
            actionCanvas.DORotate(Vector3.zero, 0.2f).OnComplete(() =>
            {
                actionCanvas.localScale = Vector3.one;
                actionCanvas.localEulerAngles = Vector3.zero;

                actionCanvas.DOKill();
                actionCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.2f);
                actionCanvas.DORotate(new Vector3(2f, 90f, 2f), 0.2f).OnComplete(() =>
                {
                    actionCanvas.gameObject.SetActive(false);
                    rewardCanvas.gameObject.SetActive(true);

                    rewardCanvas.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    rewardCanvas.localEulerAngles = new Vector3(-2f, -90f, -2f);

                    rewardCanvas.DOKill();
                    rewardCanvas.DOScale(Vector3.one, 0.2f);
                    rewardCanvas.DORotate(Vector3.zero, 0.2f).OnComplete(() =>
                    {
                        StartCoroutine(PopUpRewardRoutine());
                    });
                });
            });
        });

        yield return null;
    }

    private IEnumerator PopUpRewardRoutine()
    {
        for (int i = 0; i < currentRewards.Count; i++)
        {
            currentRewards[i].GetComponent<UIReward>().PopUpReward();
            yield return new WaitForSeconds(0.2f);
        }
        rewardParentRect.GetComponent<HorizontalLayoutGroup>().enabled = false;
    }

    private IEnumerator FlipOnNoneRoutine()
    {
        isFlippedAlready = true;
        List<PaySlot> notThisPaySlots = new List<PaySlot>(GetComponentsInChildren<PaySlot>());
        foreach (var paySlot in notThisPaySlots)
        {
            paySlot.ProceedNotThisPayment();
        }

        yield return new WaitForSeconds(0.3f);

        actionCanvas.localScale = Vector3.one;
        actionCanvas.localEulerAngles = Vector3.zero;

        actionCanvas.DOKill();
        actionCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.2f);
        actionCanvas.DORotate(new Vector3(2f, 90f, 2f), 0.2f).OnComplete(() =>
        {
            actionCanvas.gameObject.SetActive(false);
            rewardCanvas.gameObject.SetActive(true);

            rewardTitleRect.gameObject.SetActive(false);
            rewardTitleTMPro.gameObject.SetActive(false);
            rewardSealRect.gameObject.SetActive(false);

            rewardCanvas.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            rewardCanvas.localEulerAngles = new Vector3(-2f, -90f, -2f);

            rewardCanvas.DOKill();
            rewardCanvas.DOScale(Vector3.one, 0.2f);
            rewardCanvas.DORotate(Vector3.zero, 0.2f);
        });
    }



    // OFF //

    public void FlipOffContent()
    {
        if (actionCanvas.gameObject.activeSelf && !rewardCanvas.gameObject.activeSelf)
        {
            FlipOffAction();
        }
        if (!actionCanvas.gameObject.activeSelf && rewardCanvas.gameObject.activeSelf)
        {
            FlipOffReward();
        }
    }

    private void FlipOffAction()
    {
        actionCanvas.localScale = Vector3.one;
        actionCanvas.localEulerAngles = Vector3.zero;

        actionCanvas.DOKill();
        actionCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.2f);
        actionCanvas.DORotate(new Vector3(2f, 90f, 2f), 0.2f);
    }

    private void FlipOffReward()
    {
        rewardCanvas.localScale = Vector3.one;
        rewardCanvas.localEulerAngles = Vector3.zero;

        rewardCanvas.DOKill();
        rewardCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.2f);
        rewardCanvas.DORotate(new Vector3(2f, 90f, 2f), 0.2f);
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
