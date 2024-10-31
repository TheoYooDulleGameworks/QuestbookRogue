using UnityEngine;
using TMPro;

public class DescriptionContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI bodyTextTMPro = null;
    [SerializeField] private TextMeshProUGUI cancelTextTMPro = null;

    public void SetContentComponents(ContentSO _contentData)
    {
        contentData = _contentData;

        bodyTextTMPro.text = contentData.bodyText;
        cancelTextTMPro.text = contentData.cancelText;
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
