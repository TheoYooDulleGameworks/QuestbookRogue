using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPathSO", menuName = "Scriptable Objects/PlayerAssets/PlayerPathSO")]
public class PlayerPathSO : ScriptableObject
{
    [SerializeField] private List<QuestPoolSO> questPoolDatas;

    public List<QuestSO> currentPossibleQuestDatas;
    public List<QuestSO> impossibleQuestDatas;
    public List<QuestSO> triggerSavedQuestDatas;

    public void FirstSetPaths()
    {
        foreach (QuestPoolSO questPool in questPoolDatas)
        {
            foreach (QuestSO questData in questPool.questDatas)
            {
                currentPossibleQuestDatas.Add(questData);
            }
        }
    }

    public QuestSO PickRandomQuestData()
    {
        int randomNum = Random.Range(0, currentPossibleQuestDatas.Count);

        QuestSO pickedQuestData = currentPossibleQuestDatas[randomNum];
        currentPossibleQuestDatas.Remove(currentPossibleQuestDatas[randomNum]);

        return pickedQuestData;
    }
}