using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] public SelectionSO selectionData;

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
        transform.parent.parent.GetComponent<Quest>().DeActivateSelection();
        
        SceneController.Instance.TransitionToContents(selectionData.parentQuestData, selectionData);

        // transform.parent.parent.parent -> Selet Scene으로 접근해서 Grid Layout 내에 있는 'Quest'의 Index에 접근하기
        // (새로 List 파서 Grid 내에 생성될 때마다 Index 대응해서 List에 추가하는 로직이 있어야 할 듯)
        // 
    }
}
