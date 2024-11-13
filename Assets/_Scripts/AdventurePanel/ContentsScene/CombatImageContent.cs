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

    [Header("Enemy Status")]
    public EnemyStatusSO enemyStatus;

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
    [SerializeField] private RectTransform healthRect;
    [SerializeField] private RectTransform armorRect;
    [SerializeField] private RectTransform damageRect;

    [Header("Stats Sources")]
    [SerializeField] private RectTransform currentHealth10thRect;
    [SerializeField] private RectTransform currentHealth1thRect;
    [SerializeField] private List<Sprite> currentHealthNumbers = new List<Sprite>();

    [SerializeField] private RectTransform currentArmor10thRect;
    [SerializeField] private RectTransform currentArmor1thRect;
    [SerializeField] private List<Sprite> currentArmorNumbers = new List<Sprite>();

    [SerializeField] private RectTransform currentDamage10thRect;
    [SerializeField] private RectTransform currentDamage1thRect;
    [SerializeField] private List<Sprite> currentDamageNumbers = new List<Sprite>();



    private void OnEnable()
    {
        enemyStatus.currentHealth.OnValueChanged += UpdateEnemyHealthUI;
        enemyStatus.currentArmor.OnValueChanged += UpdateEnemyArmorUI;
        enemyStatus.currentDamage.OnValueChanged += UpdateEnemyDamageUI;
    }

    private void OnDisable()
    {
        enemyStatus.currentHealth.OnValueChanged -= UpdateEnemyHealthUI;
        enemyStatus.currentArmor.OnValueChanged -= UpdateEnemyArmorUI;
        enemyStatus.currentDamage.OnValueChanged -= UpdateEnemyDamageUI;
    }

    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        contentData = _contentData;
        enemyData = _questData.enemyData;

        enemyStatus.maxHealth.Value = enemyData.defaultHealth;
        enemyStatus.currentHealth.Value = enemyData.defaultHealth;

        enemyStatus.maxArmor.Value = enemyData.defaultArmor;
        enemyStatus.currentArmor.Value = enemyData.defaultArmor;

        enemyStatus.maxDamage.Value = enemyData.defaultDamage;
        enemyStatus.currentDamage.Value = enemyData.defaultDamage;

        if (enemyData.defaultHealth == 0)
        {
            healthRect.gameObject.SetActive(false);
        }
        if (enemyData.defaultArmor == 0)
        {
            armorRect.gameObject.SetActive(false);
        }
        if (enemyData.defaultDamage == 0)
        {
            damageRect.gameObject.SetActive(false);
        }

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

        UpdateEnemyHealthUI();
        UpdateEnemyArmorUI();
        UpdateEnemyDamageUI();
    }

    public void FlipOnContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        contentCanvas.localScale = new Vector3(0.75f, 0.5f, 0.5f);
        contentCanvas.localEulerAngles = new Vector3(-90f, 12f, 12f);

        contentCanvas.DOKill();
        contentCanvas.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f);
        contentCanvas.DORotate(new Vector3(4f, 12f, -2f), 0.25f);
        contentCanvas.GetComponent<CanvasGroup>().DOFade(1f, 0.25f).OnComplete(() =>
        {
            contentCanvas.DOKill();
            contentCanvas.DOScale(Vector3.one, 0.25f);
            contentCanvas.DORotate(Vector3.zero, 0.25f);
        });
    }

    public void FlipOffContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.localEulerAngles = Vector3.zero;
        contentCanvas.localScale = Vector3.one;

        contentCanvas.DOKill();
        contentCanvas.DORotate(new Vector3(-75f, -12f, -6f), 0.25f);
        contentCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.25f);
        contentCanvas.GetComponent<CanvasGroup>().DOFade(0, 0.25f);
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }



    // Stats UI Update //

    private void UpdateEnemyHealthUI()
    {
        int currentHealthValue = enemyStatus.currentHealth.Value;
        int currentHealthValue10th = currentHealthValue / 10;
        int currentHealthValue1th = currentHealthValue % 10;

        if (currentHealthValue >= 10)
        {
            if (currentHealth10thRect.gameObject != null)
            {
                currentHealth10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (currentHealth10thRect.gameObject != null)
            {
                currentHealth10thRect.gameObject.SetActive(false);
            }
        }

        switch (currentHealthValue10th)
        {
            case 0:
                currentHealth10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                currentHealth10thRect.GetComponent<Image>().sprite = currentHealthNumbers[1];
                break;
            case 2:
                currentHealth10thRect.GetComponent<Image>().sprite = currentHealthNumbers[2];
                break;
            case 3:
                currentHealth10thRect.GetComponent<Image>().sprite = currentHealthNumbers[3];
                break;
            case 4:
                currentHealth10thRect.GetComponent<Image>().sprite = currentHealthNumbers[4];
                break;
            case 5:
                currentHealth10thRect.GetComponent<Image>().sprite = currentHealthNumbers[5];
                break;
            case 6:
                currentHealth10thRect.GetComponent<Image>().sprite = currentHealthNumbers[6];
                break;
            case 7:
                currentHealth10thRect.GetComponent<Image>().sprite = currentHealthNumbers[7];
                break;
            case 8:
                currentHealth10thRect.GetComponent<Image>().sprite = currentHealthNumbers[8];
                break;
            case 9:
                currentHealth10thRect.GetComponent<Image>().sprite = currentHealthNumbers[9];
                break;
            default:
                break;
        }
        switch (currentHealthValue1th)
        {
            case 0:
                currentHealth1thRect.GetComponent<Image>().sprite = currentHealthNumbers[0];
                break;
            case 1:
                currentHealth1thRect.GetComponent<Image>().sprite = currentHealthNumbers[1];
                break;
            case 2:
                currentHealth1thRect.GetComponent<Image>().sprite = currentHealthNumbers[2];
                break;
            case 3:
                currentHealth1thRect.GetComponent<Image>().sprite = currentHealthNumbers[3];
                break;
            case 4:
                currentHealth1thRect.GetComponent<Image>().sprite = currentHealthNumbers[4];
                break;
            case 5:
                currentHealth1thRect.GetComponent<Image>().sprite = currentHealthNumbers[5];
                break;
            case 6:
                currentHealth1thRect.GetComponent<Image>().sprite = currentHealthNumbers[6];
                break;
            case 7:
                currentHealth1thRect.GetComponent<Image>().sprite = currentHealthNumbers[7];
                break;
            case 8:
                currentHealth1thRect.GetComponent<Image>().sprite = currentHealthNumbers[8];
                break;
            case 9:
                currentHealth1thRect.GetComponent<Image>().sprite = currentHealthNumbers[9];
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
