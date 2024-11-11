using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO", menuName = "Scriptable Objects/Quests/QuestSO")]
public class QuestSO : ScriptableObject
{
    public int questID;
    public string questName;

    public Sprite questThumbnail;
    public Sprite questSeal;
    public QuestType questType;

    public List<ContentSO> questContents;
    public EnemySO enemyData;
}

public enum QuestType
{
    General,
    Combat,
    Expedition,
    Intrigue,
    Boss,
    Village,
    Point,
    Encounter,
    Secret
}