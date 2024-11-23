using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Objects/Items/SkillSO")]
public class SkillSO : ScriptableObject
{
    [Header("Notes")]
    public string skillName;
    public string skillDescription;

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite defaultHoverSprite;

    [Header("Costs")]
    public SkillCostType costType;

    [Header("Casts")]
    public SkillCastType castType;

    [Header("Outputs")]
    public SkillOutputType outputType;
    public DiceSO diceOutput;
}

public enum SkillCostType
{

}

public enum SkillCastType
{

}

public enum SkillOutputType
{
    None,
    Dice,
    DiceModify,
    StaminaPoint,
    ExclusivePoint,
    Effect,
}