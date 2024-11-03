using UnityEngine;
using UnityEngine.UI;

public class RollDiceSlot : MonoBehaviour
{
    [SerializeField] private int aboveConditionValue;

    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite checkSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DiceValueCheck"))
        {
            RollDice rollDice = collision.GetComponentInParent<RollDice>();

            if (rollDice.IsAboveValue(aboveConditionValue))
            {
                Debug.Log("Succeed!!!");
            }
            else
            {
                Debug.Log("Fail..");
            }
        }
    }
}
