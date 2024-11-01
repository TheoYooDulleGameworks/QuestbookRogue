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
    public GameObject StrNormalDice_UI;
    public GameObject StrAdvancedDice_UI;
    public GameObject DexNormalDice_UI;
    public GameObject DexAdvancedDice_UI;
    public GameObject IntNormalDice_UI;
    public GameObject IntAdvancedDice_UI;
    public GameObject WilNormalDice_UI;
    public GameObject WilAdvancedDice_UI;

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