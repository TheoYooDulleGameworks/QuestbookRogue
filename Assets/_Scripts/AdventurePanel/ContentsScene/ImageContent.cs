using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI questTitleTMPro = null;
    [SerializeField] private RectTransform questImageRect = null;
    [SerializeField] private RectTransform questSealRect = null;
    [SerializeField] private RectTransform cancelButtonRect = null;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultCancelButton;
    [SerializeField] private Sprite mouseOverCancelButton;
    [SerializeField] private Sprite mouseDownCancelButton;
    [SerializeField] private Sprite activatedCompleteButton;
    [SerializeField] private Sprite mouseOverCompleteButton;
    [SerializeField] private Sprite mouseDownCompleteButton;

    public void SetContentComponents(ContentSO _contentData)
    {
        contentData = _contentData;

        questTitleTMPro.text = contentData.questTitle;
        questImageRect.GetComponent<Image>().sprite = contentData.questImage;
        questSealRect.GetComponent<Image>().sprite = contentData.questSeal;

        if (contentData.isThereCancelButton)
        {
            cancelButtonRect.gameObject.SetActive(true);
            cancelButtonRect.GetComponent<Image>().sprite = defaultCancelButton;
        }
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
