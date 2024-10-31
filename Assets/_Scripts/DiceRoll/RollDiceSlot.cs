using UnityEngine;
using UnityEngine.EventSystems;

public class RollDiceSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int aboveConditionValue;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<RollDice>().IsAboveValue(aboveConditionValue) == true)
            {
                Debug.Log("Succeed!");
            }
            else
            {
                Debug.Log("Fail...");
            }
        }
    }
}
