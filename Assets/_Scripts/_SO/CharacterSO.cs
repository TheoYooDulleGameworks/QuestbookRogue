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
    public int defaultSp;
    public int defaultCoin;
    public int defaultProvision;

    [Header("Dices")]
    public int startingStrength;
    public int startingDexterity;
    public int startingIntelligence;
    public int startingWillpower;
    public int startingAdvancedStrength;
    public int startingAdvancedDexterity;
    public int startingAdvancedIntelligence;
    public int startingAdvancedWillpower;


    [Header("Items")]
    public List<SkillSO> startingSkills;
    public List<RelicSO> startingRelics;
}
