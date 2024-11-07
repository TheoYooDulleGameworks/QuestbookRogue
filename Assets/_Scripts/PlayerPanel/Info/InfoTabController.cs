using UnityEngine;
using DG.Tweening;

public class InfoTabController : Singleton<InfoTabController>
{
    [SerializeField] private GameObject diceTab;
    [SerializeField] private GameObject skillTab;
    [SerializeField] private GameObject relicTab;
    [SerializeField] private GameObject logTab;

    [SerializeField] private DiceTabIndex diceTabIndex;
    [SerializeField] private SkillTabIndex skillTabIndex;

    private void Start()
    {
        DefaultSetTabs();

        diceTabIndex.OnDiceTabClicked += HandleDiceTabActivate;
        skillTabIndex.OnSkillTabClicked += HandleSkillTabActivate;
    }

    private void OnDestroy()
    {
        diceTabIndex.OnDiceTabClicked -= HandleDiceTabActivate;
        skillTabIndex.OnSkillTabClicked -= HandleSkillTabActivate;
    }

    public void HandleDiceTabActivate()
    {
        ResetTabs();

        if (skillTab.activeSelf == false)
        {
            diceTab.SetActive(true);
            diceTab.GetComponent<CanvasGroup>().alpha = 0;
            diceTab.GetComponent<CanvasGroup>().DOKill();
            diceTab.GetComponent<DiceTabUI>().SetPlayerDices();
            diceTab.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        }

        diceTabIndex.ActivateDiceTab();
    }

    public void HandleSkillTabActivate()
    {
        ResetTabs();

        if (skillTab.activeSelf == false)
        {
            skillTab.SetActive(true);
            skillTab.GetComponent<CanvasGroup>().DOKill();
            skillTab.GetComponent<CanvasGroup>().alpha = 0;
            skillTab.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        }

        skillTabIndex.ActivateSkillTab();
    }

    private void DefaultSetTabs()
    {
        if (skillTab != null)
        {
            skillTab.SetActive(false);
        }
        if (diceTab != null)
        {
            diceTab.SetActive(true);
        }

        if (diceTabIndex != null)
        {
            diceTabIndex.ActivateDiceTab();
        }
        if (skillTabIndex != null)
        {
            skillTabIndex.DeActivateSkillTab();
        }
    }

    private void ResetTabs()
    {
        if (diceTab.activeSelf == true)
        {
            diceTab.GetComponent<CanvasGroup>().DOKill();
            diceTab.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
            diceTab.SetActive(false);
            diceTab.GetComponent<CanvasGroup>().alpha = 1;
        }
        if (skillTab.activeSelf == true)
        {
            skillTab.GetComponent<CanvasGroup>().DOKill();
            skillTab.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
            skillTab.SetActive(false);
            skillTab.GetComponent<CanvasGroup>().alpha = 1;
        }

        if (skillTabIndex != null)
        {
            skillTabIndex.DeActivateSkillTab();
        }
        if (diceTabIndex != null)
        {
            diceTabIndex.DeActivateDiceTab();
        }
    }
}
