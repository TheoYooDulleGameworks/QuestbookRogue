using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleSlotSO", menuName = "Scriptable Objects/Slots/SingleSlotSO")]
public class SingleSlotSO : SlotSO
{
    [Header("Settings")]
    public List<DiceType> requestDiceTypes;
    public int singleDiceValue;

    [Header("Sprite Sources")]
    public Sprite defaultSlotSprite;
    public Sprite checkingSlotSprite;
    public Sprite succeedSlotSprite;
}
