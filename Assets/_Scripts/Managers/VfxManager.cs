using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VfxManager : Singleton<VfxManager>
{

    [Header("Panels")]
    [SerializeField] private RectTransform playerPanel;
    [SerializeField] private RectTransform adventurePanel;

    [Header("Enemy -> Player")]
    [SerializeField] private GameObject playerBasicImpactVfxPrefab;
    [SerializeField] private GameObject playerBlockImpactVfxPrefab;

    [Header("Player -> Enemy")]
    [SerializeField] private GameObject basicAttackVfxPrefab;
    [SerializeField] private GameObject blockAttackVfxPrefab;

    [Header("Dice FX")]
    [SerializeField] private GameObject diceUpVfxPrefab;
    [SerializeField] private GameObject diceDownVfxPrefab;

    [Header("Vignette")]
    [SerializeField] private RectTransform vignetteRect;
    [SerializeField] private Sprite impactVignette;
    [SerializeField] private Sprite blockVignette;



    // Enemy -> Player Attack //

    public void PlayerBasicImpactVfx(RectTransform playerProfilePosition)
    {
        GameObject vfx = Instantiate(playerBasicImpactVfxPrefab);
        vfx.transform.SetParent(playerPanel, false);
        vfx.transform.position = playerProfilePosition.transform.position;

        AudioManager.Instance.PlaySfx("PlayerImpact");

        vignetteRect.GetComponent<Image>().sprite = impactVignette;
        vignetteRect.GetComponent<Image>().DOFade(1, 0.1f).OnComplete(() =>
        {
            vignetteRect.GetComponent<Image>().DOFade(0, 1.9f);
        });
    }

    public void PlayerBasicBlockVfx(RectTransform playerProfilePosition)
    {
        GameObject vfx = Instantiate(playerBlockImpactVfxPrefab);
        vfx.transform.SetParent(playerPanel, false);
        vfx.transform.position = playerProfilePosition.transform.position;

        AudioManager.Instance.PlaySfx("PlayerBlock");

        vignetteRect.GetComponent<Image>().sprite = blockVignette;
        vignetteRect.GetComponent<Image>().DOFade(1, 0.1f).OnComplete(() =>
        {
            vignetteRect.GetComponent<Image>().DOFade(0, 1.9f);
        });
    }



    // Player -> Enemy Attack //

    public void BasicAttackVfx(RectTransform targetPosition)
    {
        GameObject vfx = Instantiate(basicAttackVfxPrefab);
        vfx.transform.SetParent(adventurePanel, false);
        vfx.transform.position = targetPosition.transform.position;

        AudioManager.Instance.PlaySfx("RogueBasicAttack");
    }

    public void BlockAttackVfx(RectTransform targetPosition)
    {
        GameObject vfx = Instantiate(blockAttackVfxPrefab);
        vfx.transform.SetParent(adventurePanel, false);
        vfx.transform.position = targetPosition.transform.position;

        AudioManager.Instance.PlaySfx("BlockedAttack");
    }

    // Dice FX //

    public void DiceUpVfx(RectTransform targetPosition)
    {
        GameObject vfx = Instantiate(diceUpVfxPrefab);
        vfx.transform.SetParent(adventurePanel, false);

        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            vfx.GetComponent<RectTransform>() as RectTransform,
            targetPosition.position,
            null,
            out localPosition);

        vfx.GetComponent<RectTransform>().anchoredPosition = localPosition;
    }

    public void DiceDownVfx(RectTransform targetPosition)
    {
        GameObject vfx = Instantiate(diceDownVfxPrefab);
        vfx.transform.SetParent(adventurePanel, false);

        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            vfx.GetComponent<RectTransform>() as RectTransform,
            targetPosition.position,
            null,
            out localPosition);

        vfx.GetComponent<RectTransform>().anchoredPosition = localPosition;
    }
}
