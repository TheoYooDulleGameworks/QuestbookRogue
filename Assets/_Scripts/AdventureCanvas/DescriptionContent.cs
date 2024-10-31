using UnityEngine;
using TMPro;

public class DescriptionContent : MonoBehaviour
{
    [Header("Content Data")]
    [SerializeField] private ContentSO contentData = null;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI bodyTextTMPro = null;
    [SerializeField] private TextMeshProUGUI cancelTextTMPro = null;
}
