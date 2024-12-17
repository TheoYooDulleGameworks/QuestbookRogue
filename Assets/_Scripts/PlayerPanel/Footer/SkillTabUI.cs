using UnityEngine;
using System;
using System.Collections.Generic;

public class SkillTabUI : MonoBehaviour
{
    [Header("Datasets")]
    public PlayerSkillSO playerSkills;
    public GameObject uiSignatureMeterSkillPrefab;
    public GameObject uiSkillPrefab;

    [Header("Components")]
    [SerializeField] private RectTransform signatureSkillGroup;
    [SerializeField] private RectTransform commonSkillGroup;
    [SerializeField] private RectTransform utilitySkillGroup;

    public void InitiateSkillTabUI()
    {
        if (playerSkills != null)
        {
            SetPlayerSkills();
        }

        playerSkills.OnSignatureSkillLearn += HandleSignatureSkillLearn;
        playerSkills.OnCommonSkillLearn += HandleCommonSkillLearn;
        playerSkills.OnUtilitySkillLearn += HandleUtilitySkillLearn;
    }

    private void SetPlayerSkills()
    {
        List<UISkill> previousSignatureskills = new List<UISkill>(signatureSkillGroup.GetComponentsInChildren<UISkill>());
        List<UISkill> previousCommonskills = new List<UISkill>(commonSkillGroup.GetComponentsInChildren<UISkill>());
        List<UISkill> previousUtilityskills = new List<UISkill>(utilitySkillGroup.GetComponentsInChildren<UISkill>());

        foreach (UISkill skill in previousSignatureskills)
        {
            skill.DestroyUISkill();
        }

        foreach (UISkill skill in previousCommonskills)
        {
            skill.DestroyUISkill();
        }

        foreach (UISkill skill in previousUtilityskills)
        {
            skill.DestroyUISkill();
        }

        GameObject SignatureMeterSkillPrefab = Instantiate(uiSignatureMeterSkillPrefab);
        SignatureMeterSkillPrefab.transform.SetParent(signatureSkillGroup, false);
        SignatureMeterSkillPrefab.GetComponent<UISignatureMeterSkill>().SetSkillData(playerSkills.signatureMeterSkill, playerSkills.maxSignaturePoint, playerSkills.dePointIcon, playerSkills.acPointIcon);

        for (int i = 0; i < playerSkills.signatureSkills.Count; i++)
        {
            GameObject SignatureSkillPrefab = Instantiate(uiSkillPrefab);
            SignatureSkillPrefab.transform.SetParent(signatureSkillGroup, false);
            SignatureSkillPrefab.GetComponent<UISkill>().SetSkillData(playerSkills.signatureSkills[i]);
        }
    }

    private void HandleSignatureSkillLearn(SkillSO skill)
    {
        // Instantiate
        // Animating (Later)
    }

    private void HandleCommonSkillLearn(SkillSO skill)
    {

    }

    private void HandleUtilitySkillLearn(SkillSO skill)
    {

    }
}
