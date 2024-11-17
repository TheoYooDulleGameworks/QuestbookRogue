using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VfxManager : Singleton<VfxManager>
{
    // Vfx Prefabs

    [SerializeField] private RectTransform playerPanel;
    [SerializeField] private RectTransform adventurePanel;

    [SerializeField] private GameObject playerBasicImpactVfxPrefab;
    [SerializeField] private GameObject playerBlockImpactVfxPrefab;

    [SerializeField] private GameObject basicAttackVfxPrefab;
    [SerializeField] private GameObject blockAttackVfxPrefab;

    // Vignette

    [SerializeField] private RectTransform vignetteRect;
    [SerializeField] private Sprite impactVignette;
    [SerializeField] private Sprite blockVignette;

    public void PlayerBasicImpactVfx(RectTransform playerProfilePosition)
    {
        GameObject vfx = Instantiate(playerBasicImpactVfxPrefab);
        vfx.transform.SetParent(playerPanel, false);
        vfx.transform.position = playerProfilePosition.transform.position;

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

        vignetteRect.GetComponent<Image>().sprite = blockVignette;
        vignetteRect.GetComponent<Image>().DOFade(1, 0.1f).OnComplete(() =>
        {
            vignetteRect.GetComponent<Image>().DOFade(0, 1.9f);
        });
    }

    public void BasicAttackVfx(RectTransform targetPosition)
    {
        GameObject vfx = Instantiate(basicAttackVfxPrefab);
        vfx.transform.SetParent(adventurePanel, false);
        vfx.transform.position = targetPosition.transform.position;
    }

    public void BlockAttackVfx(RectTransform targetPosition)
    {
        GameObject vfx = Instantiate(blockAttackVfxPrefab);
        vfx.transform.SetParent(adventurePanel, false);
        vfx.transform.position = targetPosition.transform.position;
    }
}
