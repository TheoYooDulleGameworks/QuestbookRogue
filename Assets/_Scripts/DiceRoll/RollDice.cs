using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class RollDice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("Value")]
    [SerializeField] private int dieValue;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Animator animator;

    [Header("Components")]
    [SerializeField] private RectTransform diceImage;
    [SerializeField] private RectTransform shadowImage;

    [Header("Sprites")]
    [SerializeField] private Sprite defaultDiceSprite;
    [SerializeField] private Sprite defaultMouseOverDiceSprite;

    [SerializeField] private Sprite valueDiceSprite;
    [SerializeField] private Sprite mouseOverDiceSprite;
    [SerializeField] private Sprite mouseDownDiceSprite;
    private Coroutine scaleCoroutine;

    [SerializeField] private bool wasRolled;
    [SerializeField] private bool isDragging;

    private void Awake()
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
            diceImage.GetComponent<Image>().sprite = defaultMouseOverDiceSprite;
            return;
        }

        diceImage.GetComponent<Image>().sprite = mouseOverDiceSprite;
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
            diceImage.GetComponent<Image>().sprite = defaultDiceSprite;
            return;
        }

        diceImage.GetComponent<Image>().sprite = valueDiceSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CursorManager.Instance.OnClickCursor();
        isDragging = true;
        transform.SetAsLastSibling();

        if (!wasRolled)
        {
            return;
        }

        diceImage.GetComponent<Image>().sprite = mouseDownDiceSprite;
        rectTransform.localScale = new Vector3(1.35f, 1.35f, 1.35f);

        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        scaleCoroutine = StartCoroutine(ScaleDownToNormal());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CursorManager.Instance.OnDefaultCursor();
        isDragging = false;

        if (!wasRolled)
        {
            return;
        }

        diceImage.GetComponent<Image>().sprite = valueDiceSprite;

        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
            scaleCoroutine = null;
        }

        rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnDrag(PointerEventData eventData)
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
        if (!wasRolled)
        {
            return;
        }

        canvasGroup.blocksRaycasts = false;
        shadowImage.gameObject.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!wasRolled)
        {
            return;
        }

        canvasGroup.blocksRaycasts = true;
        shadowImage.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (wasRolled)
        {
            return;
        }

        DiceRollAnim();
    }

    public void DiceRollAnim()
    {
        wasRolled = true;
        animator.enabled = true;
        animator.Play("DiceIdle", 0, 0f);
        animator.SetTrigger("DiceRoll");
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

    public void RaycastOn()
    {
        GetComponent<Image>().raycastTarget = true;
        animator.enabled = false;
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
