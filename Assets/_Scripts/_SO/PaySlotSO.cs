using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaySlotSO", menuName = "Scriptable Objects/Slots/PaySlotSO")]
public class PaySlotSO : SlotSO
{
    [Header("Settings")]
    public PaymentType paymentType;

    [Header("Sprite Sources")]
    public Sprite paymentIconSprite;
    public Sprite defaultSlotSprite;
    public Sprite succeedSlotSprite;
    public Sprite failSlotSprite;

    [Header("Value Sprite Dataset")]
    public List<Sprite> payValueSprites;
}
