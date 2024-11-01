using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class RollDice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Value")]
    [SerializeField] private int dieValue;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Animator animator;
    private Coroutine scaleCoroutine;

    [Header("Components")]
    [SerializeField] private RectTransform diceImage;
    [SerializeField] private RectTransform shadowImage;

    [Header("Source Sprites")]
    [SerializeField] private List<Sprite> possibleValues;
    [SerializeField] private List<Sprite> possibleHovers;
    [SerializeField] private List<Sprite> possibleClicks;

    [Header("Own Sprites")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite defaultHoverSprite;

    [SerializeField] private Sprite valueSprite = null;
    [SerializeField] private Sprite valueHoverSprite = null;
    [SerializeField] private Sprite valueClickSprite = null;

    [Header("Bool Triggers")]
    [SerializeField] private bool wasRolled = false;
    [SerializeField] private bool isDragging = false;
    [SerializeField] private bool isMouseInputInitialized = true;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging)
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

    private void Start()
    {
        animator.enabled = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging)
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
        CursorManager.Instance.OnClickCursor();
        transform.SetAsLastSibling();

        if (!wasRolled)
        {
            DiceRollAnim();
            CursorManager.Instance.OnDefaultCursor();
            isDragging = false;
            return;
        }

        diceImage.GetComponent<Image>().sprite = valueClickSprite;
        rectTransform.localScale = new Vector3(1.35f, 1.35f, 1.35f);

        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        scaleCoroutine = StartCoroutine(ScaleDownToNormal());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isMouseInputInitialized)
        {
            isMouseInputInitialized = true;
        }

        CursorManager.Instance.OnDefaultCursor();

        if (!wasRolled)
        {
            return;
        }

        diceImage.GetComponent<Image>().sprite = valueSprite;

        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
            scaleCoroutine = null;
        }

        rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnDrag(PointerEventData eventData)
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
                if (canvasGroup.blocksRaycasts == false)
                {
                    canvasGroup.blocksRaycasts = true;
                }
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
        isDragging = true;

        if (!wasRolled)
        {
            return;
        }

        canvasGroup.blocksRaycasts = false;
        shadowImage.gameObject.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (!wasRolled)
        {
            return;
        }

        canvasGroup.blocksRaycasts = true;
        shadowImage.gameObject.SetActive(true);
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
        Debug.Log(randomValue);

        if (randomValue == 0)
        {
            // NORMAL DICE //
            dieValue = 0;
        }
        else
        {
            dieValue = randomValue + 1;
        }

        valueSprite = possibleValues[randomValue];
        valueHoverSprite = possibleHovers[randomValue];
        valueClickSprite = possibleClicks[randomValue];

        animator.enabled = false;
        diceImage.GetComponent<Image>().sprite = valueSprite;
        GetComponent<Image>().raycastTarget = true;
        wasRolled = true;

        PopUpAnim();
    }



    public bool IsAboveValue(int conditionValue)
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

    public void DestroyRollDice()
    {
        Destroy(gameObject);
    }
}
