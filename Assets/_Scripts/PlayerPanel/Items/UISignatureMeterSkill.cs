using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISignatureMeterSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("SkillData")]
    public SkillSO skillData;

    [Header("Point Management")]
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private Sprite dePointIcon;
    [SerializeField] private Sprite acPointIcon;
    [SerializeField] private PlayerSkillSO playerSkills;
    [SerializeField] private List<GameObject> signaturePointPrefabs;

    [Header("Components")]
    [SerializeField] private RectTransform skillImage;
    [SerializeField] private RectTransform pointParents_1;
    [SerializeField] private RectTransform pointParents_2;

    public void SetSkillData(SkillSO _skillData, int maxSignaturePoint, Sprite _dePointIcon, Sprite _acPointIcon)
    {
        skillData = _skillData;
        skillImage.GetComponent<Image>().sprite = _skillData.defaultSprite;

        dePointIcon = _dePointIcon;
        acPointIcon = _acPointIcon;

        if (maxSignaturePoint <= 6 && maxSignaturePoint > 0)
        {
            for (int i = 0; i < maxSignaturePoint; i++)
            {
                GameObject signaturePointPrefab = Instantiate(pointPrefab);
                signaturePointPrefab.transform.SetParent(pointParents_1, false);
                signaturePointPrefab.GetComponent<Image>().sprite = dePointIcon;

                signaturePointPrefabs.Add(signaturePointPrefab);
            }
        }
        else if (maxSignaturePoint == 8)
        {
            pointParents_1.anchoredPosition = new Vector3(16, 14, 0);

            for (int i = 0; i < 4; i++)
            {
                GameObject signaturePointPrefab = Instantiate(pointPrefab);
                signaturePointPrefab.transform.SetParent(pointParents_1, false);
                signaturePointPrefab.GetComponent<Image>().sprite = dePointIcon;

                signaturePointPrefabs.Add(signaturePointPrefab);
            }

            for (int i = 4; i < 8; i++)
            {
                GameObject signaturePointPrefab = Instantiate(pointPrefab);
                signaturePointPrefab.transform.SetParent(pointParents_2, false);
                signaturePointPrefab.GetComponent<Image>().sprite = dePointIcon;

                signaturePointPrefabs.Add(signaturePointPrefab);
            }
        }
        else if (maxSignaturePoint == 10)
        {
            pointParents_1.anchoredPosition = new Vector3(16, 14, 0);

            for (int i = 0; i < 5; i++)
            {
                GameObject signaturePointPrefab = Instantiate(pointPrefab);
                signaturePointPrefab.transform.SetParent(pointParents_1, false);
                signaturePointPrefab.GetComponent<Image>().sprite = dePointIcon;

                signaturePointPrefabs.Add(signaturePointPrefab);
            }

            for (int i = 5; i < 10; i++)
            {
                GameObject signaturePointPrefab = Instantiate(pointPrefab);
                signaturePointPrefab.transform.SetParent(pointParents_2, false);
                signaturePointPrefab.GetComponent<Image>().sprite = dePointIcon;

                signaturePointPrefabs.Add(signaturePointPrefab);
            }
        }
        else if (maxSignaturePoint == 12)
        {
            pointParents_1.anchoredPosition = new Vector3(16, 14, 0);

            for (int i = 0; i < 6; i++)
            {
                GameObject signaturePointPrefab = Instantiate(pointPrefab);
                signaturePointPrefab.transform.SetParent(pointParents_1, false);
                signaturePointPrefab.GetComponent<Image>().sprite = dePointIcon;

                signaturePointPrefabs.Add(signaturePointPrefab);
            }

            for (int i = 6; i < 12; i++)
            {
                GameObject signaturePointPrefab = Instantiate(pointPrefab);
                signaturePointPrefab.transform.SetParent(pointParents_2, false);
                signaturePointPrefab.GetComponent<Image>().sprite = dePointIcon;

                signaturePointPrefabs.Add(signaturePointPrefab);
            }
        }
        else
        {
            Debug.LogWarning($"Wrong Signature Point Setting of '{_skillData}'!");
        }

        playerSkills.signaturePoint.OnValueChanged += UpdateSignaturePoints;

        UpdateSignaturePoints();
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

    private void UpdateSignaturePoints()
    {
        foreach (var pointPrefab in signaturePointPrefabs)
        {
            pointPrefab.GetComponent<Image>().sprite = dePointIcon;
        }

        int currentPoint = playerSkills.signaturePoint.Value;

        if (currentPoint <= 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < currentPoint; i++)
            {
                signaturePointPrefabs[i].GetComponent<Image>().sprite = acPointIcon;
            }
        }
    }
}