using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiSlotSO", menuName = "Scriptable Objects/Slots/MultiSlotSO")]
public class MultiSlotSO : SlotSO
{
    [Header("Settings")]
    public List<DiceType> requestDiceTypes;

    [Header("Sprite Sources")]
    public Sprite defaultSlotSprite;
    public Sprite checkingSlotSprite;
    public Sprite succeedSlotSprite;

    [Header("Value Sprite Dataset")]
    public List<Sprite> maxValueSprites;
    public List<Sprite> currentValueSprites;
}