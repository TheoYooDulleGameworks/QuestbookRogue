using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContentSO", menuName = "Scriptable Objects/Quests/ContentSO")]
public class ContentSO : ScriptableObject
{
    public enum ContentType
    {
        Blank,
        Image,
        Description,
        Action,
        Conclusion,
        Reward,
        Choose,
    }

    public ContentType contentType;

    [SerializeField] public GameObject contentTemplate;

    // Common //

    public Sprite backgroundImage;

    // Image Content //
    public string questTitle;
    public Sprite questImage;
    public Sprite questSeal;
    public bool isThereCancelButton = true;
    public bool isFreeCancel = false;

    // Description Content //
    [TextArea] public string bodyText;
    [TextArea] public string cancelText;

    // Action Content //
    public string actionTitle;
    public Sprite actionSeal;
    public List<SlotSO> actionRequestDiceSlots;
    public List<int> multiValue;
    [TextArea] public string actionRewardText;
    public List<ContentSO> rewardContents;
    public bool isThereProceedButton = true;

    // Conclusion Content //

    public string conclusionTitle;
    public Sprite conclusionSeal;
    [TextArea] public string conclusionText;


}