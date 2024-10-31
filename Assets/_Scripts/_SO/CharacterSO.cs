using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/MetaProgressions/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public Sprite characterCard;
    public Sprite characterLvContainer;
    public string characterName;
    public int defaultHp;
    public int defaultSp;
    public int defaultCoin;
    public int defaultProvision;

    public int startingStrength;
    public int startingDexterity;
    public int startingIntelligence;
    public int startingWillpower;
    public int startingAdvancedStrength;
    public int startingAdvancedDexterity;
    public int startingAdvancedIntelligence;
    public int startingAdvancedWillpower;

    public List<SkillSO> startingSkills;
    public List<RelicSO> startingRelics;
    public List<ConsumableSO> startingConsumables;
}
