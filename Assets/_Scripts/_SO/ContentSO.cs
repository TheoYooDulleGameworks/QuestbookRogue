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
        Attack,
    }

    public ContentType contentType;

    [SerializeField] private GameObject blankContentTemplate;
    [SerializeField] private GameObject imageContentTemplate;
    [SerializeField] private GameObject descriptionContentTemplate;
    [SerializeField] private GameObject actionContentTemplate;


    // Image Content //
    public string questTitle;
    public Sprite questImage;
    public Sprite questSeal;
    public bool isThereCancelButton = true;

    // Description Content //
    [TextArea] public string bodyText;
    [TextArea] public string cancelText;

    // Action Content //
    public string actionTitle;
    public Sprite actionSeal;
    [TextArea] public string actionRewardText;
    public bool isThereProceedButton = true;
}