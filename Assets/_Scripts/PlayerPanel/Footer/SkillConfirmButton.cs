using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillConfirmButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultConfirmButton;
    [SerializeField] private Sprite activatedConfirmButton;
    [SerializeField] private Sprite mouseOverConfirmButton;
    [SerializeField] private Sprite mouseDownConfirmButton;
    private bool isActivated;

    public void OnEnable()
    {
        GetComponent<Image>().sprite = defaultConfirmButton;

        GetComponent<Image>().raycastTarget = false;
        isActivated = false;
    }

    public void ActivateButton()
    {
        GetComponent<Image>().sprite = activatedConfirmButton;
        GetComponent<Image>().raycastTarget = true;
        isActivated = true;
    }

    public void DeActivateButton()
    {
        if (isActivated)
        {
            GetComponent<Image>().sprite = defaultConfirmButton;
            GetComponent<Image>().raycastTarget = false;
            isActivated = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        GetComponent<Image>().sprite = mouseOverConfirmButton;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        GetComponent<Image>().sprite = activatedConfirmButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        GetComponent<Image>().sprite = mouseDownConfirmButton;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        GetComponent<Image>().sprite = activatedConfirmButton;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        SkillManager.Instance.CastSkill();
    }
}
