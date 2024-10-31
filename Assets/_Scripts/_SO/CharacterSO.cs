using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/MetaProgressions/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] public Sprite characterCard;
    [SerializeField] public Sprite characterLvContainer;
    [SerializeField] public string characterName;
    [SerializeField] public int defaultHp;
    [SerializeField] public int defaultSp;
    [SerializeField] public int defaultCoin;
    [SerializeField] public int defaultProvision;

    [SerializeField] public List<DiceSO> defaultDices;
    [SerializeField] public List<RelicSO> defaultRelics;
}
