using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : Singleton<PopUpManager>
{
    [SerializeField] private RectTransform adventurePanel;

    [SerializeField] private GameObject effectPopUpPrefab; // Upper Position
    [SerializeField] private GameObject damagePopUpPrefab; // Lower Position
    [SerializeField] private GameObject blockPopUpPrefab; // Lower Position

    [SerializeField] private List<Sprite> attackSprites;
    [SerializeField] private Sprite blockSprite;
    [SerializeField] private List<Sprite> damageReduceSprites;
    [SerializeField] private List<Sprite> armorReduceSprites;

    public void AttackPopUp(RectTransform targetPosition, int attackAmount)
    {
        GameObject popUp = Instantiate(damagePopUpPrefab);

        if (attackAmount > 20)
        {
            attackAmount = 0;
        }
        popUp.GetComponentInChildren<Image>().sprite = attackSprites[attackAmount];

        popUp.transform.SetParent(adventurePanel, false);
        popUp.transform.position = targetPosition.transform.position;
    }

    public void BlockPopUp(RectTransform targetPosition)
    {
        GameObject popUp = Instantiate(blockPopUpPrefab);

        popUp.GetComponentInChildren<Image>().sprite = blockSprite;

        popUp.transform.SetParent(adventurePanel, false);
        popUp.transform.position = targetPosition.transform.position;
    }

    public void DamageReducePopUp(RectTransform targetPosition, int optionAmount)
    {
        GameObject popUp = Instantiate(effectPopUpPrefab);

        if (optionAmount > 9)
        {
            optionAmount = 0;
        }
        popUp.GetComponentInChildren<Image>().sprite = damageReduceSprites[optionAmount];

        popUp.transform.SetParent(adventurePanel, false);
        popUp.transform.position = targetPosition.transform.position;
    }

    public void ArmorReducePopUp(RectTransform targetPosition, int optionAmount)
    {
        GameObject popUp = Instantiate(effectPopUpPrefab);

        if (optionAmount > 9)
        {
            optionAmount = 0;
        }
        popUp.GetComponentInChildren<Image>().sprite = armorReduceSprites[optionAmount];

        popUp.transform.SetParent(adventurePanel, false);
        popUp.transform.position = targetPosition.transform.position;
    }
}
