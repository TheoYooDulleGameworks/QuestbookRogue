using UnityEngine;

[CreateAssetMenu(fileName = "RewardSO", menuName = "Scriptable Objects/Items/RewardSO")]
public class RewardSO : ScriptableObject
{
    public RewardType rewardType;
    public ResourceRewardType resourceRewardType;

    public Sprite defaultSprite;
    public Sprite hoverSprite;
}

public enum RewardType
{
    Resource,
    Skill,
    Relic,
}

public enum ResourceRewardType
{
    Coin,
    Provision,
    Xp,
    Hp,
    Sp,
}