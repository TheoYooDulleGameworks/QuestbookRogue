using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class CombatImageContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private QuestSO questData = null;
    [SerializeField] private ContentSO contentData = null;
    [SerializeField] private EnemySO enemyData = null;

    [Header("Status")]
    public EnemyStatusSO enemyStatus;
    public PlayerStatusSO playerStatus;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI questTitleTMPro = null;
    [SerializeField] private RectTransform questTitleBeltRect = null;
    [SerializeField] private RectTransform questImageRect = null;
    [SerializeField] private RectTransform hittedShader = null;
    [SerializeField] private RectTransform defeatedShader = null;
    [SerializeField] private RectTransform questSealRect = null;
    [SerializeField] private RectTransform turnEndButtonRect = null;
    [SerializeField] private RectTransform cancelButtonRect = null;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultTurnEndButton;
    [SerializeField] private Sprite defaultCancelButton;
    [SerializeField] private Sprite defaultFreeCancelButton;

    [Header("Stats")]
    [SerializeField] private RectTransform statsRect;
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

    [Header("Dead Enemy Sources")]
    [SerializeField] private Sprite deadEnemyImage = null;
    [SerializeField] private Sprite deadEnemySeal = null;
    [SerializeField] private Sprite deadEnemyBelt = null;

    [Header("Rewards")]
    [SerializeField] private GameObject rewardContentPrefab;



    private void OnEnable()
    {
        enemyStatus.currentHealth.OnValueChanged += UpdateEnemyHealthUI;
        enemyStatus.currentArmor.OnValueChanged += UpdateEnemyArmorUI;
        enemyStatus.currentDamage.OnValueChanged += UpdateEnemyDamageUI;

        GameManager.Instance.OnStagePhaseChanged += HandleStagePhaseChange;
    }

    private void OnDisable()
    {
        enemyStatus.currentHealth.OnValueChanged -= UpdateEnemyHealthUI;
        enemyStatus.currentArmor.OnValueChanged -= UpdateEnemyArmorUI;
        enemyStatus.currentDamage.OnValueChanged -= UpdateEnemyDamageUI;

        GameManager.Instance.OnStagePhaseChanged -= HandleStagePhaseChange;
    }

    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        questData = _questData;
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

        questTitleTMPro.text = _questData.questName;
        questImageRect.GetComponent<Image>().sprite = _questData.questMainImage;
        questSealRect.GetComponent<Image>().sprite = _questData.questSeal;

        hittedShader.GetComponent<Image>().sprite = _questData.hittedEnemyShader;
        deadEnemyImage = _questData.deadEnemyImage;

        turnEndButtonRect.GetComponent<Image>().sprite = defaultTurnEndButton;

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



        UpdateEnemyHealthUI();
        UpdateEnemyArmorUI();
        UpdateEnemyDamageUI();

        AudioManager.Instance.StartCombatBgm("Combat_Battle");
        AudioManager.Instance.PlaySfx("CombatBegin");
    }



    // On & Off //

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
        contentCanvas.GetComponent<CanvasGroup>().DOFade(0, 0.25f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }



    // Turn Management //

    private void HandleStagePhaseChange(StagePhase stagePhase)
    {
        if (stagePhase == StagePhase.DiceHolding)
        {
            // ENEMY TURN -> //

            StartCoroutine(EnemyAttackSequenceRoutine());
        }
        else if (stagePhase == StagePhase.DiceWaiting)
        {
            // PLAYER TURN -> //

            StartCoroutine(InitializeCombatContentsRoutine());
        }
    }

    private IEnumerator EnemyAttackSequenceRoutine()
    {
        int initailAttackAmount = enemyStatus.currentDamage.Value;
        int attackAmount = Mathf.Clamp(initailAttackAmount -= playerStatus.currentArmor.Value, 0, enemyStatus.currentDamage.Value);

        CharacterProfile characterProfile = transform.parent.parent.parent.GetComponentInChildren<CharacterProfile>();

        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        yield return new WaitForSeconds(1.5f);

        contentCanvas.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).OnComplete(() =>
        {
            contentCanvas.DOScale(Vector3.one, 0.3f);


            contentCanvas.DOAnchorPos(new Vector3(60, 12, 0), 0.3f).OnComplete(() =>
            {
                if (attackAmount > 0)
                {
                    VfxManager.Instance.PlayerBasicImpactVfx(characterProfile.GetComponent<RectTransform>());
                    PopUpManager.Instance.PlayerImpactPopUp(characterProfile.GetComponent<RectTransform>(), attackAmount);

                    characterProfile.HittedGetDamage();
                }
                else if (attackAmount <= 0)
                {
                    VfxManager.Instance.PlayerBasicBlockVfx(characterProfile.GetComponent<RectTransform>());
                    PopUpManager.Instance.PlayerBlockPopUp(characterProfile.GetComponent<RectTransform>());

                    characterProfile.HittedBlock();
                }

                playerStatus.currentHp.RemoveClampedValue(attackAmount, 0, playerStatus.maxHp.Value);

                contentCanvas.DOScale(new Vector3(1.35f, 1.35f, 1.35f), 0.1f);
                contentCanvas.DOAnchorPos(new Vector3(-120, 24, 0), 0.1f).OnComplete(() =>
                {
                    contentCanvas.DOScale(Vector3.one, 0.4f);
                    contentCanvas.DOAnchorPos(Vector3.zero, 0.4f);
                });
            });
        });

        yield return new WaitForSeconds(1.5f);

        GameManager.Instance.UpdateStagePhase(StagePhase.DiceWaiting);
    }

    private IEnumerator InitializeCombatContentsRoutine()
    {
        statsRect.DOKill();
        statsRect.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f).OnComplete(() =>
        {
            statsRect.DOScale(Vector3.one, 0.2f);

            enemyStatus.currentDamage.Value = enemyStatus.maxDamage.Value;
            enemyStatus.currentArmor.Value = enemyStatus.maxArmor.Value;
        });

        yield return new WaitForSeconds(1.5f);

        SceneController.Instance.ActivateRollDices();
    }



    // Stats Management //

    public void HittedGetDamage()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.DOShakeRotation(0.35f, 20, 0, 15);

        contentCanvas.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f).OnComplete(() =>
        {
            contentCanvas.DOScale(Vector3.one, 0.2f);
        });

        hittedShader.GetComponent<Image>().DOFade(1, 0.2f).OnComplete(() =>
        {
            hittedShader.GetComponent<Image>().DOFade(0, 0.1f);
        });
    }

    public void HittedBlock()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.DOShakeRotation(0.25f, 10, 0, 15);
        contentCanvas.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.1f).OnComplete(() =>
        {
            contentCanvas.DOScale(Vector3.one, 0.15f);
        });
    }




    // Complete Management //

    private void FlipOnDeadEnemy()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.localEulerAngles = Vector3.zero;
        contentCanvas.localScale = Vector3.one;

        contentCanvas.DOKill();
        contentCanvas.DORotate(new Vector3(-4f, -12f, 2f), 0.1f);
        defeatedShader.GetComponent<Image>().DOFade(1, 0.3f);
        contentCanvas.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f).OnComplete(() =>
        {
            contentCanvas.DOScale(Vector3.one, 0.2f);
            contentCanvas.DORotate(Vector3.zero, 0.2f).OnComplete(() =>
            {
                GameManager.Instance.UpdateStagePhase(StagePhase.Finishing);

                AudioManager.Instance.PlaySfxWithPitch("CardFlip");

                contentCanvas.localScale = Vector3.one;
                contentCanvas.localEulerAngles = Vector3.zero;

                contentCanvas.DOKill();
                contentCanvas.DOScale(new Vector3(0.75f, 0.5f, 0.5f), 0.25f);
                contentCanvas.DORotate(new Vector3(-90f, -12f, -12f), 0.25f).OnComplete(() =>
                {
                    turnEndButtonRect.gameObject.SetActive(false);
                    statsRect.gameObject.SetActive(false);
                    questImageRect.GetComponent<Image>().sprite = deadEnemyImage;
                    questTitleBeltRect.GetComponent<Image>().sprite = deadEnemyBelt;
                    questSealRect.GetComponent<Image>().sprite = deadEnemySeal;

                    contentCanvas.localScale = new Vector3(0.75f, 0.5f, 0.5f);
                    contentCanvas.localEulerAngles = new Vector3(-90, 12f, 12f);

                    contentCanvas.DOKill();
                    contentCanvas.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f);
                    contentCanvas.DORotate(new Vector3(4f, 12f, -2f), 0.25f).OnComplete(() =>
                    {

                        defeatedShader.GetComponent<Image>().DOFade(0, 0.25f);
                        contentCanvas.DOKill();
                        contentCanvas.DORotate(Vector3.zero, 0.25f);
                        contentCanvas.DOScale(Vector3.one, 0.25f).OnComplete(() =>
                        {
                            cancelButtonRect.GetComponent<CancelButton>().FreeTheCancelButton();
                        });
                    });
                });
            });
        });
    }

    public void SetRewards()
    {
        StartCoroutine(SetRewardsRoutine());
    }

    private IEnumerator SetRewardsRoutine()
    {
        yield return new WaitForSeconds(2f);

        GameObject epilogueContent = Instantiate(contentData.combatEpilogue.contentTemplate);
        epilogueContent.transform.SetParent(transform.parent, false);
        epilogueContent.name = "Epilogue";
        
        AudioManager.Instance.PlaySfxWithPitch("CardFlip");

        epilogueContent.GetComponent<DescriptionContent>().SetEpilogueComponents(contentData.combatEpilogue);
        epilogueContent.GetComponent<IContent>().FlipOnContent();

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < contentData.combatRewards.Count; i++)
        {
            GameObject rewardContent = Instantiate(rewardContentPrefab);
            rewardContent.transform.SetParent(transform.parent, false);
            rewardContent.name = $"Reward_({i + 1})";

            AudioManager.Instance.PlaySfxWithPitch("CardFlip");

            rewardContent.GetComponent<RewardContent>().SetRewardComponents(contentData.combatRewards[i]);
            rewardContent.GetComponent<IContent>().FlipOnContent();

            yield return new WaitForSeconds(0.25f);
        }
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

        if (currentHealthValue <= 0)
        {
            FlipOnDeadEnemy();
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