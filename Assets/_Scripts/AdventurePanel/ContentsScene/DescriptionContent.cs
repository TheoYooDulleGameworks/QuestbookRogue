using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DescriptionContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Components")]
    [SerializeField] private RectTransform backgroundImageRect = null;
    [SerializeField] private TextMeshProUGUI bodyTextTMPro = null;
    [SerializeField] private TextMeshProUGUI cancelTextTMPro = null;

    public void SetContentComponents(ContentSO _contentData)
    {
        contentData = _contentData;

        backgroundImageRect.GetComponent<Image>().sprite = contentData.backgroundImage;
        bodyTextTMPro.text = contentData.bodyText;
        cancelTextTMPro.text = contentData.cancelText;
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
