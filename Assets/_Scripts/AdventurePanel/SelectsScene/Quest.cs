using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Quest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Quest Data")]
    [SerializeField] private QuestSO questData = null;
    [SerializeField] private int questIndexNumber = -1;

    [Header("Components")]
    [SerializeField] private RectTransform undiscoveredImage = null;
    [SerializeField] private RectTransform mainImage = null;
    [SerializeField] private RectTransform questBelt = null;
    [SerializeField] private RectTransform questSeal = null;
    [SerializeField] private RectTransform resolvedImage = null;
    [SerializeField] private TextMeshProUGUI questTitleText = null;
    [SerializeField] private RectTransform selectionBelt = null;

    [Header("bool Trigger")]
    [SerializeField] private bool discovered = false;
    [SerializeField] private bool resolved = false;
    [SerializeField] public bool inTransition = false;

    [Header("Sprite Sources")]
    [SerializeField] private Sprite resolvedCoverSprite;
    [SerializeField] private Sprite resolvedBelt;
    [SerializeField] private Sprite resolvedSeal;
    [SerializeField] private Sprite failedSeal;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private bool isStandby;

    private void Awake()
    {
        // Test
        InitiateQuestCard(questData, 0);
    }

    private void Start()
    {
        // Test
        FlipOnQuest();
    }

    public void InitiateQuestCard(QuestSO _questData, int _questIndexNum)
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        rectTransform = canvasGroup.GetComponent<RectTransform>();

        undiscoveredImage.gameObject.SetActive(true);

        mainImage.GetComponent<Image>().sprite = null;
        mainImage.gameObject.SetActive(false);

        questSeal.GetComponent<Image>().sprite = null;
        questSeal.gameObject.SetActive(false);

        questBelt.gameObject.SetActive(false);
        questTitleText.text = null;

        selectionBelt.GetComponent<Image>().raycastTarget = false;
        selectionBelt.gameObject.SetActive(false);

        questData = _questData;
        questIndexNumber = _questIndexNum;

        discovered = false;
        inTransition = true;
    }

    // Open & Close //

    public void GenerateQuest()
    {
        canvasGroup.alpha = 0f;
        rectTransform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        rectTransform.localEulerAngles = new Vector3(0f, -12f, -12f);

        rectTransform.DOKill();
        rectTransform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.25f);
        rectTransform.DORotate(new Vector3(-90f, -12f, -12f), 0.25f);
        canvasGroup.DOFade(0.25f, 0.25f).OnComplete(() =>
        {
            rectTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            rectTransform.localEulerAngles = new Vector3(-90f, 8f, 8f);

            rectTransform.DOKill();
            rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f);
            rectTransform.DORotate(new Vector3(4f, 12f, -2f), 0.25f);
            canvasGroup.DOFade(1f, 0.25f).OnComplete(() =>
            {
                rectTransform.DOKill();
                rectTransform.DORotate(Vector3.zero, 0.25f);
                rectTransform.DOScale(Vector3.one, 0.25f);
            });
        });
    }

    public void FlipOnQuest()
    {
        inTransition = true;

        if (discovered)
        {
            return;
        }

        rectTransform.localScale = Vector3.one;
        rectTransform.localEulerAngles = Vector3.zero;

        rectTransform.DOKill();
        rectTransform.DOScale(new Vector3(0.75f, 0.5f, 0.5f), 0.25f);
        rectTransform.DORotate(new Vector3(-90f, -12f, -12f), 0.25f).OnComplete(() =>
        {
            undiscoveredImage.gameObject.SetActive(false);
            mainImage.gameObject.SetActive(true);

            mainImage.GetComponent<Image>().sprite = questData.questMainImage;

            questBelt.gameObject.SetActive(true);
            questSeal.gameObject.SetActive(true);
            questTitleText.text = questData.questName;
            questSeal.GetComponent<Image>().sprite = questData.questSeal;

            rectTransform.localScale = new Vector3(0.75f, 0.5f, 0.5f);
            rectTransform.localEulerAngles = new Vector3(-90, 12f, 12f);

            rectTransform.DOKill();
            rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f);
            rectTransform.DORotate(new Vector3(4f, 12f, -2f), 0.25f).OnComplete(() =>
            {
                rectTransform.DOKill();
                rectTransform.DORotate(Vector3.zero, 0.25f);
                rectTransform.DOScale(Vector3.one, 0.25f).OnComplete(() =>
                {
                    discovered = true;
                    inTransition = false;
                });
            });

        });
    }

    public void ResolvedQuest()
    {
        resolved = true;

        resolvedImage.gameObject.SetActive(true);
        resolvedImage.GetComponent<Image>().sprite = resolvedCoverSprite;

        questBelt.GetComponent<Image>().sprite = resolvedBelt;
        questSeal.GetComponent<Image>().sprite = resolvedSeal;
    }

    public void UnselectedQuest()
    {
        // Later
    }

    public void DeleteQuest()
    {
        rectTransform.localEulerAngles = Vector3.zero;
        rectTransform.localScale = Vector3.one;

        rectTransform.DOKill();
        rectTransform.DORotate(new Vector3(-75f, -12f, -6f), 0.25f);
        rectTransform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.25f);
        canvasGroup.DOFade(0, 0.25f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }



    // Interactions //

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inTransition || isStandby)
        {
            return;
        }

        if (!discovered || resolved)
        {
            rectTransform.DOKill();
            rectTransform.DORotate(new Vector3(-4f, -12f, 2f), 0.1f);
            rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() =>
            {
                rectTransform.DOScale(Vector3.one, 0.2f);
                rectTransform.DORotate(Vector3.zero, 0.2f);
            });
        }
        else
        {
            isStandby = true;

            rectTransform.DOKill();
            rectTransform.DORotate(new Vector3(-4f, -12f, 2f), 0.1f);
            rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() =>
            {
                rectTransform.DOScale(Vector3.one, 0.2f);
                rectTransform.DORotate(Vector3.zero, 0.2f);

                selectionBelt.gameObject.SetActive(true);
                selectionBelt.GetComponent<Selection>().questData = questData;
                selectionBelt.GetComponent<Selection>().questIndexNumber = questIndexNumber;
                selectionBelt.GetComponent<Selection>().parentQuest = this;
                selectionBelt.GetComponent<Selection>().parentRectTransform = rectTransform;
                selectionBelt.GetComponent<Image>().raycastTarget = true;
            });
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inTransition)
        {
            return;
        }

        if (!discovered || resolved)
        {
            rectTransform.DOKill();
            rectTransform.DOScale(Vector3.one, 0.1f);
            rectTransform.DORotate(Vector3.zero, 0.1f);
        }
        else
        {
            if (isStandby)
            {
                isStandby = false;

                selectionBelt.GetComponent<Image>().raycastTarget = false;
                selectionBelt.GetComponent<Selection>().questData = null;
                selectionBelt.GetComponent<Selection>().questIndexNumber = -1;
                selectionBelt.GetComponent<Selection>().parentQuest = null;
                selectionBelt.GetComponent<Selection>().parentRectTransform = null;
                selectionBelt.gameObject.SetActive(false);

                rectTransform.DOKill();
                rectTransform.DOScale(Vector3.one, 0.1f);
                rectTransform.DORotate(Vector3.zero, 0.1f);
            }
        }
    }



    // In Scene Transitions //

    public void ActivateQuestCard()
    {
        inTransition = false;
    }

    public void DeActivateQuestCard()
    {
        inTransition = true;
        selectionBelt.GetComponent<Image>().raycastTarget = false;
        selectionBelt.gameObject.SetActive(false);
    }
}