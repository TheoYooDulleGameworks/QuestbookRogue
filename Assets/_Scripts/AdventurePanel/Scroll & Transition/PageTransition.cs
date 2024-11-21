using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PageTransition : MonoBehaviour
{
    private void OnEnable()
    {
        RectTransform rect = GetComponent<RectTransform>();

        rect.DOScale(Vector3.one, 1.03f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
