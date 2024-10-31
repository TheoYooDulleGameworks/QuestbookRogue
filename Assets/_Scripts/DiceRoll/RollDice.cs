using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class RollDice : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform myRect;
    private CanvasGroup canvasGroup;

    [SerializeField] private Sprite defaultDiceSprite;
    [SerializeField] private Sprite draggingDiceSprite;

    [SerializeField] private int dieValue;

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = draggingDiceSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = defaultDiceSprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        myRect.anchoredPosition += eventData.delta / 2;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
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
}
