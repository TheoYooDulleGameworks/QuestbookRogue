using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VitalUI : MonoBehaviour
{
    [SerializeField] private PlayerStatusSO playerStatus;


    [SerializeField] private RectTransform lv10thRect;
    [SerializeField] private RectTransform lv1thRect;
    [SerializeField] private List<Sprite> lvNumbers = new List<Sprite>();

    [SerializeField] private RectTransform currentXp10thRect;
    [SerializeField] private RectTransform currentXp1thRect;
    [SerializeField] private List<Sprite> currentXpNumbers = new List<Sprite>();

    [SerializeField] private RectTransform goalXp10thRect;
    [SerializeField] private RectTransform goalXp1thRect;
    [SerializeField] private List<Sprite> goalXpNumbers = new List<Sprite>();

    [SerializeField] private RectTransform currentHp10thRect;
    [SerializeField] private RectTransform currentHp1thRect;
    [SerializeField] private List<Sprite> currentHpNumbers = new List<Sprite>();

    [SerializeField] private RectTransform maxHp10thRect;
    [SerializeField] private RectTransform maxHp1thRect;
    [SerializeField] private List<Sprite> maxHpNumbers = new List<Sprite>();

    [SerializeField] private RectTransform armorIcon;
    [SerializeField] private RectTransform currentArmor10thRect;
    [SerializeField] private RectTransform currentArmor1thRect;
    [SerializeField] private List<Sprite> currentArmorNumbers = new List<Sprite>();

    private void Start()
    {
        GameManager.Instance.OnStagePhaseChanged += HandleStagePhaseChange;

        playerStatus.Lv.OnValueChanged += UpdateLvUI;
        playerStatus.currentXp.OnValueChanged += UpdateXpUI;

        playerStatus.currentHp.OnValueChanged += UpdateHpUI;
        playerStatus.maxHp.OnValueChanged += UpdateHpUI;

        playerStatus.currentArmor.OnValueChanged += UpdateArmorUI;

        SetDefaultStatus();
    }

    private void OnDestroy()
    {
        playerStatus.Lv.OnValueChanged -= UpdateLvUI;
        playerStatus.currentXp.OnValueChanged -= UpdateXpUI;

        playerStatus.currentHp.OnValueChanged -= UpdateHpUI;
        playerStatus.maxHp.OnValueChanged -= UpdateHpUI;

        playerStatus.currentArmor.OnValueChanged -= UpdateArmorUI;
    }

    private void HandleStagePhaseChange(StagePhase stagePhase)
    {
        if (stagePhase == StagePhase.DiceWaiting)
        {
            playerStatus.currentArmor.RemoveClampedValue(9999, 0, playerStatus.currentArmor.Value);
        }
    }

    private void SetDefaultStatus()
    {
        UpdateLvUI();
        UpdateXpUI();
        UpdateHpUI();
        UpdateArmorUI();
    }

    private void UpdateLvUI()
    {
        int lvValue = playerStatus.Lv.Value;
        int lvValue10th = lvValue / 10;
        int lvValue1th = lvValue % 10;

        if (lvValue >= 10)
        {
            if (lv10thRect.gameObject != null)
            {
                lv10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (lv10thRect.gameObject != null)
            {
                lv10thRect.gameObject.SetActive(false);
            }
        }

        switch (lvValue10th)
        {
            case 0:
                lv10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                lv10thRect.GetComponent<Image>().sprite = lvNumbers[1];
                break;
            case 2:
                lv10thRect.GetComponent<Image>().sprite = lvNumbers[2];
                break;
            case 3:
                lv10thRect.GetComponent<Image>().sprite = lvNumbers[3];
                break;
            case 4:
                lv10thRect.GetComponent<Image>().sprite = lvNumbers[4];
                break;
            case 5:
                lv10thRect.GetComponent<Image>().sprite = lvNumbers[5];
                break;
            case 6:
                lv10thRect.GetComponent<Image>().sprite = lvNumbers[6];
                break;
            case 7:
                lv10thRect.GetComponent<Image>().sprite = lvNumbers[7];
                break;
            case 8:
                lv10thRect.GetComponent<Image>().sprite = lvNumbers[8];
                break;
            case 9:
                lv10thRect.GetComponent<Image>().sprite = lvNumbers[9];
                break;
            default:
                break;
        }
        switch (lvValue1th)
        {
            case 0:
                lv1thRect.GetComponent<Image>().sprite = lvNumbers[0];
                break;
            case 1:
                lv1thRect.GetComponent<Image>().sprite = lvNumbers[1];
                break;
            case 2:
                lv1thRect.GetComponent<Image>().sprite = lvNumbers[2];
                break;
            case 3:
                lv1thRect.GetComponent<Image>().sprite = lvNumbers[3];
                break;
            case 4:
                lv1thRect.GetComponent<Image>().sprite = lvNumbers[4];
                break;
            case 5:
                lv1thRect.GetComponent<Image>().sprite = lvNumbers[5];
                break;
            case 6:
                lv1thRect.GetComponent<Image>().sprite = lvNumbers[6];
                break;
            case 7:
                lv1thRect.GetComponent<Image>().sprite = lvNumbers[7];
                break;
            case 8:
                lv1thRect.GetComponent<Image>().sprite = lvNumbers[8];
                break;
            case 9:
                lv1thRect.GetComponent<Image>().sprite = lvNumbers[9];
                break;
            default:
                break;
        }

        int goalXpValue = (lvValue * 5);
        int goalXpValue10th = goalXpValue / 10;
        int goalXpValue1th = goalXpValue % 10;

        if (goalXpValue >= 10)
        {
            if (goalXp10thRect.gameObject != null)
            {
                goalXp10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (goalXp10thRect.gameObject != null)
            {
                goalXp10thRect.gameObject.SetActive(false);
            }
        }

        switch (goalXpValue10th)
        {
            case 0:
                goalXp10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                goalXp10thRect.GetComponent<Image>().sprite = goalXpNumbers[1];
                break;
            case 2:
                goalXp10thRect.GetComponent<Image>().sprite = goalXpNumbers[2];
                break;
            case 3:
                goalXp10thRect.GetComponent<Image>().sprite = goalXpNumbers[3];
                break;
            case 4:
                goalXp10thRect.GetComponent<Image>().sprite = goalXpNumbers[4];
                break;
            case 5:
                goalXp10thRect.GetComponent<Image>().sprite = goalXpNumbers[5];
                break;
            case 6:
                goalXp10thRect.GetComponent<Image>().sprite = goalXpNumbers[6];
                break;
            case 7:
                goalXp10thRect.GetComponent<Image>().sprite = goalXpNumbers[7];
                break;
            case 8:
                goalXp10thRect.GetComponent<Image>().sprite = goalXpNumbers[8];
                break;
            case 9:
                goalXp10thRect.GetComponent<Image>().sprite = goalXpNumbers[9];
                break;
            default:
                break;
        }
        switch (goalXpValue1th)
        {
            case 0:
                goalXp1thRect.GetComponent<Image>().sprite = goalXpNumbers[0];
                break;
            case 1:
                goalXp1thRect.GetComponent<Image>().sprite = goalXpNumbers[1];
                break;
            case 2:
                goalXp1thRect.GetComponent<Image>().sprite = goalXpNumbers[2];
                break;
            case 3:
                goalXp1thRect.GetComponent<Image>().sprite = goalXpNumbers[3];
                break;
            case 4:
                goalXp1thRect.GetComponent<Image>().sprite = goalXpNumbers[4];
                break;
            case 5:
                goalXp1thRect.GetComponent<Image>().sprite = goalXpNumbers[5];
                break;
            case 6:
                goalXp1thRect.GetComponent<Image>().sprite = goalXpNumbers[6];
                break;
            case 7:
                goalXp1thRect.GetComponent<Image>().sprite = goalXpNumbers[7];
                break;
            case 8:
                goalXp1thRect.GetComponent<Image>().sprite = goalXpNumbers[8];
                break;
            case 9:
                goalXp1thRect.GetComponent<Image>().sprite = goalXpNumbers[9];
                break;
            default:
                break;
        }
    }

    private void UpdateXpUI()
    {
        int currentXpValue = playerStatus.currentXp.Value;
        int currentXpValue10th = currentXpValue / 10;
        int currentXpValue1th = currentXpValue % 10;

        if (currentXpValue >= 10)
        {
            if (currentXp10thRect.gameObject != null)
            {
                currentXp10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (currentXp10thRect.gameObject != null)
            {
                currentXp10thRect.gameObject.SetActive(false);
            }
        }

        switch (currentXpValue10th)
        {
            case 0:
                currentXp10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                currentXp10thRect.GetComponent<Image>().sprite = currentXpNumbers[1];
                break;
            case 2:
                currentXp10thRect.GetComponent<Image>().sprite = currentXpNumbers[2];
                break;
            case 3:
                currentXp10thRect.GetComponent<Image>().sprite = currentXpNumbers[3];
                break;
            case 4:
                currentXp10thRect.GetComponent<Image>().sprite = currentXpNumbers[4];
                break;
            case 5:
                currentXp10thRect.GetComponent<Image>().sprite = currentXpNumbers[5];
                break;
            case 6:
                currentXp10thRect.GetComponent<Image>().sprite = currentXpNumbers[6];
                break;
            case 7:
                currentXp10thRect.GetComponent<Image>().sprite = currentXpNumbers[7];
                break;
            case 8:
                currentXp10thRect.GetComponent<Image>().sprite = currentXpNumbers[8];
                break;
            case 9:
                currentXp10thRect.GetComponent<Image>().sprite = currentXpNumbers[9];
                break;
            default:
                break;
        }
        switch (currentXpValue1th)
        {
            case 0:
                currentXp1thRect.GetComponent<Image>().sprite = currentXpNumbers[0];
                break;
            case 1:
                currentXp1thRect.GetComponent<Image>().sprite = currentXpNumbers[1];
                break;
            case 2:
                currentXp1thRect.GetComponent<Image>().sprite = currentXpNumbers[2];
                break;
            case 3:
                currentXp1thRect.GetComponent<Image>().sprite = currentXpNumbers[3];
                break;
            case 4:
                currentXp1thRect.GetComponent<Image>().sprite = currentXpNumbers[4];
                break;
            case 5:
                currentXp1thRect.GetComponent<Image>().sprite = currentXpNumbers[5];
                break;
            case 6:
                currentXp1thRect.GetComponent<Image>().sprite = currentXpNumbers[6];
                break;
            case 7:
                currentXp1thRect.GetComponent<Image>().sprite = currentXpNumbers[7];
                break;
            case 8:
                currentXp1thRect.GetComponent<Image>().sprite = currentXpNumbers[8];
                break;
            case 9:
                currentXp1thRect.GetComponent<Image>().sprite = currentXpNumbers[9];
                break;
            default:
                break;
        }
    }

    private void UpdateHpUI()
    {
        int currentHpValue = playerStatus.currentHp.Value;
        int currentHpValue10th = currentHpValue / 10;
        int currentHpValue1th = currentHpValue % 10;

        if (currentHpValue >= 10)
        {
            if (currentHp10thRect.gameObject != null)
            {
                currentHp10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (currentHp10thRect.gameObject != null)
            {
                currentHp10thRect.gameObject.SetActive(false);
            }
        }

        switch (currentHpValue10th)
        {
            case 0:
                currentHp10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                currentHp10thRect.GetComponent<Image>().sprite = currentHpNumbers[1];
                break;
            case 2:
                currentHp10thRect.GetComponent<Image>().sprite = currentHpNumbers[2];
                break;
            case 3:
                currentHp10thRect.GetComponent<Image>().sprite = currentHpNumbers[3];
                break;
            case 4:
                currentHp10thRect.GetComponent<Image>().sprite = currentHpNumbers[4];
                break;
            case 5:
                currentHp10thRect.GetComponent<Image>().sprite = currentHpNumbers[5];
                break;
            case 6:
                currentHp10thRect.GetComponent<Image>().sprite = currentHpNumbers[6];
                break;
            case 7:
                currentHp10thRect.GetComponent<Image>().sprite = currentHpNumbers[7];
                break;
            case 8:
                currentHp10thRect.GetComponent<Image>().sprite = currentHpNumbers[8];
                break;
            case 9:
                currentHp10thRect.GetComponent<Image>().sprite = currentHpNumbers[9];
                break;
            default:
                break;
        }
        switch (currentHpValue1th)
        {
            case 0:
                currentHp1thRect.GetComponent<Image>().sprite = currentHpNumbers[0];
                break;
            case 1:
                currentHp1thRect.GetComponent<Image>().sprite = currentHpNumbers[1];
                break;
            case 2:
                currentHp1thRect.GetComponent<Image>().sprite = currentHpNumbers[2];
                break;
            case 3:
                currentHp1thRect.GetComponent<Image>().sprite = currentHpNumbers[3];
                break;
            case 4:
                currentHp1thRect.GetComponent<Image>().sprite = currentHpNumbers[4];
                break;
            case 5:
                currentHp1thRect.GetComponent<Image>().sprite = currentHpNumbers[5];
                break;
            case 6:
                currentHp1thRect.GetComponent<Image>().sprite = currentHpNumbers[6];
                break;
            case 7:
                currentHp1thRect.GetComponent<Image>().sprite = currentHpNumbers[7];
                break;
            case 8:
                currentHp1thRect.GetComponent<Image>().sprite = currentHpNumbers[8];
                break;
            case 9:
                currentHp1thRect.GetComponent<Image>().sprite = currentHpNumbers[9];
                break;
            default:
                break;
        }

        int maxHpValue = playerStatus.maxHp.Value;
        int maxHpValue10th = maxHpValue / 10;
        int maxHpValue1th = maxHpValue % 10;

        if (maxHpValue >= 10)
        {
            if (maxHp10thRect.gameObject != null)
            {
                maxHp10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (maxHp10thRect.gameObject != null)
            {
                maxHp10thRect.gameObject.SetActive(false);
            }
        }

        switch (maxHpValue10th)
        {
            case 0:
                maxHp10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                maxHp10thRect.GetComponent<Image>().sprite = maxHpNumbers[1];
                break;
            case 2:
                maxHp10thRect.GetComponent<Image>().sprite = maxHpNumbers[2];
                break;
            case 3:
                maxHp10thRect.GetComponent<Image>().sprite = maxHpNumbers[3];
                break;
            case 4:
                maxHp10thRect.GetComponent<Image>().sprite = maxHpNumbers[4];
                break;
            case 5:
                maxHp10thRect.GetComponent<Image>().sprite = maxHpNumbers[5];
                break;
            case 6:
                maxHp10thRect.GetComponent<Image>().sprite = maxHpNumbers[6];
                break;
            case 7:
                maxHp10thRect.GetComponent<Image>().sprite = maxHpNumbers[7];
                break;
            case 8:
                maxHp10thRect.GetComponent<Image>().sprite = maxHpNumbers[8];
                break;
            case 9:
                maxHp10thRect.GetComponent<Image>().sprite = maxHpNumbers[9];
                break;
            default:
                break;
        }
        switch (maxHpValue1th)
        {
            case 0:
                maxHp1thRect.GetComponent<Image>().sprite = maxHpNumbers[0];
                break;
            case 1:
                maxHp1thRect.GetComponent<Image>().sprite = maxHpNumbers[1];
                break;
            case 2:
                maxHp1thRect.GetComponent<Image>().sprite = maxHpNumbers[2];
                break;
            case 3:
                maxHp1thRect.GetComponent<Image>().sprite = maxHpNumbers[3];
                break;
            case 4:
                maxHp1thRect.GetComponent<Image>().sprite = maxHpNumbers[4];
                break;
            case 5:
                maxHp1thRect.GetComponent<Image>().sprite = maxHpNumbers[5];
                break;
            case 6:
                maxHp1thRect.GetComponent<Image>().sprite = maxHpNumbers[6];
                break;
            case 7:
                maxHp1thRect.GetComponent<Image>().sprite = maxHpNumbers[7];
                break;
            case 8:
                maxHp1thRect.GetComponent<Image>().sprite = maxHpNumbers[8];
                break;
            case 9:
                maxHp1thRect.GetComponent<Image>().sprite = maxHpNumbers[9];
                break;
            default:
                break;
        }
    }

    private void UpdateArmorUI()
    {
        int currentArmorValue = playerStatus.currentArmor.Value;
        int currentArmorValue10th = currentArmorValue / 10;
        int currentArmorValue1th = currentArmorValue % 10;

        if (currentArmorValue >= 10)
        {
            if (armorIcon.gameObject != null)
            {
                armorIcon.gameObject.SetActive(true);
            }
            if (currentArmor10thRect.gameObject != null)
            {
                currentArmor10thRect.gameObject.SetActive(true);
            }
            if (currentArmor1thRect.gameObject != null)
            {
                currentArmor1thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (armorIcon.gameObject != null)
            {
                armorIcon.gameObject.SetActive(true);
            }
            if (currentArmor10thRect.gameObject != null)
            {
                currentArmor10thRect.gameObject.SetActive(false);
            }
            if (currentArmor1thRect.gameObject != null)
            {
                currentArmor1thRect.gameObject.SetActive(true);
            }
        }

        switch (currentArmorValue10th)
        {
            case 0:
                currentArmor10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                currentArmor10thRect.GetComponent<Image>().sprite = currentArmorNumbers[1];
                break;
            case 2:
                currentArmor10thRect.GetComponent<Image>().sprite = currentArmorNumbers[2];
                break;
            case 3:
                currentArmor10thRect.GetComponent<Image>().sprite = currentArmorNumbers[3];
                break;
            case 4:
                currentArmor10thRect.GetComponent<Image>().sprite = currentArmorNumbers[4];
                break;
            case 5:
                currentArmor10thRect.GetComponent<Image>().sprite = currentArmorNumbers[5];
                break;
            case 6:
                currentArmor10thRect.GetComponent<Image>().sprite = currentArmorNumbers[6];
                break;
            case 7:
                currentArmor10thRect.GetComponent<Image>().sprite = currentArmorNumbers[7];
                break;
            case 8:
                currentArmor10thRect.GetComponent<Image>().sprite = currentArmorNumbers[8];
                break;
            case 9:
                currentArmor10thRect.GetComponent<Image>().sprite = currentArmorNumbers[9];
                break;
            default:
                break;
        }
        switch (currentArmorValue1th)
        {
            case 0:
                currentArmor1thRect.GetComponent<Image>().sprite = currentArmorNumbers[0];
                break;
            case 1:
                currentArmor1thRect.GetComponent<Image>().sprite = currentArmorNumbers[1];
                break;
            case 2:
                currentArmor1thRect.GetComponent<Image>().sprite = currentArmorNumbers[2];
                break;
            case 3:
                currentArmor1thRect.GetComponent<Image>().sprite = currentArmorNumbers[3];
                break;
            case 4:
                currentArmor1thRect.GetComponent<Image>().sprite = currentArmorNumbers[4];
                break;
            case 5:
                currentArmor1thRect.GetComponent<Image>().sprite = currentArmorNumbers[5];
                break;
            case 6:
                currentArmor1thRect.GetComponent<Image>().sprite = currentArmorNumbers[6];
                break;
            case 7:
                currentArmor1thRect.GetComponent<Image>().sprite = currentArmorNumbers[7];
                break;
            case 8:
                currentArmor1thRect.GetComponent<Image>().sprite = currentArmorNumbers[8];
                break;
            case 9:
                currentArmor1thRect.GetComponent<Image>().sprite = currentArmorNumbers[9];
                break;
            default:
                break;
        }
    }
}