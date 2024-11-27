using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillCancelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultCancelButton;
    [SerializeField] private Sprite mouseOverCancelButton;
    [SerializeField] private Sprite mouseDownCancelButton;

    public void OnEnable()
    {
        GetComponent<Image>().sprite = defaultCancelButton;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = mouseOverCancelButton;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = defaultCancelButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = mouseDownCancelButton;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = defaultCancelButton;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SkillManager.Instance.CancelSkill();
    }
}
