using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class CombatImageContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;
    [SerializeField] private EnemySO enemyData = null;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI questTitleTMPro = null;
    [SerializeField] private RectTransform questImageRect = null;
    [SerializeField] private RectTransform questSealRect = null;
    [SerializeField] private RectTransform turnEndButtonRect = null;
    [SerializeField] private RectTransform cancelButtonRect = null;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultTurnEndButton;
    [SerializeField] private Sprite defaultCancelButton;
    [SerializeField] private Sprite defaultFreeCancelButton;

    [Header("Stats")]
    public EnemyStatusSO enemyStatus;
    [SerializeField] private RectTransform healthRect;
    [SerializeField] private RectTransform armorRect;
    [SerializeField] private RectTransform damageRect;

    [Header("Stats Sources")]
    [SerializeField] private RectTransform currentHp10thRect;
    [SerializeField] private RectTransform currentHp1thRect;
    [SerializeField] private List<Sprite> currentHpNumbers = new List<Sprite>();

    [SerializeField] private RectTransform currentArmor10thRect;
    [SerializeField] private RectTransform currentArmor1thRect;
    [SerializeField] private List<Sprite> currentArmorNumbers = new List<Sprite>();

    [SerializeField] private RectTransform currentDamage10thRect;
    [SerializeField] private RectTransform currentDamage1thRect;
    [SerializeField] private List<Sprite> currentDamageNumbers = new List<Sprite>();



    private void OnEnable()
    {
        enemyStatus.currentHp.OnValueChanged += UpdateEnemyHpUI;
        enemyStatus.currentArmor.OnValueChanged += UpdateEnemyArmorUI;
        enemyStatus.currentDamage.OnValueChanged += UpdateEnemyDamageUI;
    }

    private void OnDisable()
    {
        enemyStatus.currentHp.OnValueChanged -= UpdateEnemyHpUI;
        enemyStatus.currentArmor.OnValueChanged -= UpdateEnemyArmorUI;
        enemyStatus.currentDamage.OnValueChanged -= UpdateEnemyDamageUI;
    }

    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        contentData = _contentData;
        enemyData = _questData.enemyData;

        enemyStatus.maxHp.Value = enemyData.defaultHp;
        enemyStatus.currentHp.Value = enemyData.defaultHp;

        enemyStatus.maxArmor.Value = enemyData.defaultArmor;
        enemyStatus.currentArmor.Value = enemyData.defaultArmor;

        enemyStatus.maxDamage.Value = enemyData.defaultDamage;
        enemyStatus.currentDamage.Value = enemyData.defaultDamage;

        questTitleTMPro.text = contentData.questTitle;
        questImageRect.GetComponent<Image>().sprite = contentData.questImage;
        questSealRect.GetComponent<Image>().sprite = contentData.questSeal;

        turnEndButtonRect.GetComponent<Image>().sprite = defaultTurnEndButton;

        if (contentData.isThereCancelButton)
        {
            cancelButtonRect.gameObject.SetActive(true);
            cancelButtonRect.GetComponent<CancelButton>().currentQuestData = _questData;

            if (contentData.isFreeCancel)
            {
                cancelButtonRect.GetComponent<Image>().sprite = defaultFreeCancelButton;
                cancelButtonRect.GetComponent<CancelButton>().isFreeToCancel = true;
            }
            else
            {
                cancelButtonRect.GetComponent<Image>().sprite = defaultCancelButton;
                cancelButtonRect.GetComponent<CancelButton>().isFreeToCancel = false;
            }
        }

        UpdateEnemyHpUI();
        UpdateEnemyArmorUI();
        UpdateEnemyDamageUI();
    }

    public void FlipOnContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        contentCanvas.localEulerAngles = new Vector3(-2f, -90f, -2f);

        contentCanvas.DOKill();
        contentCanvas.DOScale(Vector3.one, 0.2f);
        contentCanvas.DORotate(Vector3.zero, 0.2f);
    }

    public void FlipOffContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.localScale = Vector3.one;
        contentCanvas.localEulerAngles = Vector3.zero;

        contentCanvas.DOKill();
        contentCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.2f);
        contentCanvas.DORotate(new Vector3(2f, 90f, 2f), 0.2f);
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }



    // Stats UI Update //

    private void UpdateEnemyHpUI()
    {
        int currentHpValue = enemyStatus.currentHp.Value;
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
    }

    private void UpdateEnemyArmorUI()
    {
        int currentArmorValue = enemyStatus.currentArmor.Value;
        int currentArmorValue10th = currentArmorValue / 10;
        int currentArmorValue1th = currentArmorValue % 10;

        if (currentArmorValue >= 10)
        {
            if (currentArmor10thRect.gameObject != null)
            {
                currentArmor10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (currentArmor10thRect.gameObject != null)
            {
                currentArmor10thRect.gameObject.SetActive(false);
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

    private void UpdateEnemyDamageUI()
    {
        int currentDamageValue = enemyStatus.currentDamage.Value;
        int currentDamageValue10th = currentDamageValue / 10;
        int currentDamageValue1th = currentDamageValue % 10;

        if (currentDamageValue >= 10)
        {
            if (currentDamage10thRect.gameObject != null)
            {
                currentDamage10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (currentDamage10thRect.gameObject != null)
            {
                currentDamage10thRect.gameObject.SetActive(false);
            }
        }

        switch (currentDamageValue10th)
        {
            case 0:
                currentDamage10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                currentDamage10thRect.GetComponent<Image>().sprite = currentDamageNumbers[1];
                break;
            case 2:
                currentDamage10thRect.GetComponent<Image>().sprite = currentDamageNumbers[2];
                break;
            case 3:
                currentDamage10thRect.GetComponent<Image>().sprite = currentDamageNumbers[3];
                break;
            case 4:
                currentDamage10thRect.GetComponent<Image>().sprite = currentDamageNumbers[4];
                break;
            case 5:
                currentDamage10thRect.GetComponent<Image>().sprite = currentDamageNumbers[5];
                break;
            case 6:
                currentDamage10thRect.GetComponent<Image>().sprite = currentDamageNumbers[6];
                break;
            case 7:
                currentDamage10thRect.GetComponent<Image>().sprite = currentDamageNumbers[7];
                break;
            case 8:
                currentDamage10thRect.GetComponent<Image>().sprite = currentDamageNumbers[8];
                break;
            case 9:
                currentDamage10thRect.GetComponent<Image>().sprite = currentDamageNumbers[9];
                break;
            default:
                break;
        }
        switch (currentDamageValue1th)
        {
            case 0:
                currentDamage1thRect.GetComponent<Image>().sprite = currentDamageNumbers[0];
                break;
            case 1:
                currentDamage1thRect.GetComponent<Image>().sprite = currentDamageNumbers[1];
                break;
            case 2:
                currentDamage1thRect.GetComponent<Image>().sprite = currentDamageNumbers[2];
                break;
            case 3:
                currentDamage1thRect.GetComponent<Image>().sprite = currentDamageNumbers[3];
                break;
            case 4:
                currentDamage1thRect.GetComponent<Image>().sprite = currentDamageNumbers[4];
                break;
            case 5:
                currentDamage1thRect.GetComponent<Image>().sprite = currentDamageNumbers[5];
                break;
            case 6:
                currentDamage1thRect.GetComponent<Image>().sprite = currentDamageNumbers[6];
                break;
            case 7:
                currentDamage1thRect.GetComponent<Image>().sprite = currentDamageNumbers[7];
                break;
            case 8:
                currentDamage1thRect.GetComponent<Image>().sprite = currentDamageNumbers[8];
                break;
            case 9:
                currentDamage1thRect.GetComponent<Image>().sprite = currentDamageNumbers[9];
                break;
            default:
                break;
        }
    }
}
