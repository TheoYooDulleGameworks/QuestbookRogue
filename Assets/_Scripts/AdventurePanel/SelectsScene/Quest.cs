using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Quest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Quest Data")]
    [SerializeField] private QuestSO questData = null;

    [Header("Components")]
    [SerializeField] private RectTransform mainImage = null;
    [SerializeField] private RectTransform selectionBelt = null;
    [SerializeField] private RectTransform questBelt = null;
    [SerializeField] private RectTransform questSeal = null;
    [SerializeField] private TextMeshProUGUI questTitleText = null;

    [SerializeField] private Sprite undiscoveredImage = null;

    [Header("bool Trigger")]
    [SerializeField] private bool discovered = false;
    public bool Discovered
    {
        get { return discovered; }
    }
    [SerializeField] public bool inTransition = false;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        SetQuestComponents();
    }

    private void OnEnable()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        rectTransform = canvasGroup.GetComponent<RectTransform>();

        selectionBelt.GetComponent<Image>().raycastTarget = false;
        selectionBelt.gameObject.SetActive(false);
        mainImage.gameObject.SetActive(true);
    }

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

        if (!Discovered)
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

        if (!Discovered)
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



    // QUESTS //

    public void ActivateQuestCard()
    {
        inTransition = false;
    }

    public void DeActivateQuestCard()
    {
        inTransition = true;
    }



    // QUEST COMPONENTS SETTINGS //

    public void SetQuestComponents()
    {
        inTransition = false;

        if (Discovered)
        {
            if (questData != null)
            {
                mainImage.gameObject.SetActive(true);
                questBelt.gameObject.SetActive(true);
                questSeal.gameObject.SetActive(true);

                mainImage.GetComponent<Image>().sprite = questData.questThumbnail;
                questSeal.GetComponent<Image>().sprite = questData.questSeal;
                questTitleText.text = questData.questName;
            }
        }
        else
        {
            questBelt.gameObject.SetActive(false);
            questSeal.gameObject.SetActive(false);

            mainImage.GetComponent<Image>().sprite = undiscoveredImage;
            questTitleText.text = null;
        }
    }
}