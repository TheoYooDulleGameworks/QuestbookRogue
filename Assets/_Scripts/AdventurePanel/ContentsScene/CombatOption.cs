using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CombatOption : MonoBehaviour
{
    [Header("Method")]
    [SerializeField] private EnemyStatusSO enemyStatus;
    [SerializeField] private bool isClosed = false;

    [Header("Assigned Option")]
    public CombatOptionType combatOptionType;
    [SerializeField] private OptionModifyType optionModifyType;
    [SerializeField] private int optionAmount;

    [Header("Assigned Sprite")]
    [SerializeField] private Sprite deBox;
    [SerializeField] private Sprite acBox;
    [SerializeField] private Sprite hoverBox;
    [SerializeField] private Sprite clickBox;
    [SerializeField] private Sprite closedBox;

    [SerializeField] private Sprite assigned_deIcon;
    [SerializeField] private Sprite assigned_acIcon;
    [SerializeField] private Sprite assigned_clickIcon;
    [SerializeField] private Sprite assigned_closedIcon;
    [SerializeField] private List<Sprite> assigned_deValues;
    [SerializeField] private List<Sprite> assigned_acValues;
    [SerializeField] private List<Sprite> assigned_clickValues;
    [SerializeField] private List<Sprite> assigned_closedValues;

    [Header("Components")]
    public RectTransform iconRect;
    public RectTransform markRect;
    public RectTransform value10thRect;
    public RectTransform value1thRect;

    [Header("Sprite Sources")]
    [SerializeField] private Sprite deAttackIcon;
    [SerializeField] private Sprite acAttackIcon;
    [SerializeField] private Sprite clickAttackIcon;
    [SerializeField] private Sprite closedAttackIcon;
    [SerializeField] private List<Sprite> deAttackValues;
    [SerializeField] private List<Sprite> acAttackValues;
    [SerializeField] private List<Sprite> clickAttackValues;
    [SerializeField] private List<Sprite> closedAttackValues;
    [SerializeField] private List<Sprite> attackBoxes;

    [SerializeField] private Sprite deDamageIcon;
    [SerializeField] private Sprite acDamageIcon;
    [SerializeField] private Sprite clickDamageIcon;
    [SerializeField] private Sprite closedDamageIcon;
    [SerializeField] private List<Sprite> deDamageValues;
    [SerializeField] private List<Sprite> acDamageValues;
    [SerializeField] private List<Sprite> clickDamageValues;
    [SerializeField] private List<Sprite> closedDamageValues;
    [SerializeField] private List<Sprite> damageBoxes;

    [SerializeField] private Sprite deArmorIcon;
    [SerializeField] private Sprite acArmorIcon;
    [SerializeField] private Sprite clickArmorIcon;
    [SerializeField] private Sprite closedArmorIcon;
    [SerializeField] private List<Sprite> deArmorValues;
    [SerializeField] private List<Sprite> acArmorValues;
    [SerializeField] private List<Sprite> clickArmorValues;
    [SerializeField] private List<Sprite> closedArmorValues;
    [SerializeField] private List<Sprite> armorBoxes;

    public void ResetOptionUI()
    {
        isClosed = false;
        DefaultOptionUI();
    }

    public void ApplyEffect()
    {
        if (isClosed)
        {
            return;
        }

        CombatImageContent enemyProfile = transform.parent.parent.parent.parent.GetComponentInChildren<CombatImageContent>();

        switch (combatOptionType)
        {
            case CombatOptionType.Attack:
                if (optionModifyType == OptionModifyType.None)
                {
                    int initailAttackAmount = optionAmount;
                    int attackAmount = Mathf.Clamp(initailAttackAmount -= enemyStatus.currentArmor.Value, 0, optionAmount);

                    if (attackAmount > 0)
                    {
                        VfxManager.Instance.BasicAttackVfx(enemyProfile.GetComponent<RectTransform>());
                        PopUpManager.Instance.AttackPopUp(enemyProfile.GetComponent<RectTransform>(), attackAmount);
                        enemyProfile.HittedGetDamage();
                    }
                    else if (attackAmount <= 0)
                    {
                        VfxManager.Instance.BlockAttackVfx(enemyProfile.GetComponent<RectTransform>());
                        PopUpManager.Instance.BlockPopUp(enemyProfile.GetComponent<RectTransform>());
                        enemyProfile.HittedBlock();
                    }

                    enemyStatus.currentHealth.RemoveClampedValue(attackAmount, 0, enemyStatus.maxHealth.Value);

                    DefaultOptionUI();
                    DeleteDices();
                }
                break;
            case CombatOptionType.DamageModify:
                if (optionModifyType == OptionModifyType.Minus)
                {
                    if (optionAmount > 0)
                    {
                        PopUpManager.Instance.DamageReducePopUp(enemyProfile.GetComponent<RectTransform>(), optionAmount);
                    }

                    enemyStatus.currentDamage.RemoveClampedValue(optionAmount, 0, enemyStatus.maxDamage.Value);
                    isClosed = true;
                    ClosedOptionUI();
                }
                break;
            case CombatOptionType.ArmorModify:
                if (optionModifyType == OptionModifyType.Minus)
                {
                    if (optionAmount > 0)
                    {
                        PopUpManager.Instance.ArmorReducePopUp(enemyProfile.GetComponent<RectTransform>(), optionAmount);
                    }

                    enemyStatus.currentArmor.RemoveClampedValue(optionAmount, 0, enemyStatus.maxArmor.Value);
                    isClosed = true;
                    ClosedOptionUI();
                }
                break;
            default:
                break;
        }
    }

    private void DeleteDices()
    {
        List<DiceSlot> diceSlots = new List<DiceSlot>(transform.parent.parent.GetComponentsInChildren<DiceSlot>());
        foreach (DiceSlot slot in diceSlots)
        {
            slot.DeleteKeepingDices();
        }
    }

    public void SetOptionComponents(CombatOptionType _combatOptionType, OptionModifyType _optionModifyType, int _optionAmount)
    {
        // Assign Datas //

        combatOptionType = _combatOptionType;
        optionModifyType = _optionModifyType;
        optionAmount = _optionAmount;

        // Assign Sprites //

        if (_combatOptionType == CombatOptionType.Attack)
        {
            deBox = attackBoxes[0];
            acBox = attackBoxes[1];
            hoverBox = attackBoxes[2];
            clickBox = attackBoxes[3];
            closedBox = attackBoxes[4];

            assigned_deIcon = deAttackIcon;
            assigned_acIcon = acAttackIcon;
            assigned_clickIcon = clickAttackIcon;
            assigned_closedIcon = closedAttackIcon;
            assigned_deValues = deAttackValues;
            assigned_acValues = acAttackValues;
            assigned_clickValues = clickAttackValues;
            assigned_closedValues = closedAttackValues;
        }
        else if (_combatOptionType == CombatOptionType.DamageModify)
        {
            deBox = damageBoxes[0];
            acBox = damageBoxes[1];
            hoverBox = damageBoxes[2];
            clickBox = damageBoxes[3];
            closedBox = damageBoxes[4];

            assigned_deIcon = deDamageIcon;
            assigned_acIcon = acDamageIcon;
            assigned_clickIcon = clickDamageIcon;
            assigned_closedIcon = closedDamageIcon;
            assigned_deValues = deDamageValues;
            assigned_acValues = acDamageValues;
            assigned_clickValues = clickDamageValues;
            assigned_closedValues = closedDamageValues;
        }
        else if (_combatOptionType == CombatOptionType.ArmorModify)
        {
            deBox = armorBoxes[0];
            acBox = armorBoxes[1];
            hoverBox = armorBoxes[2];
            clickBox = armorBoxes[3];
            closedBox = armorBoxes[4];

            assigned_deIcon = deArmorIcon;
            assigned_acIcon = acArmorIcon;
            assigned_clickIcon = clickArmorIcon;
            assigned_closedIcon = closedArmorIcon;
            assigned_deValues = deArmorValues;
            assigned_acValues = acArmorValues;
            assigned_clickValues = clickArmorValues;
            assigned_closedValues = closedArmorValues;
        }

        DefaultOptionUI();
    }

    public void DefaultOptionUI()
    {
        if (isClosed)
        {
            return;
        }

        GetComponent<Image>().sprite = deBox;
        iconRect.GetComponent<Image>().sprite = assigned_deIcon;

        int amountValue = optionAmount;
        int amountValue10th = amountValue / 10;
        int amountValue1th = amountValue % 10;

        if (optionModifyType == OptionModifyType.None)
        {
            if (markRect.gameObject != null)
            {
                markRect.gameObject.SetActive(false);
            }
        }
        else
        {
            if (optionModifyType == OptionModifyType.Minus)
            {
                if (markRect.gameObject != null)
                {
                    markRect.gameObject.SetActive(true);
                }

                markRect.GetComponent<Image>().sprite = assigned_deValues[10];
            }
            else if (optionModifyType == OptionModifyType.Plus)
            {
                if (markRect.gameObject != null)
                {
                    markRect.gameObject.SetActive(true);
                }

                markRect.GetComponent<Image>().sprite = assigned_deValues[11];
            }
        }

        if (amountValue >= 10)
        {
            if (value10thRect.gameObject != null)
            {
                value10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (value10thRect.gameObject != null)
            {
                value10thRect.gameObject.SetActive(false);
            }
        }

        switch (amountValue10th)
        {
            case 0:
                value10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                value10thRect.GetComponent<Image>().sprite = assigned_deValues[1];
                break;
            case 2:
                value10thRect.GetComponent<Image>().sprite = assigned_deValues[2];
                break;
            case 3:
                value10thRect.GetComponent<Image>().sprite = assigned_deValues[3];
                break;
            case 4:
                value10thRect.GetComponent<Image>().sprite = assigned_deValues[4];
                break;
            case 5:
                value10thRect.GetComponent<Image>().sprite = assigned_deValues[5];
                break;
            case 6:
                value10thRect.GetComponent<Image>().sprite = assigned_deValues[6];
                break;
            case 7:
                value10thRect.GetComponent<Image>().sprite = assigned_deValues[7];
                break;
            case 8:
                value10thRect.GetComponent<Image>().sprite = assigned_deValues[8];
                break;
            case 9:
                value10thRect.GetComponent<Image>().sprite = assigned_deValues[9];
                break;
            default:
                break;
        }
        switch (amountValue1th)
        {
            case 0:
                value1thRect.GetComponent<Image>().sprite = assigned_deValues[0];
                break;
            case 1:
                value1thRect.GetComponent<Image>().sprite = assigned_deValues[1];
                break;
            case 2:
                value1thRect.GetComponent<Image>().sprite = assigned_deValues[2];
                break;
            case 3:
                value1thRect.GetComponent<Image>().sprite = assigned_deValues[3];
                break;
            case 4:
                value1thRect.GetComponent<Image>().sprite = assigned_deValues[4];
                break;
            case 5:
                value1thRect.GetComponent<Image>().sprite = assigned_deValues[5];
                break;
            case 6:
                value1thRect.GetComponent<Image>().sprite = assigned_deValues[6];
                break;
            case 7:
                value1thRect.GetComponent<Image>().sprite = assigned_deValues[7];
                break;
            case 8:
                value1thRect.GetComponent<Image>().sprite = assigned_deValues[8];
                break;
            case 9:
                value1thRect.GetComponent<Image>().sprite = assigned_deValues[9];
                break;
            default:
                break;
        }
    }

    public void ActivatedOptionUI()
    {
        if (isClosed)
        {
            return;
        }

        GetComponent<Image>().sprite = acBox;
        iconRect.GetComponent<Image>().sprite = assigned_acIcon;

        int amountValue = optionAmount;
        int amountValue10th = amountValue / 10;
        int amountValue1th = amountValue % 10;

        if (optionModifyType == OptionModifyType.None)
        {
            if (markRect.gameObject != null)
            {
                markRect.gameObject.SetActive(false);
            }
        }
        else
        {
            if (optionModifyType == OptionModifyType.Minus)
            {
                if (markRect.gameObject != null)
                {
                    markRect.gameObject.SetActive(true);
                }

                markRect.GetComponent<Image>().sprite = assigned_acValues[10];
            }
            else if (optionModifyType == OptionModifyType.Plus)
            {
                if (markRect.gameObject != null)
                {
                    markRect.gameObject.SetActive(true);
                }

                markRect.GetComponent<Image>().sprite = assigned_acValues[11];
            }
        }

        if (amountValue >= 10)
        {
            if (value10thRect.gameObject != null)
            {
                value10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (value10thRect.gameObject != null)
            {
                value10thRect.gameObject.SetActive(false);
            }
        }

        switch (amountValue10th)
        {
            case 0:
                value10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[1];
                break;
            case 2:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[2];
                break;
            case 3:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[3];
                break;
            case 4:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[4];
                break;
            case 5:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[5];
                break;
            case 6:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[6];
                break;
            case 7:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[7];
                break;
            case 8:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[8];
                break;
            case 9:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[9];
                break;
            default:
                break;
        }
        switch (amountValue1th)
        {
            case 0:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[0];
                break;
            case 1:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[1];
                break;
            case 2:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[2];
                break;
            case 3:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[3];
                break;
            case 4:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[4];
                break;
            case 5:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[5];
                break;
            case 6:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[6];
                break;
            case 7:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[7];
                break;
            case 8:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[8];
                break;
            case 9:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[9];
                break;
            default:
                break;
        }
    }

    public void HoverOptionUI()
    {
        if (isClosed)
        {
            return;
        }

        GetComponent<Image>().sprite = hoverBox;
        iconRect.GetComponent<Image>().sprite = assigned_acIcon;

        int amountValue = optionAmount;
        int amountValue10th = amountValue / 10;
        int amountValue1th = amountValue % 10;

        if (optionModifyType == OptionModifyType.None)
        {
            if (markRect.gameObject != null)
            {
                markRect.gameObject.SetActive(false);
            }
        }
        else
        {
            if (optionModifyType == OptionModifyType.Minus)
            {
                if (markRect.gameObject != null)
                {
                    markRect.gameObject.SetActive(true);
                }

                markRect.GetComponent<Image>().sprite = assigned_acValues[10];
            }
            else if (optionModifyType == OptionModifyType.Plus)
            {
                if (markRect.gameObject != null)
                {
                    markRect.gameObject.SetActive(true);
                }

                markRect.GetComponent<Image>().sprite = assigned_acValues[11];
            }
        }

        if (amountValue >= 10)
        {
            if (value10thRect.gameObject != null)
            {
                value10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (value10thRect.gameObject != null)
            {
                value10thRect.gameObject.SetActive(false);
            }
        }

        switch (amountValue10th)
        {
            case 0:
                value10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[1];
                break;
            case 2:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[2];
                break;
            case 3:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[3];
                break;
            case 4:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[4];
                break;
            case 5:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[5];
                break;
            case 6:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[6];
                break;
            case 7:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[7];
                break;
            case 8:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[8];
                break;
            case 9:
                value10thRect.GetComponent<Image>().sprite = assigned_acValues[9];
                break;
            default:
                break;
        }
        switch (amountValue1th)
        {
            case 0:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[0];
                break;
            case 1:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[1];
                break;
            case 2:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[2];
                break;
            case 3:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[3];
                break;
            case 4:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[4];
                break;
            case 5:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[5];
                break;
            case 6:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[6];
                break;
            case 7:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[7];
                break;
            case 8:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[8];
                break;
            case 9:
                value1thRect.GetComponent<Image>().sprite = assigned_acValues[9];
                break;
            default:
                break;
        }
    }

    public void ClickOptionUI()
    {
        if (isClosed)
        {
            return;
        }

        GetComponent<Image>().sprite = clickBox;
        iconRect.GetComponent<Image>().sprite = assigned_clickIcon;

        int amountValue = optionAmount;
        int amountValue10th = amountValue / 10;
        int amountValue1th = amountValue % 10;

        if (optionModifyType == OptionModifyType.None)
        {
            if (markRect.gameObject != null)
            {
                markRect.gameObject.SetActive(false);
            }
        }
        else
        {
            if (optionModifyType == OptionModifyType.Minus)
            {
                if (markRect.gameObject != null)
                {
                    markRect.gameObject.SetActive(true);
                }

                markRect.GetComponent<Image>().sprite = assigned_clickValues[10];
            }
            else if (optionModifyType == OptionModifyType.Plus)
            {
                if (markRect.gameObject != null)
                {
                    markRect.gameObject.SetActive(true);
                }

                markRect.GetComponent<Image>().sprite = assigned_clickValues[11];
            }
        }

        if (amountValue >= 10)
        {
            if (value10thRect.gameObject != null)
            {
                value10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (value10thRect.gameObject != null)
            {
                value10thRect.gameObject.SetActive(false);
            }
        }

        switch (amountValue10th)
        {
            case 0:
                value10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                value10thRect.GetComponent<Image>().sprite = assigned_clickValues[1];
                break;
            case 2:
                value10thRect.GetComponent<Image>().sprite = assigned_clickValues[2];
                break;
            case 3:
                value10thRect.GetComponent<Image>().sprite = assigned_clickValues[3];
                break;
            case 4:
                value10thRect.GetComponent<Image>().sprite = assigned_clickValues[4];
                break;
            case 5:
                value10thRect.GetComponent<Image>().sprite = assigned_clickValues[5];
                break;
            case 6:
                value10thRect.GetComponent<Image>().sprite = assigned_clickValues[6];
                break;
            case 7:
                value10thRect.GetComponent<Image>().sprite = assigned_clickValues[7];
                break;
            case 8:
                value10thRect.GetComponent<Image>().sprite = assigned_clickValues[8];
                break;
            case 9:
                value10thRect.GetComponent<Image>().sprite = assigned_clickValues[9];
                break;
            default:
                break;
        }
        switch (amountValue1th)
        {
            case 0:
                value1thRect.GetComponent<Image>().sprite = assigned_clickValues[0];
                break;
            case 1:
                value1thRect.GetComponent<Image>().sprite = assigned_clickValues[1];
                break;
            case 2:
                value1thRect.GetComponent<Image>().sprite = assigned_clickValues[2];
                break;
            case 3:
                value1thRect.GetComponent<Image>().sprite = assigned_clickValues[3];
                break;
            case 4:
                value1thRect.GetComponent<Image>().sprite = assigned_clickValues[4];
                break;
            case 5:
                value1thRect.GetComponent<Image>().sprite = assigned_clickValues[5];
                break;
            case 6:
                value1thRect.GetComponent<Image>().sprite = assigned_clickValues[6];
                break;
            case 7:
                value1thRect.GetComponent<Image>().sprite = assigned_clickValues[7];
                break;
            case 8:
                value1thRect.GetComponent<Image>().sprite = assigned_clickValues[8];
                break;
            case 9:
                value1thRect.GetComponent<Image>().sprite = assigned_clickValues[9];
                break;
            default:
                break;
        }
    }

    public void ClosedOptionUI()
    {
        isClosed = true;

        GetComponent<Image>().sprite = closedBox;
        iconRect.GetComponent<Image>().sprite = assigned_closedIcon;

        int amountValue = optionAmount;
        int amountValue10th = amountValue / 10;
        int amountValue1th = amountValue % 10;

        if (optionModifyType == OptionModifyType.None)
        {
            if (markRect.gameObject != null)
            {
                markRect.gameObject.SetActive(false);
            }
        }
        else
        {
            if (optionModifyType == OptionModifyType.Minus)
            {
                if (markRect.gameObject != null)
                {
                    markRect.gameObject.SetActive(true);
                }

                markRect.GetComponent<Image>().sprite = assigned_closedValues[10];
            }
            else if (optionModifyType == OptionModifyType.Plus)
            {
                if (markRect.gameObject != null)
                {
                    markRect.gameObject.SetActive(true);
                }

                markRect.GetComponent<Image>().sprite = assigned_closedValues[11];
            }
        }

        if (amountValue >= 10)
        {
            if (value10thRect.gameObject != null)
            {
                value10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (value10thRect.gameObject != null)
            {
                value10thRect.gameObject.SetActive(false);
            }
        }

        switch (amountValue10th)
        {
            case 0:
                value10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                value10thRect.GetComponent<Image>().sprite = assigned_closedValues[1];
                break;
            case 2:
                value10thRect.GetComponent<Image>().sprite = assigned_closedValues[2];
                break;
            case 3:
                value10thRect.GetComponent<Image>().sprite = assigned_closedValues[3];
                break;
            case 4:
                value10thRect.GetComponent<Image>().sprite = assigned_closedValues[4];
                break;
            case 5:
                value10thRect.GetComponent<Image>().sprite = assigned_closedValues[5];
                break;
            case 6:
                value10thRect.GetComponent<Image>().sprite = assigned_closedValues[6];
                break;
            case 7:
                value10thRect.GetComponent<Image>().sprite = assigned_closedValues[7];
                break;
            case 8:
                value10thRect.GetComponent<Image>().sprite = assigned_closedValues[8];
                break;
            case 9:
                value10thRect.GetComponent<Image>().sprite = assigned_closedValues[9];
                break;
            default:
                break;
        }
        switch (amountValue1th)
        {
            case 0:
                value1thRect.GetComponent<Image>().sprite = assigned_closedValues[0];
                break;
            case 1:
                value1thRect.GetComponent<Image>().sprite = assigned_closedValues[1];
                break;
            case 2:
                value1thRect.GetComponent<Image>().sprite = assigned_closedValues[2];
                break;
            case 3:
                value1thRect.GetComponent<Image>().sprite = assigned_closedValues[3];
                break;
            case 4:
                value1thRect.GetComponent<Image>().sprite = assigned_closedValues[4];
                break;
            case 5:
                value1thRect.GetComponent<Image>().sprite = assigned_closedValues[5];
                break;
            case 6:
                value1thRect.GetComponent<Image>().sprite = assigned_closedValues[6];
                break;
            case 7:
                value1thRect.GetComponent<Image>().sprite = assigned_closedValues[7];
                break;
            case 8:
                value1thRect.GetComponent<Image>().sprite = assigned_closedValues[8];
                break;
            case 9:
                value1thRect.GetComponent<Image>().sprite = assigned_closedValues[9];
                break;
            default:
                break;
        }
    }

}