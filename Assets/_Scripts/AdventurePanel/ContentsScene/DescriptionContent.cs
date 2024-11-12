using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DescriptionContent : MonoBehaviour, IContent
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

        contentCanvas.GetComponent<CanvasGroup>().alpha = 0;
        contentCanvas.localScale = new Vector3(0.75f, 0.5f, 0.5f);
        contentCanvas.localEulerAngles = new Vector3(-90f, 12f, 12f);

        contentCanvas.DOKill();
        contentCanvas.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f);
        contentCanvas.DORotate(new Vector3(4f, 12f, -2f), 0.25f);
        contentCanvas.GetComponent<CanvasGroup>().DOFade(1f, 0.25f).OnComplete(() =>
        {
            contentCanvas.DOKill();
            contentCanvas.DOScale(Vector3.one, 0.25f);
            contentCanvas.DORotate(Vector3.zero, 0.25f);
        });
    }

    public void FlipOffContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.localEulerAngles = Vector3.zero;
        contentCanvas.localScale = Vector3.one;

        contentCanvas.DOKill();
        contentCanvas.DORotate(new Vector3(-75f, -12f, -6f), 0.25f);
        contentCanvas.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 0.25f);
        contentCanvas.GetComponent<CanvasGroup>().DOFade(0, 0.25f);
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
