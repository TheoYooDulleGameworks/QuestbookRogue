using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class SkillManager : Singleton<SkillManager>
{
    [Header("Transition")]
    [SerializeField] private CanvasGroup playerPanel;
    [SerializeField] private CanvasGroup adventurePanel;
    [SerializeField] private RectTransform fadeImage;
    [SerializeField] private RectTransform skillCancel;

    [Header("Skill Check")]
    [SerializeField] private int staminaPoint = 0;
    [SerializeField] private int signaturePoint = 0;
    [SerializeField] private RectTransform rollDicePanel;

    public List<RollDice> rollDices;
    public List<RollDice> clickableDices;

    public void SkillCostPhase(SkillSO skillData)
    {
        rollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());
        foreach (var dice in rollDices)
        {
            dice.SkillDeActivate();
        }

        FadeInScene();
    }

    public void SkillCastPhase(SkillSO skillData)
    {
        rollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());
        foreach (var dice in rollDices)
        {
            dice.SkillDeActivate();
        }
    }

    public void CancelSkill()
    {
        rollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());
        foreach (var dice in rollDices)
        {
            dice.SkillCanceled();
        }

        rollDices.Clear();
        clickableDices.Clear();

        FadeOutScene();
    }

    private void FadeInScene()
    {
        playerPanel.blocksRaycasts = false;
        adventurePanel.blocksRaycasts = false;
        skillCancel.gameObject.SetActive(true);
        skillCancel.GetComponent<SkillCancelButton>().ActiavteCancelButton();

        skillCancel.GetComponent<CanvasGroup>().DOKill();
        fadeImage.GetComponent<CanvasGroup>().DOKill();

        skillCancel.GetComponent<CanvasGroup>().alpha = 0f;
        fadeImage.GetComponent<CanvasGroup>().alpha = 0f;

        fadeImage.GetComponent<CanvasGroup>().DOFade(0.5f, 0.25f);
        skillCancel.GetComponent<CanvasGroup>().DOFade(1f, 0.25f);
    }

    private void FadeOutScene()
    {
        fadeImage.GetComponent<CanvasGroup>().DOKill();
        skillCancel.GetComponent<CanvasGroup>().DOKill();

        fadeImage.GetComponent<CanvasGroup>().alpha = 0.5f;
        skillCancel.GetComponent<CanvasGroup>().alpha = 1f;

        skillCancel.GetComponent<CanvasGroup>().DOFade(0f, 0.25f);
        fadeImage.GetComponent<CanvasGroup>().DOFade(0f, 0.25f).OnComplete(() =>
        {
            skillCancel.gameObject.SetActive(false);
            playerPanel.blocksRaycasts = true;
            adventurePanel.blocksRaycasts = true;
        });
    }
}