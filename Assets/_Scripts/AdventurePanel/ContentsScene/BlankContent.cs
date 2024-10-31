using UnityEngine;

public class BlankContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    public void SetContentComponents(ContentSO _contentData)
    {
        contentData = _contentData;
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
