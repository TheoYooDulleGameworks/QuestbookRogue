using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

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

    private RectTransform rectTransform;
    private Coroutine scaleCoroutine;

    private void Awake()
    {
        SetQuestComponents();
    }

    private void OnEnable()
    {
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
            rectTransform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine);
            }
            scaleCoroutine = StartCoroutine(ScaleDownToNormal());

            undiscoveredQuestBelt.gameObject.SetActive(true);
            return;
        }
        else
        {
            rectTransform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine);
            }
            scaleCoroutine = StartCoroutine(ScaleDownToNormal());
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
        if (!Discovered)
        {
            return;
        }

        SceneController.Instance.TransitionToContents(questData);
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



    // Scale Anim //

    private IEnumerator ScaleDownToNormal()
    {
        Vector3 startScale = rectTransform.localScale;
        Vector3 endScale = new Vector3(1f, 1f, 1f);
        float duration = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            rectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = endScale;
        scaleCoroutine = null;
    }
}