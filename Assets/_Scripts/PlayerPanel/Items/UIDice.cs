using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("DiceData")]
    public DiceSO diceData;

    [Header("Components")]
    [SerializeField] private RectTransform diceImage;

    public void SetDiceData()
    {
        diceImage.GetComponent<Image>().sprite = diceData.defaultSprite;
    }

    public void DestroyUIDice()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        diceImage.GetComponent<Image>().sprite = diceData.defaultHoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       diceImage.GetComponent<Image>().sprite = diceData.defaultSprite;
    }
}