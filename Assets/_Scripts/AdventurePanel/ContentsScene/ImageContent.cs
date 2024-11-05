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
    [SerializeField] private Sprite defaultFreeCancelButton;
    [SerializeField] private Sprite mouseOverFreeCancelButton;
    [SerializeField] private Sprite mouseDownFreeCancelButton;

    public void SetContentComponents(QuestSO _questData, ContentSO _contentData)
    {
        contentData = _contentData;

        questTitleTMPro.text = contentData.questTitle;
        questImageRect.GetComponent<Image>().sprite = contentData.questImage;
        questSealRect.GetComponent<Image>().sprite = contentData.questSeal;

        if (contentData.isThereCancelButton)
        {
            cancelButtonRect.gameObject.SetActive(true);
            cancelButtonRect.GetComponent<CancelButton>().currentQuestData = _questData;

            if (contentData.isFreeCancel)
            {
                cancelButtonRect.GetComponent<Image>().sprite = defaultFreeCancelButton;
                cancelButtonRect.GetComponent<CancelButton>().isFreeToCancel = true;
            }
            else
            {
                cancelButtonRect.GetComponent<Image>().sprite = defaultCancelButton;
                cancelButtonRect.GetComponent<CancelButton>().isFreeToCancel = false;
            }
        }
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
