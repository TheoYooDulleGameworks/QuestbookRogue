using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO", menuName = "Scriptable Objects/QuestSO")]
public class QuestSO : ScriptableObject
{
    [SerializeField] public int questID;
    [SerializeField] public string questName;
    [SerializeField] public string questType;
    [SerializeField] public Sprite questSeal;
    [SerializeField] public Sprite questThumbnail;

    [SerializeField] public Sprite questContentBG;
}
