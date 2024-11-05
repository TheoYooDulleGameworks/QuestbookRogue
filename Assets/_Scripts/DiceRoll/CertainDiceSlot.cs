using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Linq;
using System;

public class CertainDiceSlot : DiceSlot, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    [Header("Conditions")]
    [SerializeField] private List<DiceType> diceTypes = new List<DiceType>();
    [SerializeField] private List<int> aboveConditionValues;

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
    private Coroutine scaleCoroutine;

    private void OnEnable()
    {
        if (slotDiceImage.gameObject.activeSelf)
        {
            slotDiceImage.gameObject.SetActive(false);
        }
    }

    public override void SetSlotComponents(ContentSO contentData, int refValueIndex, SlotSO slotData)
    {
        CertainSlotSO certainSlotData = slotData as CertainSlotSO;

        if (certainSlotData != null)
        {
            diceTypes = certainSlotData.requestDiceTypes;
            aboveConditionValues = certainSlotData.requestDiceValues;
            defaultSprite = certainSlotData.defaultSlotSprite;
            checkingSprite = certainSlotData.checkingSlotSprite;
            succeedSprite = certainSlotData.succeedSlotSprite;

            GetComponent<Image>().sprite = defaultSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DiceValueCheck"))
        {
            RollDice rollDice = collision.GetComponentInParent<RollDice>();

            if (rollDice.IsSameValue(diceTypes, aboveConditionValues))
            {
                GetComponent<RectTransform>().localScale = new Vector3(1.35f, 1.35f, 1.35f);
                if (scaleCoroutine != null)
                {
                    StopCoroutine(scaleCoroutine);
                }
                scaleCoroutine = StartCoroutine(ScaleDownToNormal());

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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isConfirmed)
            {
                return;
            }

            isConfirmed = false;
            OnConfirmed?.Invoke();

            GetComponent<RectTransform>().localScale = new Vector3(1.35f, 1.35f, 1.35f);
            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine);
            }
            scaleCoroutine = StartCoroutine(ScaleDownToNormal());

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
    }


    private IEnumerator ScaleDownToNormal()
    {
        Vector3 startScale = GetComponent<RectTransform>().localScale;
        Vector3 endScale = new Vector3(1f, 1f, 1f);
        float duration = 0.15f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            GetComponent<RectTransform>().localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponent<RectTransform>().localScale = endScale;
        scaleCoroutine = null;
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
