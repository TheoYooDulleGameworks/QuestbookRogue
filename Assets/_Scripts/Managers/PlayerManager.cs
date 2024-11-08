using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] public CharacterSO playerCharacter;

    [SerializeField] private Image playerCard;
    [SerializeField] private Image playerLvContainer;
    [SerializeField] private TextMeshProUGUI playerName;

    // Player Assets //
    [SerializeField] public PlayerStatusSO playerStatus;
    [SerializeField] public PlayerDiceSO playerDices;
    [SerializeField] public PlayerPathSO playerPaths;

    protected override void Awake()
    {
        base.Awake();

        FirstSetPlayerProfile();
        FirstSetPlayerStatus();
        FirstSetPlayerDices();
        FirstSetPlayerPaths();
    }

    private void FirstSetPlayerProfile()
    {
        playerCard.sprite = playerCharacter.characterCard;
        playerLvContainer.sprite = playerCharacter.characterLvContainer;
        playerName.text = playerCharacter.characterName;
    }

    private void FirstSetPlayerStatus()
    {
        playerStatus.Lv.Value = 1;
        playerStatus.currentXp.Value = 0;

        playerStatus.maxHp.Value = playerCharacter.defaultHp;
        playerStatus.currentHp.Value = playerStatus.maxHp.Value;

        playerStatus.maxSp.Value = playerCharacter.defaultSp;
        playerStatus.currentSp.Value = playerStatus.maxSp.Value;

        playerStatus.currentArmor.Value = 0;

        playerStatus.Coin.Value = playerCharacter.defaultCoin;
        playerStatus.Provision.Value = playerCharacter.defaultProvision;

    }

    private void FirstSetPlayerDices()
    {
        playerDices.StrNormalDice.Value = playerCharacter.startingStrength;
        playerDices.StrAdvancedDice.Value = playerCharacter.startingAdvancedStrength;
        
        playerDices.DexNormalDice.Value = playerCharacter.startingDexterity;
        playerDices.DexAdvancedDice.Value = playerCharacter.startingAdvancedDexterity;

        playerDices.IntNormalDice.Value = playerCharacter.startingIntelligence;
        playerDices.IntAdvancedDice.Value = playerCharacter.startingAdvancedIntelligence;

        playerDices.WilNormalDice.Value = playerCharacter.startingWillpower;
        playerDices.WilAdvancedDice.Value = playerCharacter.startingAdvancedWillpower;
    }

    private void FirstSetPlayerPaths()
    {
        playerPaths.FirstSetPaths();
    }
}
