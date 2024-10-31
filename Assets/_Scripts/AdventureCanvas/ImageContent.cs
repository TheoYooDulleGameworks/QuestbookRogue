using TMPro;
using UnityEngine;

public class ImageContent : MonoBehaviour
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
}
