using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("DiceData")]
    public SkillSO skillData;

    [Header("Components")]
    [SerializeField] private RectTransform skillImage;

    public void SetDiceData()
    {
        skillImage.GetComponent<Image>().sprite = skillData.defaultSprite;
    }

    public void DestroyUIDice()
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
        SkillManager.Instance.SkillCostPhase(skillData);
    }
}