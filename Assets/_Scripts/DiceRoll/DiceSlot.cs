using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class DiceSlot : MonoBehaviour
{
    public virtual event Action OnConfirmed;
    public virtual void SetSlotComponents(ContentSO contentData, int requestValue, SlotSO slotData)
    {

    }

    public bool QuestIsOver = false;

    public virtual bool CheckConfirmed()
    {
        OnConfirmed?.Invoke();
        return false;
    }

    public virtual void DeleteKeepingDices()
    {
        
    }
}