using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class RollDice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("Value")]
    [SerializeField] private DiceType diceType;
    [SerializeField] private int dieValue;
    [SerializeField] private GameObject checkCollider;

    private RectTransform rectTransform;
    private Canvas canvas;
    private Animator animator;
    private Coroutine scaleCoroutine;

    [Header("Components")]
    [SerializeField] private RectTransform diceImage;
    [SerializeField] private RectTransform shadowImage;

    [Header("Source Sprites")]
    [SerializeField] private List<Sprite> possibleValues;
    [SerializeField] private List<Sprite> possibleHovers;
    [SerializeField] private List<Sprite> possibleClicks;
    [SerializeField] private List<Sprite> possibleSlotValues;
    [SerializeField] private List<Sprite> possibleSlotClicks;

    [Header("Own Sprites")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite defaultHoverSprite;

    [SerializeField] private Sprite valueSprite = null;
    [SerializeField] private Sprite valueHoverSprite = null;
    [SerializeField] private Sprite valueClickSprite = null;
    [SerializeField] public Sprite valueSlotSprite = null;
    [SerializeField] public Sprite valueSlotClickSprite = null;

    [Header("Bool Triggers")]
    [SerializeField] private bool wasRolled = false;
    [SerializeField] private bool isDragging = false;
    [SerializeField] private bool isMouseInputInitialized = true;
    [SerializeField] private bool notYetRolledAndWait = false;
    [SerializeField] private bool onceRolled = false;

    [SerializeField] private bool isAdvanced = false;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        animator = GetComponent<Animator>();
        animator.enabled = false;

        checkCollider.SetActive(false);
    }

    public void ActivateRollDice()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    public void DeActivateRollDice()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    public void DestroyRollDice()
    {
        Destroy(gameObject);
    }

    public void OnReRollTriggered()
    {
        if (wasRolled)
        {
            wasRolled = false;
        }
        if (!isMouseInputInitialized)
        {
            isMouseInputInitialized = true;
        }
        if (notYetRolledAndWait)
        {
            notYetRolledAndWait = false;
        }
        if (onceRolled)
        {
            onceRolled = false;
        }

        diceImage.GetComponent<Image>().sprite = defaultSprite;
        PopUpAnim();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging || notYetRolledAndWait)
        {
            return;
        }

        if (!wasRolled)
        {
            diceImage.GetComponent<Image>().sprite = defaultHoverSprite;
            return;
        }

        diceImage.GetComponent<Image>().sprite = valueHoverSprite;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging || notYetRolledAndWait)
        {
            return;
        }

        if (!wasRolled)
        {
            diceImage.GetComponent<Image>().sprite = defaultSprite;
            return;
        }

        diceImage.GetComponent<Image>().sprite = valueSprite;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            CursorManager.Instance.OnClickCursor();

            if (!wasRolled)
            {
                diceImage.GetComponent<Image>().sprite = defaultSprite;
                rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                notYetRolledAndWait = true;

                return;
            }

            transform.SetAsLastSibling();
            diceImage.GetComponent<Image>().sprite = valueClickSprite;
            rectTransform.localScale = new Vector3(1.35f, 1.35f, 1.35f);

            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine);
            }
            scaleCoroutine = StartCoroutine(ScaleDownToNormal());

            if (checkCollider.activeSelf)
            {
                checkCollider.SetActive(false);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isMouseInputInitialized)
            {
                isMouseInputInitialized = true;
            }

            CursorManager.Instance.OnDefaultCursor();

            if (!wasRolled)
            {
                rectTransform.localScale = new Vector3(1f, 1f, 1f);
                notYetRolledAndWait = false;

                return;
            }


            diceImage.GetComponent<Image>().sprite = valueSprite;

            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine);
                scaleCoroutine = null;
            }

            rectTransform.localScale = new Vector3(1f, 1f, 1f);

            if (!checkCollider.activeSelf)
            {
                checkCollider.SetActive(true);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {

            if (wasRolled)
            {
                return;
            }

            CursorManager.Instance.OnDefaultCursor();
            RollAllDice();
        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnReRollTriggered();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (wasRolled && isMouseInputInitialized)
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                     canvas.transform as RectTransform,
                     eventData.position,
                     eventData.pressEventCamera,
                     out Vector2 localPoint))
                {
                    rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
                    ClampPositionToBoundary();
                }
            }
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Input.mousePosition;

            if (isDragging && (mousePosition.x < 0 || mousePosition.x > 1920 * canvas.scaleFactor || mousePosition.y < 0 || mousePosition.y > 1080 * canvas.scaleFactor))

            {
                isMouseInputInitialized = false;

                CursorManager.Instance.OnDefaultCursor();
                diceImage.GetComponent<Image>().sprite = valueSprite;
                if (scaleCoroutine != null)
                {
                    StopCoroutine(scaleCoroutine);
                    scaleCoroutine = null;
                }
                rectTransform.localScale = new Vector3(1f, 1f, 1f);

                isDragging = false;
                if (shadowImage.gameObject.activeSelf == false)
                {
                    shadowImage.gameObject.SetActive(true);
                }
            }
        }
    }

    private void ClampPositionToBoundary()
    {
        Vector3 minPosition = transform.parent.GetComponent<RectTransform>().rect.min - rectTransform.rect.min;
        Vector3 maxPosition = transform.parent.GetComponent<RectTransform>().rect.max - rectTransform.rect.max;

        Vector2 clampedPosition = rectTransform.anchoredPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minPosition.x, maxPosition.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minPosition.y, maxPosition.y);

        rectTransform.anchoredPosition = clampedPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!wasRolled)
            {
                return;
            }

            isDragging = true;

            shadowImage.gameObject.SetActive(false);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!wasRolled)
            {
                return;
            }

            isDragging = false;

            shadowImage.gameObject.SetActive(true);
        }
    }

    private void RollAllDice()
    {
        StartCoroutine(RollAllDiceSequence());
    }

    private IEnumerator RollAllDiceSequence()
    {
        List<RollDice> allDices = new List<RollDice>(transform.parent.GetComponentsInChildren<RollDice>());

        foreach (var dice in allDices)
        {
            if (!dice.onceRolled)
            {
                dice.onceRolled = true;
                dice.DiceRollAnim();
                yield return new WaitForSeconds(0.025f);
            }
        }
    }

    public void DiceRollAnim()
    {
        animator.enabled = true;
        animator.Play("DiceIdle", 0, 0f);
        animator.SetTrigger("DiceRoll");
        isMouseInputInitialized = false;
    }

    public void DiceRollValue()
    {
        int randomValue = Random.Range(0, 6);

        if (randomValue == 0)
        {
            if (isAdvanced)
            {
                dieValue = 1;
            }
            else
            {
                dieValue = 0;
            }
        }
        else
        {
            dieValue = randomValue + 1;
        }

        valueSprite = possibleValues[randomValue];
        valueHoverSprite = possibleHovers[randomValue];
        valueClickSprite = possibleClicks[randomValue];
        valueSlotSprite = possibleSlotValues[randomValue];
        valueSlotClickSprite = possibleSlotClicks[randomValue];

        animator.enabled = false;
        diceImage.GetComponent<Image>().sprite = valueSprite;
        GetComponent<Image>().raycastTarget = true;
        wasRolled = true;
        isMouseInputInitialized = true;

        PopUpAnim();
    }



    public bool IsAboveValue(List<DiceType> diceTypes, int conditionValue)
    {
        if (diceTypes.Contains(diceType))
        {
            if (conditionValue <= dieValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool IsSameValue(List<DiceType> diceTypes, List<int> conditionValues)
    {
        if (diceTypes.Contains(diceType))
        {
            if (conditionValues.Contains(dieValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool IsFillValue(List<DiceType> diceTypes)
    {
        if (diceTypes.Contains(diceType))
        {
            if (dieValue != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public int DieValue()
    {
        return dieValue;
    }



    public void RaycastOff()
    {
        GetComponent<Image>().raycastTarget = false;
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

    public void PopUpAnim()
    {
        rectTransform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
        StartCoroutine(PopDownToNormal());
    }

    private IEnumerator PopDownToNormal()
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
    }
}

public enum DiceType
{
    None,
    Strength,
    Dexterity,
    Intelligence,
    Willpower,
}