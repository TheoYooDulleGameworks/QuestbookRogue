using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PaySlot : DiceSlot, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Conditions")]
    [SerializeField] private PaymentType paymentType;
    [SerializeField] private int payValue;

    [Header("Player Assets")]
    [SerializeField] private PlayerStatusSO playerStatus;

    [Header("Components")]
    [SerializeField] private RectTransform payIconImage;
    [SerializeField] private RectTransform maskImage;
    [SerializeField] private RectTransform payValueImage10th;
    [SerializeField] private RectTransform payValueImage1th;

    [Header("Sprites")]
    [SerializeField] private Sprite payIconSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite succeedSprite;
    [SerializeField] private Sprite failSprite;
    [SerializeField] private List<Sprite> payValueSprites;

    [Header("bool Triggers")]
    [SerializeField] private bool isConfirmed;
    public override event Action OnConfirmed;
    private RectTransform rectTransform;
    private Coroutine scaleCoroutine;
    private Coroutine colorCoroutine;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public override void SetSlotComponents(ContentSO contentData, int refValueIndex, SlotSO slotData)
    {
        PaySlotSO paySlotData = slotData as PaySlotSO;

        if (paySlotData != null)
        {
            paymentType = paySlotData.paymentType;
            payValue = contentData.payValue[refValueIndex];

            payIconSprite = paySlotData.paymentIconSprite;

            defaultSprite = paySlotData.defaultSlotSprite;
            succeedSprite = paySlotData.succeedSlotSprite;
            failSprite = paySlotData.failSlotSprite;
            payValueSprites = paySlotData.payValueSprites;

            Color color = maskImage.GetComponent<Image>().color;
            color.a = 0f;
            maskImage.GetComponent<Image>().color = color;

            GetComponent<Image>().sprite = defaultSprite;
            payIconImage.GetComponent<Image>().sprite = payIconSprite;

            int payValue10th = payValue / 10;
            int payValue1th = payValue % 10;

            if (payValue < 10)
            {
                payValueImage10th.GetComponent<Image>().sprite = null;
                payValueImage10th.gameObject.SetActive(false);
                payValueImage1th.GetComponent<Image>().sprite = payValueSprites[payValue1th];
            }
            else
            {
                payValueImage10th.GetComponent<Image>().sprite = payValueSprites[payValue10th];
                payValueImage1th.GetComponent<Image>().sprite = payValueSprites[payValue1th];
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color color = maskImage.GetComponent<Image>().color;
        color.a = 0.3f;
        maskImage.GetComponent<Image>().color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color color = maskImage.GetComponent<Image>().color;
        color.a = 0f;
        maskImage.GetComponent<Image>().color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Color color = maskImage.GetComponent<Image>().color;
            color.a = 0.7f;
            maskImage.GetComponent<Image>().color = color;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Color color = maskImage.GetComponent<Image>().color;
            color.a = 0f;
            maskImage.GetComponent<Image>().color = color;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isConfirmed)
            {
                if (CheckPayment(paymentType, payValue) == true)
                {
                    SpendPayment(paymentType, payValue);

                    GetComponent<RectTransform>().localScale = new Vector3(1.35f, 1.35f, 1.35f);
                    if (scaleCoroutine != null)
                    {
                        StopCoroutine(scaleCoroutine);
                    }
                    scaleCoroutine = StartCoroutine(ScaleDownToNormal());

                    GetComponent<Image>().sprite = succeedSprite;
                    isConfirmed = true;
                    OnConfirmed?.Invoke();
                }
                else
                {
                    GetComponent<RectTransform>().localScale = new Vector3(1.35f, 1.35f, 1.35f);
                    if (scaleCoroutine != null)
                    {
                        StopCoroutine(scaleCoroutine);
                    }
                    scaleCoroutine = StartCoroutine(ScaleDownToNormal());

                    GetComponent<Image>().sprite = failSprite;
                    if (colorCoroutine != null)
                    {
                        StopCoroutine(colorCoroutine);
                    }
                    colorCoroutine = StartCoroutine(ColorChangeToNormal());
                }
            }
            else
            {
                RefundPayment(paymentType, payValue);

                GetComponent<RectTransform>().localScale = new Vector3(1.35f, 1.35f, 1.35f);
                if (scaleCoroutine != null)
                {
                    StopCoroutine(scaleCoroutine);
                }
                scaleCoroutine = StartCoroutine(ScaleDownToNormal());

                GetComponent<Image>().sprite = defaultSprite;
                isConfirmed = false;
                OnConfirmed?.Invoke();
            }
        }
    }

    private bool CheckPayment(PaymentType paymentType, int payValue)
    {
        StatusValue playerPaymentValue;

        switch (paymentType)
        {
            case PaymentType.Coin:
                playerPaymentValue = playerStatus.Coin;
                break;
            case PaymentType.Provision:
                playerPaymentValue = playerStatus.Provision;
                break;
            case PaymentType.Hp:
                playerPaymentValue = playerStatus.currentHp;
                break;
            case PaymentType.Sp:
                playerPaymentValue = playerStatus.currentSp;
                break;
            default:
                playerPaymentValue = playerStatus.Lv;
                break;
        }

        if (playerPaymentValue.Value >= payValue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpendPayment(PaymentType paymentType, int payValue)
    {
        StatusValue playerPaymentValue;

        switch (paymentType)
        {
            case PaymentType.Coin:
                playerPaymentValue = playerStatus.Coin;
                break;
            case PaymentType.Provision:
                playerPaymentValue = playerStatus.Provision;
                break;
            case PaymentType.Hp:
                playerPaymentValue = playerStatus.currentHp;
                break;
            case PaymentType.Sp:
                playerPaymentValue = playerStatus.currentSp;
                break;
            default:
                playerPaymentValue = playerStatus.Lv;
                break;
        }

        playerPaymentValue.Value -= payValue;
    }

    private void RefundPayment(PaymentType paymentType, int payValue)
    {
        StatusValue playerPaymentValue;

        switch (paymentType)
        {
            case PaymentType.Coin:
                playerPaymentValue = playerStatus.Coin;
                break;
            case PaymentType.Provision:
                playerPaymentValue = playerStatus.Provision;
                break;
            case PaymentType.Hp:
                playerPaymentValue = playerStatus.currentHp;
                break;
            case PaymentType.Sp:
                playerPaymentValue = playerStatus.currentSp;
                break;
            default:
                playerPaymentValue = playerStatus.Lv;
                break;
        }

        playerPaymentValue.Value += payValue;
    }

    private IEnumerator ScaleDownToNormal()
    {
        Vector3 startScale = rectTransform.localScale;
        Vector3 endScale = new Vector3(1f, 1f, 1f);
        float duration = 0.15f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            rectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = endScale;
        scaleCoroutine = null;
    }

    private IEnumerator ColorChangeToNormal()
    {
        yield return new WaitForSeconds(0.5f);

        GetComponent<Image>().sprite = defaultSprite;
    }

    public override bool CheckConfirmed()
    {
        if (isConfirmed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ProceedNotThisPayment()
    {
        if (isConfirmed)
        {
            RefundPayment(paymentType, payValue);
        }
    }
}
