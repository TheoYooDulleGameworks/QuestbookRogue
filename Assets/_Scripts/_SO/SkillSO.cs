using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Objects/Items/SkillSO")]
public class SkillSO : ScriptableObject
{
    [Header("Notes")]
    public string skillName;
    public Sprite defaultSprite;
    public Sprite defaultHoverSprite;
    [TextArea] public string skillDescription;
    [TextArea] public string costDescription;
    [TextArea] public string castDescription;

    [Header("Cooldown")]
    public SkillLimitType skillLimitType;
    public SkillCooldownType skillCooldownType;
    public bool isCooldown = false;

    [Header("COST")]
    public SkillCostType costType;
    public List<DiceType> singleDiceTypes;
    public int aboveConditionValue;

    [Header("CAST")]
    public SkillCastType castType;
    public List<DiceType> newDiceSets;
    public List<FixedDiceSet> fixedDiceSets;
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
    SingleDiceCost,
    MultiDiceCost,
    ResourceCost,
}

public enum SkillCastType
{
    NewDice,
    FixedDice,
    ReRoll,
    Modify,
    StaminaPoint,
    SignaturePoint,
    Effect,
}

[System.Serializable]
public class FixedDiceSet
{
    public DiceType fixedDiceType;
    public int fixedDiceValue;
}