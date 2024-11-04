using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private PlayerStatusSO playerStatus;
    [SerializeField] private PlayerDiceSO playerDice;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestAddValue();
        }
    }

    private void TestAddValue()
    {
        playerDice.StrNormalDice.AddValue(1);
        playerDice.DexAdvancedDice.AddValue(1);
        playerDice.IntNormalDice.AddValue(1);
        playerDice.WilNormalDice.AddValue(1);

        playerStatus.Coin.AddValue(1);
        playerStatus.Provision.AddValue(1);

        playerStatus.currentHp.AddValue(1);
        playerStatus.maxHp.AddValue(1);
        playerStatus.currentSp.AddValue(1);
        playerStatus.maxSp.AddValue(1);
        playerStatus.currentXp.AddValue(1);
        playerStatus.Lv.AddValue(1);
    }
}
