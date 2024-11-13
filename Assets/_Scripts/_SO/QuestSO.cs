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
    public List<ContentSO> questContents;

    [Header("__________ COMBAT _______________________________________________________________")]
    public EnemySO enemyData;
}

public enum QuestType
{
    General,
    Combat,
    Exploration,
    Encounter,
    Town,
    Secret
}