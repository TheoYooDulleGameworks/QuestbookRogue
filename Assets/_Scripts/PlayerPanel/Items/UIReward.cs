using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class UIReward : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Reward Data")]
    public RewardSet rewardDataSet;

    [Header("Player Assets")]
    [SerializeField] private PlayerStatusSO playerStatus;

    [Header("Components")]
    [SerializeField] private RectTransform rewardImage;

    public void SetRewardData(RewardSet _rewardDataSet)
    {
        rewardDataSet = _rewardDataSet;
        rewardImage.GetComponent<Image>().sprite = rewardDataSet.rewardData.defaultSprite;
    }

    public void PopUpReward()
    {
        gameObject.SetActive(true);
        RectTransform rewardRect = GetComponent<RectTransform>();

        rewardRect.DOKill();
        rewardRect.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        rewardRect.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f).OnComplete(() =>
        {
            rewardRect.DOScale(Vector3.one, 0.2f);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rewardImage.GetComponent<Image>().sprite = rewardDataSet.rewardData.hoverSprite;

        Vector3 rotateVector;
        Vector3 leftRotateVector = new Vector3(-2f, -2f, -2f);
        Vector3 rightRotateVector = new Vector3(2f, 2f, 2f);

        int randomInt = Random.Range(1, 3);
        if (randomInt == 1)
        {
            rotateVector = leftRotateVector;
        }
        else
        {
            rotateVector = rightRotateVector;
        }

        rewardImage.DOKill();
        rewardImage.DORotate(rotateVector, 0.1f);
        rewardImage.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).OnComplete(() =>
        {
            rewardImage.DOScale(Vector3.one, 0.2f);
            rewardImage.DORotate(Vector3.zero, 0.2f);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rewardImage.GetComponent<Image>().sprite = rewardDataSet.rewardData.defaultSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        rewardImage.DOKill();
        rewardImage.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).OnComplete(() =>
        {
            rewardImage.DOScale(new Vector3(0f, 0f, 0f), 0.2f).OnComplete(() =>
            {
                GetRewards(rewardDataSet);
                Destroy(gameObject);
            });
        });
    }

    public void GetRewards(RewardSet _rewardDataSet)
    {
        if (_rewardDataSet.rewardData.rewardType == RewardType.Resource)
        {
            int addValue = _rewardDataSet.rewardAmount;

            switch (_rewardDataSet.rewardData.resourceRewardType)
            {
                case ResourceRewardType.Coin:
                    playerStatus.Coin.AddValue(addValue);
                    break;
                case ResourceRewardType.Provision:
                    playerStatus.Provision.AddValue(addValue);
                    break;
                case ResourceRewardType.Xp:
                    playerStatus.currentXp.AddValue(addValue);
                    break;
                case ResourceRewardType.Hp:
                    playerStatus.currentHp.AddClampedValue(addValue, 0, playerStatus.maxHp.Value);
                    break;
                case ResourceRewardType.Sp:
                    playerStatus.currentSp.AddClampedValue(addValue, 0, playerStatus.maxSp.Value);
                    break;
            }
        }
    }
}