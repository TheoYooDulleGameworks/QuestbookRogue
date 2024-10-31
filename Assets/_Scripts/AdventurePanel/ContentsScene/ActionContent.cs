using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionContent : MonoBehaviour, IContent
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI actionTitleTMPro = null;
    [SerializeField] private RectTransform actionSealRect = null;
    [SerializeField] private TextMeshProUGUI rewardTitleTMPro = null;
    [SerializeField] private RectTransform proceedButtonRect = null;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultProceedButton;
    [SerializeField] private Sprite activatedProceedButton;
    [SerializeField] private Sprite mouseOverProceedButton;
    [SerializeField] private Sprite mouseDownProceedButton;

    // [Header("Dice Slots")]
    // [SerializeField] List<DiceSlot> possibleDiceSlots;

    public void SetContentComponents(ContentSO _contentData)
    {
        contentData = _contentData;

        actionTitleTMPro.text = contentData.actionTitle;
        actionSealRect.GetComponent<Image>().sprite = contentData.actionSeal;
        rewardTitleTMPro.text = contentData.actionRewardText;

        if (contentData.isThereProceedButton)
        {
            proceedButtonRect.gameObject.SetActive(true);
            proceedButtonRect.GetComponent<Image>().sprite = defaultProceedButton;
        }
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }
}
