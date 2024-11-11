using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CombatDescriptionContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Components")]
    [SerializeField] private RectTransform backgroundImageRect = null;
    [SerializeField] private TextMeshProUGUI bodyTextTMPro = null;
    [SerializeField] private TextMeshProUGUI cancelTextTMPro = null;

    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        contentData = _contentData;

        backgroundImageRect.GetComponent<Image>().sprite = contentData.backgroundImage;
        bodyTextTMPro.text = contentData.bodyText;
        cancelTextTMPro.text = contentData.cancelText;
    }

    public void FlipOnContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        contentCanvas.localEulerAngles = new Vector3(-2f, -90f, -2f);

        contentCanvas.DOKill();
        contentCanvas.DOScale(Vector3.one, 0.2f);
        contentCanvas.DORotate(Vector3.zero, 0.2f);
    }

    public void FlipOffContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();
        
        contentCanvas.localScale = Vector3.one;
        contentCanvas.localEulerAngles = Vector3.zero;

        contentCanvas.DOKill();
        contentCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.2f);
        contentCanvas.DORotate(new Vector3(2f, 90f, 2f), 0.2f);
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
