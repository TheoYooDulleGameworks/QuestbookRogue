using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Path : MonoBehaviour
{
    [SerializeField] private RectTransform questTypeImageRect;
    [SerializeField] private List<Sprite> questTypeSprites;

    public void ActivatePath()
    {
        gameObject.SetActive(true);
    }

    public void DeActivatePath()
    {
        gameObject.SetActive(false);
    }

    public void AssignQuestType(QuestType _questType)
    {
        int questTypeNum = (int)_questType;

        questTypeImageRect.GetComponent<Image>().sprite = questTypeSprites[questTypeNum];
        questTypeImageRect.gameObject.SetActive(true);
    }

    public bool CheckOnPath()
    {
        return gameObject.activeSelf;
    }
}
