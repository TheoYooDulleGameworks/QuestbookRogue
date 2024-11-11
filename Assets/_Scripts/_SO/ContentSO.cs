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

    public ContentType contentType;
    [SerializeField] public GameObject contentTemplate;

    // Common //
    public Sprite backgroundImage;

    // Image Content //
    public string questTitle;
    public Sprite questImage;
    public Sprite questSeal;
    public bool isThereCancelButton = true;
    public bool isFreeCancel = false;

    // Description Content //
    [TextArea] public string bodyText;
    [TextArea] public string cancelText;

    // Action Content //
    public Sprite actionImage;
    public Sprite actionBelt;
    public string actionTitle;
    public List<SlotSet> actionRequestSlots1Row;
    public List<SlotSet> actionRequestSlots2Row;
    [TextArea] public string actionRewardText;
    public bool isThereProceedButton = true;
    public bool isFreeAction = false;
    public CombatOptionSet combatOptionSet;

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
    public int combatOptionAmount;
}

public enum DiceSlotType
{
    Single,
    Multi,
    Pay,
}

public enum CombatOptionType
{
    Attack,
    DamageDown,
    ArmorDown,
}