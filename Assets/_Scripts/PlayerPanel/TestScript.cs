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
        playerDice.StrDice.AddValue(1);
        playerDice.AgiDice.AddValue(1);
        playerDice.IntDice.AddValue(1);
        playerDice.WilDice.AddValue(1);

        playerStatus.Gold.AddValue(1);
        playerStatus.Provision.AddValue(1);

        playerStatus.maxHp.AddValue(1);
        playerStatus.currentHp.AddClampedValue(1, 0, playerStatus.maxHp.Value);

        playerStatus.currentArmor.AddValue(1);

        playerStatus.currentXp.AddValue(1);
    }
}
