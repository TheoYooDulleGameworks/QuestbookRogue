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
        //
    }

    public void FlipOffContent()
    {
        RectTransform contentCanvas = GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();

        contentCanvas.localScale = Vector3.one;

        contentCanvas.DOKill();
        contentCanvas.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
