using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Objects/Items/SkillSO")]
public class SkillSO : ScriptableObject
{
    [Header("__________ NOTES _______________________________________________________________")]
    public string skillName;
    public Sprite defaultSprite;
    public Sprite defaultHoverSprite;
    [TextArea] public string skillDescription;
    [TextArea] public string costDescription;
    [TextArea] public string castDescription;

    [Header("__________ COOLDOWN _______________________________________________________________")]
    public SkillLimitType skillLimitType;
    public SkillCooldownType skillCooldownType;
    public bool isCooldown = false;

    [Header("__________ COST _______________________________________________________________")]
    public SkillCostType costType;
    public int costValue;
    public int aboveConditionValue;
    public List<DiceType> costDiceTypes;

    [Header("__________ CAST _______________________________________________________________")]
    public SkillCastType castType;
    public int castValue;
    public int modifyValue;
    public List<DiceType> castDiceType;
    public List<DiceCastSet> diceCastSets;
}

public enum SkillLimitType
{
    Common,
    Combat,
    Event,
}

public enum SkillCooldownType
{
    None,
    Turn,
    Season,
}

public enum SkillCostType
{
    DiceCost,
    SignaturePointCost,
}

[System.Serializable]
public class DiceCostSet
{
    public List<DiceType> diceTypes;
    public int aboveConditionValue;
}

public enum SkillCastType
{
    NewDice,
    FixedDice,
    ReRoll,
    Modify,
    SignaturePoint,
    Effect,
}

[System.Serializable]
public class DiceCastSet
{
    public DiceType diceType;
    public int diceValue;
}