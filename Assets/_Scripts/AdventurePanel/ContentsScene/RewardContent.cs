using UnityEngine;

public class RewardContent : MonoBehaviour, IContent
{
    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        throw new System.NotImplementedException();
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
