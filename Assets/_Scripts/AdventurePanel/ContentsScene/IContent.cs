using UnityEngine;

public interface IContent
{
    public void SetContentComponents(QuestSO _questData, ContentSO _contentData);
    public void FlipOnContent();
    public void FlipOffContent();
    public void DestroyContent();
}
