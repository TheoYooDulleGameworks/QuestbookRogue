using UnityEngine;
using TMPro;

public class ActionContent : MonoBehaviour
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI actionTitleTMPro = null;
    [SerializeField] private RectTransform actionSealRect = null;
    [SerializeField] private TextMeshProUGUI rewardTitleTMPro = null;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultProceedButton;
    [SerializeField] private Sprite activatedProceedButton;
    [SerializeField] private Sprite mouseOverProceedButton;
    [SerializeField] private Sprite mouseDownProceedButton;

    // [Header("Dice Slots")]
    // [SerializeField] List<DiceSlot> possibleDiceSlots;
}
