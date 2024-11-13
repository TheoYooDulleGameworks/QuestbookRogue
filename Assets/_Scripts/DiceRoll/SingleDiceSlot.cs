using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class SingleDiceSlot : DiceSlot, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    [Header("Conditions")]
    [SerializeField] private List<DiceType> diceTypes = new List<DiceType>();
    [SerializeField] private int aboveConditionValue;

    [Header("Components")]
    [SerializeField] private RectTransform slotDiceImage;
    [SerializeField] private GameObject keepingDicePrefab;

    [Header("Sprites")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite checkingSprite;
    [SerializeField] private Sprite succeedSprite;

    [Header("bool Triggers")]
    [SerializeField] private bool isConfirmed;
    public override event Action OnConfirmed;

    [Header("Tweening")]
    [SerializeField] private float popUpScale = 1.35f;
    [SerializeField] private float popUpDuration = 0.25f;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        if (slotDiceImage.gameObject.activeSelf)
        {
            slotDiceImage.gameObject.SetActive(false);
        }
    }

    public override void SetSlotComponents(ContentSO contentData, int requestvalue, SlotSO slotData)
    {
        SingleSlotSO singleSlotData = slotData as SingleSlotSO;

        if (singleSlotData != null)
        {
            diceTypes = singleSlotData.requestDiceTypes;
            aboveConditionValue = singleSlotData.singleDiceValue;
            defaultSprite = singleSlotData.defaultSlotSprite;
            checkingSprite = singleSlotData.checkingSlotSprite;
            succeedSprite = singleSlotData.succeedSlotSprite;

            GetComponent<Image>().sprite = defaultSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DiceValueCheck"))
        {
            RollDice rollDice = collision.GetComponentInParent<RollDice>();

            if (rollDice.IsAboveValue(diceTypes, aboveConditionValue))
            {
                rectTransform.DOKill();
                rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
                rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).SetEase(Ease.OutCubic);

                if (!slotDiceImage.gameObject.activeSelf)
                {
                    slotDiceImage.gameObject.SetActive(true);
                }
                slotDiceImage.GetComponent<Image>().sprite = rollDice.valueSlotSprite;
                GetComponent<Image>().sprite = succeedSprite;

                rollDice.DeActivateRollDice();
                keepingDicePrefab = rollDice.gameObject;

                isConfirmed = true;
                OnConfirmed?.Invoke();
            }
            else
            {
                return;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isConfirmed)
            {
                GetComponent<Image>().sprite = defaultSprite;
                slotDiceImage.GetComponent<Image>().sprite = keepingDicePrefab.GetComponent<RollDice>().valueSlotClickSprite;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isConfirmed)
            {
                GetComponent<Image>().sprite = succeedSprite;
                slotDiceImage.GetComponent<Image>().sprite = keepingDicePrefab.GetComponent<RollDice>().valueSlotSprite;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isConfirmed)
        {
            return;
        }

        isConfirmed = false;
        OnConfirmed?.Invoke();

        rectTransform.DOKill();
        rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
        rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).SetEase(Ease.OutCubic);

        keepingDicePrefab.GetComponent<RollDice>().ActivateRollDice();
        keepingDicePrefab.GetComponent<RollDice>().PopUpAnim();

        Vector2 mousePosition = Input.mousePosition;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponentInParent<Canvas>().GetComponent<RectTransform>(),
            mousePosition,
            null,
            out localPoint
        );

        keepingDicePrefab.GetComponent<RectTransform>().anchoredPosition = localPoint;

        slotDiceImage.GetComponent<Image>().sprite = null;
        if (slotDiceImage.gameObject.activeSelf)
        {
            slotDiceImage.gameObject.SetActive(false);
        }

        GetComponent<Image>().sprite = defaultSprite;
        keepingDicePrefab = null;
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

    public override void DeleteKeepingDices()
    {
        if (!isConfirmed)
        {
            return;
        }

        rectTransform.DOKill();
        rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
        rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).SetEase(Ease.OutCubic);

        Destroy(keepingDicePrefab);
        keepingDicePrefab = null;
        slotDiceImage.GetComponent<Image>().sprite = null;
        slotDiceImage.gameObject.SetActive(false);
        GetComponent<Image>().sprite = defaultSprite;
        isConfirmed = false;
    }
}
