using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] public CharacterSO playerCharacter;

    [SerializeField] private RectTransform characterImageRect;
    [SerializeField] private RectTransform hittedImageRect;
    [SerializeField] private RectTransform playerLvContainer;
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
        characterImageRect.GetComponent<Image>().sprite = playerCharacter.characterImage;
        hittedImageRect.GetComponent<Image>().sprite = playerCharacter.hittedImage;
        playerLvContainer.GetComponent<Image>().sprite = playerCharacter.characterLvContainer;
        playerName.text = playerCharacter.characterName;
    }

    private void FirstSetPlayerStatus()
    {
        playerStatus.Lv.Value = 1;
        playerStatus.currentXp.Value = 0;

        playerStatus.maxHp.Value = playerCharacter.defaultHp;
        playerStatus.currentHp.Value = playerStatus.maxHp.Value;

        playerStatus.currentArmor.Value = 0;

        playerStatus.Gold.Value = playerCharacter.defaultGold;
        playerStatus.Provision.Value = playerCharacter.defaultProvision;

    }

    private void FirstSetPlayerDices()
    {
        playerDices.StrDice.Value = playerCharacter.startingStrength;

        playerDices.AgiDice.Value = playerCharacter.startingAgility;

        playerDices.IntDice.Value = playerCharacter.startingIntelligence;

        playerDices.WilDice.Value = playerCharacter.startingWillpower;
    }

    private void FirstSetPlayerPaths()
    {
        playerPaths.ResetPaths();
        playerPaths.FirstSetPaths();
    }
}
