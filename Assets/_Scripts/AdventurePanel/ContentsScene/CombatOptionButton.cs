using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CombatOptionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public List<CombatOption> combatOptionLists;
    [SerializeField] private bool isActivated = false;

    private void OnEnable()
    {
        isActivated = false;
        GetComponent<Image>().raycastTarget = false;
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
