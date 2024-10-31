using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDicesSO", menuName = "Scriptable Objects/PlayerAssets/PlayerDicesSO")]
public class PlayerDicesSO : ScriptableObject
{
    public StatusValue StrengthDices;
    public StatusValue AdvancedStrengthDices;
    public StatusValue DexterityDices;
    public StatusValue AdvancedDexterityDices;
    public StatusValue IntelligenceDices;
    public StatusValue AdvancedIntelligenceDices;
    public StatusValue WillpowerDices;
    public StatusValue AdvancedWillpowerDices;

    public StatusValue curseDices;
}