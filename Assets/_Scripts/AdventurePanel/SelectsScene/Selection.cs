using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Selection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] public QuestSO questData;
    [SerializeField] public int questIndexNumber  = -1;

    [SerializeField] public Quest parentQuest;
    [SerializeField] public RectTransform parentRectTransform;

    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite clickSprite;


    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = defaultSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = clickSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = defaultSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (parentQuest.inTransition)
        {
            return;
        }

        parentQuest.inTransition = true;
        SceneController.Instance.DeActivateSelects();

        parentRectTransform.DOKill();
        parentRectTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f).OnComplete(() =>
        {
            parentRectTransform.DOScale(Vector3.one, 0.2f).OnComplete(() =>
            {
                SceneController.Instance.TransitionToContents(questData, questIndexNumber);
            });
        });
    }
}
