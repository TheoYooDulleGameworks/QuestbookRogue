using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class ImageContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI questTitleTMPro = null;
    [SerializeField] private RectTransform questImageRect = null;
    [SerializeField] private RectTransform questSealRect = null;
    [SerializeField] private RectTransform cancelButtonRect = null;

    [Header("Rewards")]
    [SerializeField] private ContentSO currentEpliogue = null;
    [SerializeField] private List<RewardSet> currentRewards = null;
    [SerializeField] private RectTransform completeShader = null;
    [SerializeField] private GameObject rewardContentPrefab = null;
    
    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultCancelButton;
    [SerializeField] private Sprite defaultFreeCancelButton;



    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        contentData = _contentData;

        questTitleTMPro.text = _questData.questName;
        questImageRect.GetComponent<Image>().sprite = _questData.questMainImage;
        questSealRect.GetComponent<Image>().sprite = _questData.questSeal;

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

    public void FlipOnCompleted(Sprite epilogueImage, ContentSO _epilogueContent, List<RewardSet> _rewards)
    {
        currentEpliogue = _epilogueContent;
        currentRewards = _rewards;

        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.localEulerAngles = Vector3.zero;
        contentCanvas.localScale = Vector3.one;

        contentCanvas.DOKill();
        contentCanvas.DORotate(new Vector3(-4f, -12f, 2f), 0.1f);
        completeShader.GetComponent<Image>().DOFade(1, 0.3f);
        contentCanvas.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f).OnComplete(() =>
        {
            contentCanvas.DOScale(Vector3.one, 0.2f);
            contentCanvas.DORotate(Vector3.zero, 0.2f).OnComplete(() =>
            {
                GameManager.Instance.UpdateStagePhase(StagePhase.Finishing);

                contentCanvas.localScale = Vector3.one;
                contentCanvas.localEulerAngles = Vector3.zero;

                contentCanvas.DOKill();
                contentCanvas.DOScale(new Vector3(0.75f, 0.5f, 0.5f), 0.25f);
                contentCanvas.DORotate(new Vector3(-90f, -12f, -12f), 0.25f).OnComplete(() =>
                {
                    questImageRect.GetComponent<Image>().sprite = epilogueImage;

                    contentCanvas.localScale = new Vector3(0.75f, 0.5f, 0.5f);
                    contentCanvas.localEulerAngles = new Vector3(-90, 12f, 12f);

                    contentCanvas.DOKill();
                    contentCanvas.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f);
                    contentCanvas.DORotate(new Vector3(4f, 12f, -2f), 0.25f).OnComplete(() =>
                    {

                        completeShader.GetComponent<Image>().DOFade(0, 0.25f);
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

        GameObject epilogueContent = Instantiate(currentEpliogue.contentTemplate);
        epilogueContent.transform.SetParent(transform.parent, false);
        epilogueContent.name = "Epilogue";

        AudioManager.Instance.PlaySfxWithPitch("CardFlip");
        
        epilogueContent.GetComponent<DescriptionContent>().SetEpilogueComponents(currentEpliogue);
        epilogueContent.GetComponent<IContent>().FlipOnContent();

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < currentRewards.Count; i++)
        {
            GameObject rewardContent = Instantiate(rewardContentPrefab);
            rewardContent.transform.SetParent(transform.parent, false);
            rewardContent.name = $"Reward_({i + 1})";

            AudioManager.Instance.PlaySfxWithPitch("CardFlip");

            rewardContent.GetComponent<RewardContent>().SetRewardComponents(currentRewards[i]);
            rewardContent.GetComponent<IContent>().FlipOnContent();

            yield return new WaitForSeconds(0.25f);
        }
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
}
