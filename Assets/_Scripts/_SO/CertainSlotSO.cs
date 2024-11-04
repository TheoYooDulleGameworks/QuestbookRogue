using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CertainSlotSO", menuName = "Scriptable Objects/Slots/CertainSlotSO")]
public class CertainSlotSO : SlotSO
{
    [Header("Settings")]
    public List<DiceType> requestDiceTypes;
    public List<int> requestDiceValues;

    [Header("Sprite Sources")]
    public Sprite defaultSlotSprite;
    public Sprite checkingSlotSprite;
    public Sprite succeedSlotSprite;
}
