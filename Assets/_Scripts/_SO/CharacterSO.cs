using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/MetaProgressions/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    [Header("Profile")]
    public PlayerCharacter playerCharacter;
    public Sprite characterImage;
    public Sprite hittedImage;
    public Sprite characterLvContainer;
    public string characterName;

    [Header("Status")]
    public int defaultHp;
    public int defaultGold;
    public int defaultProvision;

    [Header("Dices")]
    public int startingStrength;
    public int startingAgility;
    public int startingIntelligence;
    public int startingWillpower;

    [Header("Skills")]
    public SkillSO startingMeterSkill;
    public int maxSignaturePoint;
    public Sprite dePointIcon;
    public Sprite acPointIcon;
    public List<SkillSO> startingSkills;

    [Header("Relics")]
    public List<RelicSO> startingRelics;
}

public enum PlayerCharacter
{
    None,
    Rogue,
    Warrior,
    Mage,
    Knight,
    Warden,
    Sorcerer,
    Priestess,
    Sellsword,
    Champion,
    Ranger,
    Bard,
    Druid,
    Shaman,
    Tinkerer,
    Shapeshifter,
    Guardian,
    Watcher,
    Weaver,
    Enchantress,
    Prophet,
}