using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Quest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Quest Data")]
    [SerializeField] private QuestSO questData = null;

    [Header("Components")]
    [SerializeField] private RectTransform questThumbnail = null;
    [SerializeField] private RectTransform questBelt = null;
    [SerializeField] private RectTransform questSeal = null;
    [SerializeField] private TextMeshProUGUI questTitleText = null;

    [Header("About Discovery")]
    [SerializeField] private Sprite undiscoveredQuestThumbnail = null;
    [SerializeField] private RectTransform undiscoveredQuestBelt = null;
    [SerializeField] private bool discovered = false;
    public bool Discovered
    {
        get { return discovered; }
    }

    [Header("bool Trigger")]
    [SerializeField] private bool inTransition = false;

    [Header("Tweening")]
    [SerializeField] private float popUpScale = 1.1f;
    [SerializeField] private float popUpDuration = 0.25f;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        SetQuestComponents();
    }

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inTransition)
        {
            return;
        }

        if (!Discovered)
        {
            rectTransform.DOKill();
            rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
            rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).SetEase(Ease.OutCubic);

            undiscoveredQuestBelt.gameObject.SetActive(true);
            return;
        }
        else
        {
            rectTransform.DOKill();
            rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
            rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).SetEase(Ease.OutCubic);
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
            undiscoveredQuestBelt.gameObject.SetActive(false);
            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Discovered || inTransition)
        {
            return;
        }

        SceneController.Instance.TransitionToContents(questData);

        rectTransform.DOKill();
        rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
        rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).SetEase(Ease.OutCubic);
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
        undiscoveredQuestBelt.gameObject.SetActive(false);
        inTransition = false;

        if (Discovered)
        {
            if (questData != null)
            {
                questThumbnail.gameObject.SetActive(true);
                questBelt.gameObject.SetActive(true);
                questSeal.gameObject.SetActive(true);

                questThumbnail.GetComponent<Image>().sprite = questData.questThumbnail;
                questSeal.GetComponent<Image>().sprite = questData.questSeal;
                questTitleText.text = questData.questName;
            }
        }
        else
        {
            questBelt.gameObject.SetActive(false);
            questSeal.gameObject.SetActive(false);

            questThumbnail.GetComponent<Image>().sprite = undiscoveredQuestThumbnail;
            questTitleText.text = null;
        }
    }

    public void ResetQuestComponents()
    {

    }
}