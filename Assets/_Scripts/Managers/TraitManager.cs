using UnityEngine;

public class TraitManager : Singleton<TraitManager>
{
    [Header("Player Character")]
    public PlayerCharacter playerCharacter;

    [Header("Casting")]
    [SerializeField] private PlayerStatusSO playerStatus;
    [SerializeField] private PlayerDiceSO playerDices;
    [SerializeField] private PlayerSkillSO playerSkills;

    // [Header("__________ PLAYER TRAITS _______________________________________________________________")]

    // [Header("__________ WORLD TRAITS _______________________________________________________________")]


    // LOGIC //

    protected override void Awake()
    {
        base.Awake();
        ResetTraits();
    }

    public void SetPlayerCharacter(PlayerCharacter _playerCharacter)
    {
        playerCharacter = _playerCharacter;
    }

    private void ResetTraits()
    {
        playerCharacter = PlayerCharacter.None;
    }

    // PLAYER TRAITS //

    public void CHAR_Rogue_ReRollSuccessTrait()
    {
        playerSkills.signaturePoint.AddClampedValue(1, 0, playerSkills.maxSignaturePoint);
    }

    // WORLD TRAITS //

    
}
