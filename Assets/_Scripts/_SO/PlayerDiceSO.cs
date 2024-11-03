using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDicesSO", menuName = "Scriptable Objects/PlayerAssets/PlayerDicesSO")]
public class PlayerDiceSO : ScriptableObject
{
    [Header("Player's Dices")]
    public StatusValue StrNormalDice;
    public StatusValue StrAdvancedDice;
    public StatusValue DexNormalDice;
    public StatusValue DexAdvancedDice;
    public StatusValue IntNormalDice;
    public StatusValue IntAdvancedDice;
    public StatusValue WilNormalDice;
    public StatusValue WilAdvancedDice;

    [Header("UIDice Datasets")]
    public DiceSO StrNormalDice_UI;
    public DiceSO StrAdvancedDice_UI;
    public DiceSO DexNormalDice_UI;
    public DiceSO DexAdvancedDice_UI;
    public DiceSO IntNormalDice_UI;
    public DiceSO IntAdvancedDice_UI;
    public DiceSO WilNormalDice_UI;
    public DiceSO WilAdvancedDice_UI;

    [Header("RollDice Datasets")]
    public GameObject StrNormalDice_Roll;
    public GameObject StrAdvancedDice_Roll;
    public GameObject DexNormalDice_Roll;
    public GameObject DexAdvancedDice_Roll;
    public GameObject IntNormalDice_Roll;
    public GameObject IntAdvancedDice_Roll;
    public GameObject WilNormalDice_Roll;
    public GameObject WilAdvancedDice_Roll;
}