using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image myImage;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (myImage != null)
        {
            myImage.color = new Color32(255, 164, 0, 255);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (myImage != null)
        {
            myImage.color = Color.white;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (myImage != null)
        {
            myImage.color = Color.white;
        }
        
        this.transform.parent.parent.parent.GetComponent<Quest>().DeActivateContent();
    }
}
