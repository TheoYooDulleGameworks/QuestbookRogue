using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillConfirmButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultConfirmButton;
    [SerializeField] private Sprite mouseOverConfirmButton;
    [SerializeField] private Sprite mouseDownConfirmButton;

    private bool inTransition = true;

    public void OnEnable()
    {
        GetComponent<Image>().sprite = defaultConfirmButton;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inTransition)
        {
            return;
        }

        GetComponent<Image>().sprite = mouseOverConfirmButton;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inTransition)
        {
            return;
        }

        GetComponent<Image>().sprite = defaultConfirmButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = mouseDownConfirmButton;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = defaultConfirmButton;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inTransition)
        {
            return;
        }

        inTransition = true;
        SkillManager.Instance.ConfirmSkill();
    }

    public void ActiavteConfirmButton()
    {
        inTransition = false;
    }
}
