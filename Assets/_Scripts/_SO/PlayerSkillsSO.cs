using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillSO", menuName = "Scriptable Objects/PlayerAssets/PlayerSkillSO")]
public class PlayerSkillSO : ScriptableObject
{
    public StatusValue signaturePoint;
    public int maxSignaturePoint;
    public Sprite dePointIcon;
    public Sprite acPointIcon;

    public SkillSO signatureMeterSkill;
    public List<SkillSO> signatureSkills;
    public List<SkillSO> commonSkills;
    public List<SkillSO> utilitySkills;

    public event Action<SkillSO> OnSignatureSkillLearn;

    public event Action<SkillSO> OnCommonSkillLearn;

    public event Action<SkillSO> OnUtilitySkillLearn;

    public void ResetPlayerSkills()
    {
        signatureMeterSkill = null;
        dePointIcon = null;
        acPointIcon = null;

        signatureSkills.Clear();
        commonSkills.Clear();
        utilitySkills.Clear();
    }

    public void LearnSignatureSkill(SkillSO skill)
    {
        signatureSkills.Add(skill);
        OnSignatureSkillLearn?.Invoke(skill);
    }

    public void LearnCommonSkill(SkillSO skill)
    {
        commonSkills.Add(skill);
        OnCommonSkillLearn?.Invoke(skill);
    }

    public void LearnUtilitySkill(SkillSO skill)
    {
        utilitySkills.Add(skill);
        OnUtilitySkillLearn?.Invoke(skill);
    }


}