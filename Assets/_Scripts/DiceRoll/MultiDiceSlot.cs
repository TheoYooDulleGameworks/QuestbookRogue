using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Linq;
using System;

public class MultiDiceSlot : DiceSlot, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    [Header("Conditions")]
    [SerializeField] private List<DiceType> diceTypes = new List<DiceType>();
    [SerializeField] public int conditionValue;
    [SerializeField] public int currentValue = 0;

    [Header("Components")]
    [SerializeField] private RectTransform slotDiceImage;
    [SerializeField] private RectTransform maxValueImage10th;
    [SerializeField] private RectTransform maxValueImage1th;
    [SerializeField] private RectTransform currentValueImage10th;
    [SerializeField] private RectTransform currentValueImage1th;
    [SerializeField] private List<GameObject> keepingDicePrefabs;

    [Header("Sprites")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite checkingSprite;
    [SerializeField] private Sprite succeedSprite;
    [SerializeField] private List<Sprite> maxSprites;
    [SerializeField] private List<Sprite> currentSprites;

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

    public override void SetSlotComponents(ContentSO contentData, int refValueIndex, SlotSO slotData)
    {
        MultiSlotSO multiSlotData = slotData as MultiSlotSO;

        if (multiSlotData != null)
        {
            diceTypes = multiSlotData.requestDiceTypes;
            conditionValue = contentData.multiValue[refValueIndex];
            defaultSprite = multiSlotData.defaultSlotSprite;
            checkingSprite = multiSlotData.checkingSlotSprite;
            succeedSprite = multiSlotData.succeedSlotSprite;
            maxSprites = multiSlotData.maxValueSprites;
            currentSprites = multiSlotData.currentValueSprites;

            GetComponent<Image>().sprite = defaultSprite;

            int multiConditionValue10th = conditionValue / 10;
            int multiConditionValue1th = conditionValue % 10;

            if (conditionValue < 10)
            {
                maxValueImage10th.GetComponent<Image>().sprite = null;
                maxValueImage10th.gameObject.SetActive(false);
                maxValueImage1th.GetComponent<Image>().sprite = maxSprites[multiConditionValue1th];
            }
            else
            {
                maxValueImage10th.GetComponent<Image>().sprite = maxSprites[multiConditionValue10th];
                maxValueImage1th.GetComponent<Image>().sprite = maxSprites[multiConditionValue1th];
            }

            currentValueImage10th.GetComponent<Image>().sprite = null;
            currentValueImage1th.GetComponent<Image>().sprite = null;

            if (currentValueImage10th.gameObject.activeSelf)
            {
                currentValueImage10th.gameObject.SetActive(false);
            }
            if (currentValueImage1th.gameObject.activeSelf)
            {
                currentValueImage1th.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DiceValueCheck"))
        {
            RollDice rollDice = collision.GetComponentInParent<RollDice>();

            if (!isConfirmed && rollDice.IsFillValue(diceTypes) == true)
            {
                Debug.Log("Fill!!!!!");

                rectTransform.DOKill();
                rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
                rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).SetEase(Ease.OutCubic);

                if (!slotDiceImage.gameObject.activeSelf)
                {
                    slotDiceImage.gameObject.SetActive(true);
                }

                slotDiceImage.GetComponent<Image>().sprite = rollDice.valueSlotSprite;
                keepingDicePrefabs.Add(rollDice.gameObject);
                rollDice.DeActivateRollDice();

                int justValue = rollDice.DieValue();
                currentValue += justValue;

                int currentValue10th = currentValue / 10;
                int currentValue1th = currentValue % 10;

                if (currentValue < 10)
                {
                    currentValueImage10th.GetComponent<Image>().sprite = null;
                    if (currentValueImage10th.gameObject.activeSelf)
                    {
                        currentValueImage10th.gameObject.SetActive(false);
                    }
                    if (!currentValueImage1th.gameObject.activeSelf)
                    {
                        currentValueImage1th.gameObject.SetActive(true);
                    }
                    currentValueImage1th.GetComponent<Image>().sprite = currentSprites[currentValue1th];
                }
                else
                {
                    if (!currentValueImage10th.gameObject.activeSelf)
                    {
                        currentValueImage10th.gameObject.SetActive(true);
                    }
                    if (!currentValueImage1th.gameObject.activeSelf)
                    {
                        currentValueImage1th.gameObject.SetActive(true);
                    }
                    currentValueImage10th.GetComponent<Image>().sprite = currentSprites[currentValue10th];
                    currentValueImage1th.GetComponent<Image>().sprite = currentSprites[currentValue1th];
                }

                IsConfirmCheck();
            }
            else
            {
                Debug.Log("Fail.....");
            }
        }
    }

    private void IsConfirmCheck()
    {
        if (currentValue >= conditionValue)
        {
            GetComponent<Image>().sprite = succeedSprite;
            isConfirmed = true;
            OnConfirmed?.Invoke();
        }
        else
        {
            GetComponent<Image>().sprite = defaultSprite;
            isConfirmed = false;
            OnConfirmed?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (keepingDicePrefabs.Count != 0)
        {
            if (isConfirmed)
            {
                GetComponent<Image>().sprite = defaultSprite;
            }
            slotDiceImage.GetComponent<Image>().sprite = keepingDicePrefabs.Last().GetComponent<RollDice>().valueSlotClickSprite;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (keepingDicePrefabs.Count != 0)
        {
            if (isConfirmed)
            {
                GetComponent<Image>().sprite = succeedSprite;
            }
            slotDiceImage.GetComponent<Image>().sprite = keepingDicePrefabs.Last().GetComponent<RollDice>().valueSlotSprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (keepingDicePrefabs.Count == 0)
        {
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            int j = keepingDicePrefabs.Count;

            Debug.Log(j);

            for (int i = 0; i < j; i++)
            {
                Debug.Log(i);

                rectTransform.DOKill();
                rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
                rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).SetEase(Ease.OutCubic);

                keepingDicePrefabs.First().GetComponent<RollDice>().ActivateRollDice();
                keepingDicePrefabs.First().GetComponent<RollDice>().PopUpAnim();

                Vector2 mousePosition = Input.mousePosition;
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    GetComponentInParent<Canvas>().GetComponent<RectTransform>(),
                    mousePosition,
                    null,
                    out localPoint
                );
                keepingDicePrefabs.First().GetComponent<RectTransform>().anchoredPosition = localPoint + new Vector2(i * 30, i * -60);

                int justValue = keepingDicePrefabs.First().GetComponent<RollDice>().DieValue();

                currentValue -= justValue;

                keepingDicePrefabs.Remove(keepingDicePrefabs.First());
            }
        }

        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            rectTransform.DOKill();
            rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
            rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).SetEase(Ease.OutCubic);

            keepingDicePrefabs.Last().GetComponent<RollDice>().ActivateRollDice();
            keepingDicePrefabs.Last().GetComponent<RollDice>().PopUpAnim();

            Vector2 mousePosition = Input.mousePosition;
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponentInParent<Canvas>().GetComponent<RectTransform>(),
                mousePosition,
                null,
                out localPoint
            );
            keepingDicePrefabs.Last().GetComponent<RectTransform>().anchoredPosition = localPoint;

            int justValue = keepingDicePrefabs.Last().GetComponent<RollDice>().DieValue();
            keepingDicePrefabs.Remove(keepingDicePrefabs.Last());

            currentValue -= justValue;
        }

        else
        {
            return;
        }



        int currentValue10th = currentValue / 10;
        int currentValue1th = currentValue % 10;

        if (currentValue == 0)
        {

            currentValueImage10th.GetComponent<Image>().sprite = null;
            currentValueImage1th.GetComponent<Image>().sprite = null;

            if (currentValueImage10th.gameObject.activeSelf)
            {
                currentValueImage10th.gameObject.SetActive(false);
            }
            if (currentValueImage1th.gameObject.activeSelf)
            {
                currentValueImage1th.gameObject.SetActive(false);
            }
        }
        else if (currentValue < 10)
        {
            currentValueImage10th.GetComponent<Image>().sprite = null;
            if (currentValueImage10th.gameObject.activeSelf)
            {
                currentValueImage10th.gameObject.SetActive(false);
            }
            if (!currentValueImage1th.gameObject.activeSelf)
            {
                currentValueImage1th.gameObject.SetActive(true);
            }
            currentValueImage1th.GetComponent<Image>().sprite = currentSprites[currentValue1th];
        }
        else
        {
            if (!currentValueImage10th.gameObject.activeSelf)
            {
                currentValueImage10th.gameObject.SetActive(true);
            }
            if (!currentValueImage1th.gameObject.activeSelf)
            {
                currentValueImage1th.gameObject.SetActive(true);
            }

            currentValueImage10th.GetComponent<Image>().sprite = currentSprites[currentValue10th];
            currentValueImage1th.GetComponent<Image>().sprite = currentSprites[currentValue1th];
        }

        if (keepingDicePrefabs.Count != 0)
        {
            slotDiceImage.GetComponent<Image>().sprite = keepingDicePrefabs.Last().GetComponent<RollDice>().valueSlotSprite;
        }
        else
        {
            slotDiceImage.GetComponent<Image>().sprite = null;
            slotDiceImage.gameObject.SetActive(false);
        }

        IsConfirmCheck();
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
}
