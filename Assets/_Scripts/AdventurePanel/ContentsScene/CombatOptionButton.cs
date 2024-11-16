using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CombatOptionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public List<CombatOption> combatOptionLists;
    [SerializeField] private bool isActivated = false;

    private void OnEnable()
    {
        isActivated = false;
        GetComponent<Image>().raycastTarget = false;

        GameManager.Instance.OnStagePhaseChanged += HandleStagePhaseChange;
    }

    private void OnDisable()
    {
        isActivated = false;
        GetComponent<Image>().raycastTarget = false;

        GameManager.Instance.OnStagePhaseChanged -= HandleStagePhaseChange;
    }

    public void ActivateButton()
    {
        isActivated = true;
        GetComponent<Image>().raycastTarget = true;

        foreach (CombatOption option in combatOptionLists)
        {
            option.ActivatedOptionUI();
        }
    }

    public void DeActivateButton()
    {
        isActivated = false;
        GetComponent<Image>().raycastTarget = false;

        foreach (CombatOption option in combatOptionLists)
        {
            option.DefaultOptionUI();
        }
    }

    private void HandleStagePhaseChange(StagePhase stagePhase)
    {
        if (stagePhase == StagePhase.DiceWaiting)
        {
            GetComponent<RectTransform>().DOKill();
            GetComponent<RectTransform>().DOScale(new Vector3(1.35f, 1.35f, 1.35f), 0.1f).OnComplete(() =>
            {
                GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f);

                foreach (CombatOption option in combatOptionLists)
                {
                    option.ResetOptionUI();
                }
            });
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        foreach (CombatOption option in combatOptionLists)
        {
            option.HoverOptionUI();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        foreach (CombatOption option in combatOptionLists)
        {
            option.ActivatedOptionUI();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        foreach (CombatOption option in combatOptionLists)
        {
            option.ClickOptionUI();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        foreach (CombatOption option in combatOptionLists)
        {
            option.ActivatedOptionUI();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isActivated)
        {
            return;
        }

        isActivated = false;
        GetComponent<Image>().raycastTarget = false;

        foreach (CombatOption option in combatOptionLists)
        {
            option.ApplyEffect();
        }
    }
}
