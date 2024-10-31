using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Quest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Quest Data")]
    [SerializeField] private QuestSO questData = null;

    [Header("Components")]
    [SerializeField] private RectTransform questThumbnail = null;
    [SerializeField] private RectTransform questBelt = null;
    [SerializeField] private RectTransform questSeal = null;
    [SerializeField] private TextMeshProUGUI questTitleText = null;

    [Header("Selections")]
    [SerializeField] private RectTransform questSelection = null;
    [SerializeField] private GameObject selectionBeltPrefab = null;
    [SerializeField] private GameObject smallStaminaPrefab = null;

    [Header("About Discovery")]
    [SerializeField] private Sprite undiscoveredQuestThumbnail = null;
    [SerializeField] private RectTransform undiscoveredQuestBelt = null;
    [SerializeField] private bool discovered = false;
    public bool Discovered
    {
        get { return discovered; }
    }

    [Header("In Transition")]
    [SerializeField] private bool inTransition = false;

    private void Awake()
    {
        SetData();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inTransition)
        {
            return;
        }

        if (!Discovered)
        {
            questThumbnail.anchoredPosition = questThumbnail.anchoredPosition + new Vector2(0, 8);
            undiscoveredQuestBelt.gameObject.SetActive(true);
            return;
        }

        questThumbnail.anchoredPosition = questThumbnail.anchoredPosition + new Vector2(0, 8);
        questBelt.anchoredPosition = questBelt.anchoredPosition + new Vector2(0, 8);
        questSeal.anchoredPosition = questSeal.anchoredPosition + new Vector2(0, 8);

        questSelection.gameObject.SetActive(true);
        List<Image> selectionBelts = new List<Image>(questSelection.GetComponentsInChildren<Image>());
        foreach (var image in selectionBelts)
        {
            image.raycastTarget = true;
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
            questThumbnail.anchoredPosition = questThumbnail.anchoredPosition + new Vector2(0, -8);
            undiscoveredQuestBelt.gameObject.SetActive(false);
            return;
        }

        questThumbnail.anchoredPosition = questThumbnail.anchoredPosition + new Vector2(0, -8);
        questBelt.anchoredPosition = questBelt.anchoredPosition + new Vector2(0, -8);
        questSeal.anchoredPosition = questSeal.anchoredPosition + new Vector2(0, -8);

        questSelection.gameObject.SetActive(false);

        List<Image> selectionBelts = new List<Image>(questSelection.GetComponentsInChildren<Image>());
        foreach (var image in selectionBelts)
        {
            image.raycastTarget = false;
        }
    }

    public void DropDownQuestCard()
    {
        questThumbnail.anchoredPosition = questThumbnail.anchoredPosition + new Vector2(0, -8);
        questBelt.anchoredPosition = questBelt.anchoredPosition + new Vector2(0, -8);
        questSeal.anchoredPosition = questSeal.anchoredPosition + new Vector2(0, -8);

        questSelection.gameObject.SetActive(false);
        List<Image> selectionBelts = new List<Image>(questSelection.GetComponentsInChildren<Image>());
        foreach (var image in selectionBelts)
        {
            image.raycastTarget = false;
        }
    }

    public void ActivateQuestCard()
    {
        inTransition = false;
    }

    public void DeActivateQuestCard()
    {
        inTransition = true;
    }

    public void SetData()
    {
        undiscoveredQuestBelt.gameObject.SetActive(false);
        inTransition = false;

        if (Discovered)
        {
            if (questData != null)
            {
                List<GameObject> instantiatedSelections = new List<GameObject>();

                for (int i = 0; i < questData.selectionDatas.Count; i++)
                {
                    GameObject selectionBelt = Instantiate(selectionBeltPrefab);
                    selectionBelt.GetComponent<Selection>().selectionData = questData.selectionDatas[i];

                    selectionBelt.transform.SetParent(questSelection, false);
                    selectionBelt.name = $"selectionBelt ({i + 1})";
                    instantiatedSelections.Add(selectionBelt);

                    instantiatedSelections[i].GetComponentInChildren<TextMeshProUGUI>().text = questData.selectionDatas[i].selectionName;

                    var selectionStaminaParent = instantiatedSelections[i].GetComponentInChildren<HorizontalLayoutGroup>().GetComponent<RectTransform>();

                    for (int j = 0; j < questData.selectionDatas[i].selectionStamina; j++)
                    {
                        GameObject selectionStamina = Instantiate(smallStaminaPrefab);
                        selectionStamina.transform.SetParent(selectionStaminaParent, false);
                    }
                }

                questSelection.gameObject.SetActive(false);

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

    public void ResetData()
    {

    }
}