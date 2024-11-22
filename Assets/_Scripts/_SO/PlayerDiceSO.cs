using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDicesSO", menuName = "Scriptable Objects/PlayerAssets/PlayerDicesSO")]
public class PlayerDiceSO : ScriptableObject
{
    [Header("Player's Dices")]
    public StatusValue StrDice;
    public StatusValue AgiDice;
    public StatusValue IntDice;
    public StatusValue WilDice;

    [Header("UIDice Datasets")]
    public DiceSO StrDice_UI;
    public DiceSO AgiDice_UI;
    public DiceSO IntDice_UI;
    public DiceSO WilDice_UI;

    [Header("RollDice Datasets")]
    public GameObject StrDice_Roll;
    public GameObject AgiDice_Roll;
    public GameObject IntDice_Roll;
    public GameObject WilDice_Roll;
}