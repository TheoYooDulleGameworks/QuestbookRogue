using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProceedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Current Action Content Data")]
    [SerializeField] public QuestSO currentQuestData;
    [SerializeField] public ContentSO currentActionContentData;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultProceedButton;
    [SerializeField] private Sprite activatedProceedButton;
    [SerializeField] private Sprite mouseOverProceedButton;
    [SerializeField] private Sprite mouseDownProceedButton;

    private void OnEnable()
    {
        GetComponent<Image>().raycastTarget = false;
        GetComponent<Image>().sprite = defaultProceedButton;
    }

    public void ActivateButton()
    {
        GetComponent<Image>().sprite = activatedProceedButton;
        GetComponent<Image>().raycastTarget = true;
    }

    public void DeActivateButton()
    {
        GetComponent<Image>().sprite = defaultProceedButton;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = mouseOverProceedButton;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = activatedProceedButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = mouseDownProceedButton;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = activatedProceedButton;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;

        List<PaySlot> paySlots = new List<PaySlot>(transform.parent.GetComponentsInChildren<PaySlot>());
        if (paySlots != null && paySlots.Count > 0)
        {
            foreach (PaySlot paySlot in paySlots)
            {
                paySlot.isPrice = true;
            }
        }

        SceneController.Instance.DontRefundDices();
        SceneController.Instance.NotPaySlotRefund();

        GetComponentInParent<ActionContent>().DeliverRewards();
    }
}
