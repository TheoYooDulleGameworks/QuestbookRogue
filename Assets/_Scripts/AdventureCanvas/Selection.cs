using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] public SelectionData selectionData;

    [SerializeField] private Sprite DefaultBelt;
    [SerializeField] private Sprite MouseOverBelt;
    [SerializeField] private Sprite MouseDownBelt;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = MouseOverBelt;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = DefaultBelt;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = MouseDownBelt;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = DefaultBelt;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerExit(eventData);
        transform.parent.parent.GetComponent<Quest>().DropDownQuestCard();
        
        transform.parent.parent.parent.parent.GetComponent<SceneController>().TransitionToContents(selectionData);
    }
}
