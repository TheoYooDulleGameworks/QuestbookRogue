using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Quest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator myAnimator;

    [SerializeField] private RectTransform questThumbnail = null;
    [SerializeField] private RectTransform questBelt = null;
    [SerializeField] private RectTransform questSeal = null;
    [SerializeField] private TextMeshProUGUI questTitleText = null;

    [SerializeField] private RectTransform questContent = null;
    [SerializeField] private RectTransform questContentBG = null;

    [SerializeField] private Sprite coveredQuestThumbnail = null;

    [SerializeField] private QuestSO questData = null;
    [SerializeField] private bool uncovered = false;
    public bool Uncovered
    {
        get { return uncovered; }
    }

    private void Awake()
    {
        ResetData();
        myAnimator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Uncovered)
        {
            questThumbnail.anchoredPosition = questThumbnail.anchoredPosition + new Vector2(0, 8);
            return;
        }

        questThumbnail.anchoredPosition = questThumbnail.anchoredPosition + new Vector2(0, 8);
        questBelt.anchoredPosition = questBelt.anchoredPosition + new Vector2(0, 8);
        questSeal.anchoredPosition = questSeal.anchoredPosition + new Vector2(0, 8);

        questContent.gameObject.SetActive(true);
        questContentBG.GetComponent<Image>().sprite = questData.questContentBG;

        questContentBG.GetComponent<Image>().raycastTarget = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Uncovered)
        {
            questThumbnail.anchoredPosition = questThumbnail.anchoredPosition + new Vector2(0, -8);
            return;
        }

        questThumbnail.anchoredPosition = questThumbnail.anchoredPosition + new Vector2(0, -8);
        questBelt.anchoredPosition = questBelt.anchoredPosition + new Vector2(0, -8);
        questSeal.anchoredPosition = questSeal.anchoredPosition + new Vector2(0, -8);

        questContentBG.GetComponent<Image>().sprite = null;
        questContent.gameObject.SetActive(false);
        questContentBG.GetComponent<Image>().raycastTarget = false;
    }

    public void DeActivateContent()
    {
        questContentBG.GetComponent<Image>().sprite = null;
        questContent.gameObject.SetActive(false);
        questContentBG.GetComponent<Image>().raycastTarget = false;
    }

    public void ResetData()
    {
        questContent.gameObject.SetActive(false);

        if (Uncovered)
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

            questThumbnail.GetComponent<Image>().sprite = coveredQuestThumbnail;
            questTitleText.text = null;
        }
    }
}