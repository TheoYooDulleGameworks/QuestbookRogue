using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContentSO", menuName = "Scriptable Objects/Quests/ContentSO")]
public class ContentSO : ScriptableObject
{
    public enum ContentType
    {
        Blank,
        Image,
        Description,
        Action,
    }

    [Header("__________ COMMON _______________________________________________________________")]
    public ContentType contentType;
    [SerializeField] public GameObject contentTemplate;

    // Image Content //
    [Header("__________ IMAGE _______________________________________________________________")]
    public bool isFreeCancel = false;

    // Description Content //
    [Header("__________ DESCRIPTION _______________________________________________________________")]
    [TextArea] public string bodyText;

    // Action Content //
    [Header("__________ ACTION _______________________________________________________________")]
    public string actionTitle;
    public List<SlotSet> actionRequestSlots1Row;
    public List<SlotSet> actionRequestSlots2Row;

    [Header("__________ COMBAT _______________________________________________________________")]
    public List<CombatOptionSet> combatOptionSets;

    [Header("__________ COMBAT REWARD _______________________________________________________________")]
    public ContentSO combatEpilogue;
    public List<RewardSet> combatRewards;

    [Header("__________ EVENT REWARD _______________________________________________________________")]
    public Sprite eventEpilogueImage;
    public ContentSO eventEpilogue;
    public List<RewardSet> eventRewards;
}

[System.Serializable]
public class SlotSet
{
    [SerializeField] public SlotSO RequestSlot;
    [SerializeField] public DiceSlotType DiceSlotType;
    [SerializeField] public int RequestValue;
}

[System.Serializable]
public class RewardSet
{
    public RewardSO rewardData;
    public RewardTier rewardTier;
    public int rewardAmount;
}

public enum RewardTier
{
    Neutral,
    Common,
    Rare,
    Legendary,
    Unique,
    Secret,
    Cursed,
}

[System.Serializable]
public class CombatOptionSet
{
    public CombatOptionType combatOptionType;
    public OptionModifyType optionModifyType;
    public int optionAmount;
}

public enum DiceSlotType
{
    Single,
    Just,
    Multi,
    Pay,
}

public enum CombatOptionType
{
    Attack,
    DamageModify,
    ArmorModify,
}

public enum OptionModifyType
{
    None,
    Minus,
    Plus,
}