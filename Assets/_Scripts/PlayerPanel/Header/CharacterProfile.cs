using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterProfile : MonoBehaviour
{

    [SerializeField] private RectTransform hittedImage;

    public void HittedGetDamage()
    {
        RectTransform profileCard = GetComponent<RectTransform>();

        profileCard.DOShakeRotation(0.35f, 20, 0, 15);

        profileCard.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f).OnComplete(() =>
        {
            profileCard.DOScale(Vector3.one, 0.2f);
        });


        hittedImage.GetComponent<Image>().DOFade(1, 0.2f).OnComplete(() =>
        {
            hittedImage.GetComponent<Image>().DOFade(0, 0.1f);
        });

    }

    public void HittedBlock()
    {
        RectTransform profileCard = GetComponent<RectTransform>();

        profileCard.DOShakeRotation(0.25f, 10, 0, 15);
        profileCard.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.1f).OnComplete(() =>
        {
            profileCard.DOScale(Vector3.one, 0.15f);
        });
    }
}
