using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfoTabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    private Image myImage;
    [SerializeField] private InfoTab thisTab;

    public bool isCurrentTab = true;

    [SerializeField] public Sprite currentTabSprite;
    [SerializeField] public Sprite mouseOverTabSprite;
    [SerializeField] public Sprite mouseDownTabSprite;
    [SerializeField] public Sprite defaultTabSprite;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public void ActivateButton()
    {
        isCurrentTab = true;
        myImage.sprite = currentTabSprite;
    }

    public void DeActivateButton()
    {
        isCurrentTab = false;
        myImage.sprite = defaultTabSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isCurrentTab)
        {
            if (myImage != null)
            {
                myImage.sprite = mouseOverTabSprite;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isCurrentTab)
        {
            if (myImage != null)
            {
                myImage.sprite = defaultTabSprite;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isCurrentTab)
        {
            if (myImage != null)
            {
                myImage.sprite = mouseDownTabSprite;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isCurrentTab)
        {
            return;
        }

        InfoTabController.Instance.SetActiveTab(thisTab);
    }
}
