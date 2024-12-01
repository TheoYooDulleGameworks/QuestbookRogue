using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TurnEndButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private Sprite defaultTurnEndButton;
    [SerializeField] private Sprite activatedTurnEndButton;
    [SerializeField] private Sprite mouseOverTurnEndButton;
    [SerializeField] private Sprite mouseDownTurnEndButton;

    [SerializeField] private bool isActivated;

    private void OnEnable()
    {
        GameManager.Instance.OnStagePhaseChanged += HandleStagePhaseChange;

        isActivated = false;

        GetComponent<Image>().sprite = defaultTurnEndButton;
        GetComponent<Image>().raycastTarget = false;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStagePhaseChanged -= HandleStagePhaseChange;

        isActivated = false;

        GetComponent<Image>().sprite = null;
        GetComponent<Image>().raycastTarget = false;
    }

    private void HandleStagePhaseChange(StagePhase stagePhase)
    {
        if (stagePhase == StagePhase.DiceUsing)
        {
            isActivated = true;

            GetComponent<RectTransform>().DOKill();
            GetComponent<RectTransform>().DOScale(new Vector3(1.35f, 1.35f, 1.35f), 0.1f).OnComplete(() =>
            {
                GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f);
                GetComponent<Image>().sprite = activatedTurnEndButton;
                GetComponent<Image>().raycastTarget = true;
            });

        }
        else if (stagePhase == StagePhase.DiceHolding)
        {
            isActivated = false;

            GetComponent<Image>().sprite = defaultTurnEndButton;
            GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        GetComponent<Image>().sprite = mouseOverTurnEndButton;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        GetComponent<Image>().sprite = activatedTurnEndButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        GetComponent<Image>().sprite = mouseDownTurnEndButton;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        GetComponent<Image>().sprite = activatedTurnEndButton;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        AudioManager.Instance.PlaySfx("Confirm");
        
        GameManager.Instance.UpdateStagePhase(StagePhase.DiceHolding);
    }
}
