using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DiceTabIndex : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    private Image myImage;

    [SerializeField] public bool isCurrentTab = true;

    [SerializeField] public Sprite currentTabSprite;
    [SerializeField] public Sprite mouseOverTabSprite;
    [SerializeField] public Sprite mouseDownTabSprite;
    [SerializeField] public Sprite defaultTabSprite;

    public event Action OnDiceTabClicked;

    private void Awake()
    {
        myImage = GetComponent<Image>();
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

        OnDiceTabClicked?.Invoke();
    }

    public void ActivateDiceTab()
    {
        isCurrentTab = true;
        myImage.sprite = currentTabSprite;
    }

    public void DeActivateDiceTab()
    {
        isCurrentTab = false;
        myImage.sprite = defaultTabSprite;
    }
}
