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
    public Sprite backgroundImage;

    // Image Content //
    [Header("__________ IMAGE _______________________________________________________________")]
    public string questTitle;
    public Sprite questImage;
    public Sprite combatHittedImage;
    public Sprite questSeal;

    [Header("__________ BUTTON BOOL _______________________________________________________________")]
    public bool isThereCancelButton = true;
    public bool isFreeCancel = false;

    // Description Content //
    [Header("__________ DESCRIPTION _______________________________________________________________")]
    [TextArea] public string bodyText;
    [TextArea] public string cancelText;

    // Action Content //
    [Header("__________ ACTION _______________________________________________________________")]
    public string actionTitle;
    public Sprite actionImage;
    public Sprite actionBelt;
    [TextArea] public string actionRewardText;
    public List<SlotSet> actionRequestSlots1Row;
    public List<SlotSet> actionRequestSlots2Row;

    [Header("__________ BUTTON BOOL _______________________________________________________________")]
    public bool isThereProceedButton = true;
    public bool isFreeAction = false;

    [Header("__________ COMBAT _______________________________________________________________")]
    public List<CombatOptionSet> combatOptionSets;

    [Header("__________ REWARD _______________________________________________________________")]
    public Sprite rewardBelt;
    public string rewardTitle;
    public List<RewardSet> rewardObjects;
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
    public int rewardAmount;
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