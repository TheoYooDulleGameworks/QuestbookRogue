using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Current Quest Data")]
    [SerializeField] public QuestSO currentQuestData;
    [SerializeField] public bool isFreeToCancel;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultCancelButton;
    [SerializeField] private Sprite mouseOverCancelButton;
    [SerializeField] private Sprite mouseDownCancelButton;
    [SerializeField] private Sprite defaultFreeCancelButton;
    [SerializeField] private Sprite mouseOverFreeCancelButton;
    [SerializeField] private Sprite mouseDownFreeCancelButton;

    private void OnEnable()
    {
        GetComponent<Image>().raycastTarget = true;
    }

    public void DeActivateTarget()
    {
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isFreeToCancel)
        {
            GetComponent<Image>().sprite = mouseOverFreeCancelButton;
        }
        else
        {
            GetComponent<Image>().sprite = mouseOverCancelButton;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isFreeToCancel)
        {
            GetComponent<Image>().sprite = defaultFreeCancelButton;
        }
        else
        {
            GetComponent<Image>().sprite = defaultCancelButton;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isFreeToCancel)
        {
            GetComponent<Image>().sprite = mouseDownFreeCancelButton;
        }
        else
        {
            GetComponent<Image>().sprite = mouseDownCancelButton;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isFreeToCancel)
        {
            GetComponent<Image>().sprite = defaultFreeCancelButton;
        }
        else
        {
            GetComponent<Image>().sprite = defaultCancelButton;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isFreeToCancel)
        {

        }
        else
        {

        }

        GetComponent<Image>().raycastTarget = false;

        List<PaySlot> notThisPaySlots = new List<PaySlot>(transform.parent.parent.GetComponentsInChildren<PaySlot>());
        for (int i = 0; i < notThisPaySlots.Count; i++)
        {
            notThisPaySlots[i].ProceedNotThisPayment();
        }

        SceneController.Instance.TransitionToSelects(currentQuestData);
    }
}

