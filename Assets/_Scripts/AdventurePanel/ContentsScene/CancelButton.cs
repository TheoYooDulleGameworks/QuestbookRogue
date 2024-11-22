using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CancelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Current Quest Data")]
    [SerializeField] public QuestSO currentQuestData;
    [SerializeField] public bool isFreeToCancel;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultCancelButton;
    [SerializeField] private Sprite mouseOverCancelButton;
    [SerializeField] private Sprite mouseDownCancelButton;
    [SerializeField] private Sprite defaultFreeCancelButton;
    [SerializeField] private Sprite mouseOverFreeCancelButton;
    [SerializeField] private Sprite mouseDownFreeCancelButton;

    private void OnEnable()
    {
        GetComponent<Image>().raycastTarget = false;
    }

    public void DeActivateTarget()
    {
        GetComponent<Image>().raycastTarget = false;
    }

    public void ActivateTarget()
    {
        GetComponent<Image>().raycastTarget = true;
    }

    public void WaitAndActivateTarget()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.DOScale(Vector3.one, 1.5f).OnComplete(() =>
        {
            GetComponent<Image>().raycastTarget = true;
        });
    }

    public void FreeTheCancelButton()
    {
        if (isFreeToCancel)
        {
            return;
        }

        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
        rectTransform.DOKill();
        isFreeToCancel = true;
        GetComponent<Image>().sprite = defaultFreeCancelButton;

        rectTransform.DOScale(Vector3.one, 0.25f).OnComplete(() =>
        {
            rectTransform.DOScale(Vector3.one, 0.75f).OnComplete(() =>
            {
                GetComponent<Image>().raycastTarget = true;
            });
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isFreeToCancel)
        {
            GetComponent<Image>().sprite = mouseOverFreeCancelButton;
        }
        else
        {
            GetComponent<Image>().sprite = mouseOverCancelButton;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isFreeToCancel)
        {
            GetComponent<Image>().sprite = defaultFreeCancelButton;
        }
        else
        {
            GetComponent<Image>().sprite = defaultCancelButton;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isFreeToCancel)
        {
            GetComponent<Image>().sprite = mouseDownFreeCancelButton;
        }
        else
        {
            GetComponent<Image>().sprite = mouseDownCancelButton;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isFreeToCancel)
        {
            GetComponent<Image>().sprite = defaultFreeCancelButton;
        }
        else
        {
            GetComponent<Image>().sprite = defaultCancelButton;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;

        SceneController.Instance.NotPaySlotRefund();

        if (isFreeToCancel)
        {
            // Quest Complete -> Save
        }
        else
        {
            StartCoroutine(FailedQuestRoutine());
            return;
        }

        SceneController.Instance.TransitionToSelects(currentQuestData);
        GameManager.Instance.UpdateStagePhase(StagePhase.None);
    }

    private IEnumerator FailedQuestRoutine()
    {
        SceneController.Instance.FailedQuest();
        SceneController.Instance.DeActivateRollDices();

        yield return new WaitForSeconds(2f);

        SceneController.Instance.TransitionToSelects(currentQuestData);
        GameManager.Instance.UpdateStagePhase(StagePhase.None);
    }
}