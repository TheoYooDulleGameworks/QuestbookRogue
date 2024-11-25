using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("SkillData")]
    public SkillSO skillData;

    [Header("Components")]
    [SerializeField] private RectTransform skillImage;

    private void OnEnable()
    {
        SetSkillData();
    }

    public void SetSkillData()
    {
        skillImage.GetComponent<Image>().sprite = skillData.defaultSprite;
    }

    public void DestroyUISkill()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillImage.GetComponent<Image>().sprite = skillData.defaultHoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillImage.GetComponent<Image>().sprite = skillData.defaultSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!skillData.isCooldown)
        {
            SkillManager.Instance.SkillCostPhase(skillData);
        }
    }
}