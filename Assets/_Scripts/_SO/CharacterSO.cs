using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/MetaProgressions/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    [Header("Profile")]
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

    [Header("Items")]
    public List<SkillSO> startingSkills;
    public List<RelicSO> startingRelics;
}
