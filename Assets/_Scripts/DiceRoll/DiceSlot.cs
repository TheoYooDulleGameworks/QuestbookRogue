using System;
using UnityEngine;

public abstract class DiceSlot : MonoBehaviour
{
    public virtual event Action OnConfirmed;
    public virtual void SetSlotComponents(ContentSO contentData, int refValueIndex, SlotSO slotData)
    {

    }

    public virtual bool CheckConfirmed()
    {
        OnConfirmed?.Invoke();
        return false;
    }
}