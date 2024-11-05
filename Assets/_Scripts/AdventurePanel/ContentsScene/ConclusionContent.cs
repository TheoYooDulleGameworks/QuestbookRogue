using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConclusionContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Components")]
    [SerializeField] private RectTransform backgroundImageRect = null;
    [SerializeField] private TextMeshProUGUI conclusionTitleTMPro = null;
    [SerializeField] private RectTransform conclusionSealRect = null;
    [SerializeField] private TextMeshProUGUI conclusionTextTMPro = null;

    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        contentData = _contentData;

        backgroundImageRect.GetComponent<Image>().sprite = contentData.backgroundImage;
        conclusionTitleTMPro.text = contentData.conclusionTitle;
        conclusionSealRect.GetComponent<Image>().sprite = contentData.conclusionSeal;
        conclusionTextTMPro.text = contentData.conclusionText;
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
