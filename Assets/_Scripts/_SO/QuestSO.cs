using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO", menuName = "Scriptable Objects/Quests/QuestSO")]
public class QuestSO : ScriptableObject
{
    [Header("__________ ID _______________________________________________________________")]
    public int questID;
    public string questName;

    [Header("__________ TYPE _______________________________________________________________")]
    public QuestType questType;
    public Sprite questSeal;

    [Header("__________ CONTENTS _______________________________________________________________")]
    public Sprite questMainImage;
    public Sprite questBackgroundImage;
    public Sprite questBeltImage;
    public List<ContentSO> questContents;

    [Header("__________ COMBAT _______________________________________________________________")]
    public EnemySO enemyData;
    public Sprite hittedEnemyShader;
    public Sprite deadEnemyImage;
}

public enum QuestType
{
    General,
    Combat,
    BossCombat,
    Trap,
    Exploration,
    Encounter,
    Mission,
    Town,
    Merchant,
    Secret
}