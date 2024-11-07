using UnityEngine;
using DG.Tweening;

public class BlankContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        contentData = _contentData;
    }

    public void FlipOnContent()
    {
        // ~~~ Last Fade Out //

        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();
        CanvasGroup canvasAlpha = contentCanvas.GetComponent<CanvasGroup>();

        canvasAlpha.alpha = 1f;
        contentCanvas.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        contentCanvas.localEulerAngles = new Vector3(-2f, -90f, -2f);

        contentCanvas.DOKill();
        contentCanvas.DOScale(new Vector3(0.35f, 0.35f, 0.35f), 0.2f);
        contentCanvas.DORotate(Vector3.zero, 0.2f);
        canvasAlpha.DOFade(0, 0.2f);
    }

    public void FlipOffContent()
    {
        // First Fade In ~~~ //

        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();
        CanvasGroup canvasAlpha = contentCanvas.GetComponent<CanvasGroup>();

        canvasAlpha.alpha = 0f;
        contentCanvas.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        contentCanvas.localEulerAngles = Vector3.zero;

        contentCanvas.DOKill();
        contentCanvas.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.2f);
        contentCanvas.DORotate(new Vector3(2f, 90f, 2f), 0.2f);
        canvasAlpha.DOFade(1, 0.2f);
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
