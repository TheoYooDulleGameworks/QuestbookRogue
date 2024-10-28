using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public CharacterSO playerCharacter;

    [SerializeField] private Image playerCard;
    [SerializeField] private Image playerLvContainer;
    [SerializeField] private TextMeshProUGUI playerName;

    // Player Assets //
    [SerializeField] public PlayerStatusSO playerStatus;


    private void Awake()
    {
        SetPlayerProfile();
        SetPlayerStatus();
    }

    private void SetPlayerProfile()
    {
        playerCard.sprite = playerCharacter.characterCard;
        playerLvContainer.sprite = playerCharacter.characterLvContainer;
        playerName.text = playerCharacter.characterName;
    }

    private void SetPlayerStatus()
    {
        playerStatus.Lv.Value = 1;
        playerStatus.currentXp.Value = 0;

        playerStatus.maxHp.Value = playerCharacter.defaultHp;
        playerStatus.currentHp.Value = playerStatus.maxHp.Value;

        playerStatus.maxSp.Value = playerCharacter.defaultSp;
        playerStatus.currentSp.Value = playerStatus.maxSp.Value;

        playerStatus.Coin.Value = playerCharacter.defaultCoin;
        playerStatus.Provision.Value = playerCharacter.defaultProvision;
    }
}
