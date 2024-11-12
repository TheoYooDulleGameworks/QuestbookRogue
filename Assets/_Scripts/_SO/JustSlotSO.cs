using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JustSlotSO", menuName = "Scriptable Objects/Slots/JustSlotSO")]
public class JustSlotSO : SlotSO
{
    [Header("Settings")]
    public List<DiceType> requestDiceTypes;
    public List<int> justDiceValues;

    [Header("Sprite Sources")]
    public Sprite defaultSlotSprite;
    public Sprite checkingSlotSprite;
    public Sprite succeedSlotSprite;
}
