using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillCancelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultCancelButton;
    [SerializeField] private Sprite mouseOverCancelButton;
    [SerializeField] private Sprite mouseDownCancelButton;

    private bool inTransition;

    private void Start()
    {
        GetComponent<Image>().sprite = defaultCancelButton;
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inTransition)
        {
            return;
        }

        GetComponent<Image>().sprite = mouseOverCancelButton;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inTransition)
        {
            return;
        }

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
        if (inTransition)
        {
            return;
        }

        inTransition = true;
        SkillManager.Instance.CancelSkill();
    }

    public void ActiavteCancelButton()
    {
        GetComponent<Image>().sprite = defaultCancelButton;
        inTransition = false;
    }
}
