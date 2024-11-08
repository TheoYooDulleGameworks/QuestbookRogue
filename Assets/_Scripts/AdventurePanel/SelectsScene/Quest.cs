using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Quest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Quest Data")]
    [SerializeField] private QuestSO questData = null;
    // [SerializeField] private UndiscoveredSO undiscoveredData = null;

    [Header("Components")]
    [SerializeField] private RectTransform undiscoveredImage = null;
    [SerializeField] private RectTransform mainImage = null;
    [SerializeField] private RectTransform questBelt = null;
    [SerializeField] private RectTransform questSeal = null;
    [SerializeField] private TextMeshProUGUI questTitleText = null;
    [SerializeField] private RectTransform selectionBelt = null;

    [Header("bool Trigger")]
    [SerializeField] private bool discovered = false;
    [SerializeField] public bool inTransition = false;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        // canvasGroup = GetComponentInChildren<CanvasGroup>();
        // rectTransform = canvasGroup.GetComponent<RectTransform>();
    }

    public void InitiateQuestCard(QuestSO _questData)
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

        discovered = false;
        inTransition = true;
    }

    // Open & Close //

    public void GenerateQuest()
    {
        canvasGroup.alpha = 0f;
        rectTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        rectTransform.localEulerAngles = Vector3.zero;

        rectTransform.DOKill();
        rectTransform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.2f);
        rectTransform.DORotate(new Vector3(2f, 90f, 2f), 0.2f);
        canvasGroup.DOFade(1, 0.2f).OnComplete(() =>
        {
            rectTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            rectTransform.localEulerAngles = new Vector3(-2f, -90f, -2f);

            rectTransform.DOKill();
            rectTransform.DOScale(Vector3.one, 0.2f);
            rectTransform.DORotate(Vector3.zero, 0.2f);
        });
    }

    public void FlipOnQuest()
    {
        rectTransform.localScale = Vector3.one;
        rectTransform.localEulerAngles = Vector3.zero;

        rectTransform.DOKill();
        rectTransform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.2f);
        rectTransform.DORotate(new Vector3(2f, 90f, 2f), 0.2f).OnComplete(() =>
        {
            undiscoveredImage.gameObject.SetActive(false);

            mainImage.gameObject.SetActive(true);
            mainImage.GetComponent<Image>().sprite = questData.questThumbnail;

            questBelt.gameObject.SetActive(true);
            questSeal.gameObject.SetActive(true);
            questTitleText.text = questData.questName;
            questSeal.GetComponent<Image>().sprite = questData.questSeal;

            rectTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            rectTransform.localEulerAngles = new Vector3(-2f, -90f, -2f);

            rectTransform.DOKill();
            rectTransform.DOScale(Vector3.one, 0.2f);
            rectTransform.DORotate(Vector3.zero, 0.2f);

            discovered = true;
        });
    }

    public void FlipOffQuest()
    {

    }



    // Interactions //

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inTransition)
        {
            return;
        }

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

        if (!discovered)
        {
            rectTransform.DOKill();
            rectTransform.DORotate(rotateVector, 0.1f);
            rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() =>
            {
                rectTransform.DOScale(Vector3.one, 0.2f);
                rectTransform.DORotate(Vector3.zero, 0.2f);
            });
        }
        else
        {
            rectTransform.DOKill();
            rectTransform.DORotate(rotateVector, 0.1f);
            rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() =>
            {
                rectTransform.DOScale(Vector3.one, 0.2f);
                rectTransform.DORotate(Vector3.zero, 0.2f);

                selectionBelt.gameObject.SetActive(true);
                selectionBelt.GetComponent<Selection>().questData = questData;
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

        if (!discovered)
        {
            rectTransform.DOKill();
            rectTransform.DOScale(Vector3.one, 0.25f);
            rectTransform.DORotate(Vector3.zero, 0.25f);
        }
        else
        {
            selectionBelt.GetComponent<Image>().raycastTarget = false;
            selectionBelt.GetComponent<Selection>().questData = null;
            selectionBelt.GetComponent<Selection>().parentQuest = null;
            selectionBelt.GetComponent<Selection>().parentRectTransform = null;
            selectionBelt.gameObject.SetActive(false);

            rectTransform.DOKill();
            rectTransform.DOScale(Vector3.one, 0.25f);
            rectTransform.DORotate(Vector3.zero, 0.25f);
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