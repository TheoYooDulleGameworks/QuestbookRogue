using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class InfoTabController : Singleton<InfoTabController>
{
    [SerializeField] private InfoTab currentTab;

    [SerializeField] private GameObject diceTab;
    [SerializeField] private GameObject skillTab;
    [SerializeField] private GameObject relicTab;
    [SerializeField] private GameObject logTab;

    [SerializeField] private InfoTabButton diceButton;
    [SerializeField] private InfoTabButton skillButton;
    [SerializeField] private InfoTabButton relicButton;
    [SerializeField] private InfoTabButton logButton;

    private Dictionary<InfoTab, GameObject> tabDictionary;

    private void Start()
    {
        InitializeTabs();
        currentTab = InfoTab.None;
        SetActiveTab(InfoTab.DiceTab);
    }

    private void InitializeTabs()
    {
        tabDictionary = new Dictionary<InfoTab, GameObject>
        {
            { InfoTab.DiceTab, diceTab },
            { InfoTab.SkillTab, skillTab },
            { InfoTab.RelicTab, relicTab },
            { InfoTab.LogTab, logTab }
        };

        foreach (var tab in tabDictionary.Values)
        {
            if (tab != null)
            {
                tab.SetActive(false);
                tab.GetComponent<CanvasGroup>().alpha = 1; // 초기화
            }
        }
    }

    public void SetActiveTab(InfoTab tabToActivate)
    {
        if (currentTab == tabToActivate)
        {
            return;
        }

        foreach (var tab in tabDictionary.Values)
        {
            if (tab.activeSelf)
            {
                tab.GetComponent<CanvasGroup>().DOKill();
                tab.GetComponent<CanvasGroup>().DOFade(0, 0.15f).OnComplete(() => tab.SetActive(false));
            }
        }

        diceButton.DeActivateButton();
        skillButton.DeActivateButton();
        relicButton.DeActivateButton();
        logButton.DeActivateButton();

        switch (tabToActivate)
        {
            case InfoTab.DiceTab:
                diceButton.ActivateButton();
                break;
            case InfoTab.SkillTab:
                skillButton.ActivateButton();
                break;
            case InfoTab.RelicTab:
                relicButton.ActivateButton();
                break;
            case InfoTab.LogTab:
                logButton.ActivateButton();
                break;
            default:
                break;
        }

        if (tabDictionary.TryGetValue(tabToActivate, out GameObject selectedTab))
        {
            selectedTab.SetActive(true);
            selectedTab.GetComponent<CanvasGroup>().DOKill();
            selectedTab.GetComponent<CanvasGroup>().alpha = 0;
            selectedTab.GetComponent<CanvasGroup>().DOFade(1, 0.15f);
            currentTab = tabToActivate;
        }
    }
}

public enum InfoTab
{
    DiceTab,
    SkillTab,
    RelicTab,
    LogTab,
    None
}