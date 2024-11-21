using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnScroll : MonoBehaviour
{
    private void OnEnable()
    {
        Image image = GetComponent<Image>();
        RectTransform rect = GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector3(0, 176, 0);

        image.DOKill();
        rect.DOKill();

        image.DOFade(1, 0.25f);
        rect.DOAnchorPos(new Vector3(0, 218, 0), 0.5f);
        rect.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).OnComplete(() =>
        {
            rect.DOAnchorPos(new Vector3(0, 210, 0), 0.5f);
            rect.DOScale(Vector3.one, 0.5f).OnComplete(() =>
            {
                rect.DOScale(Vector3.one, 0.25f).OnComplete(() =>
                {
                    rect.DOAnchorPos(new Vector3(0, 206, 0), 0.25f);
                    image.DOFade(0, 0.25f).OnComplete(() =>
                    {
                        this.gameObject.SetActive(false);
                    });
                });
            });
        });
    }
}
