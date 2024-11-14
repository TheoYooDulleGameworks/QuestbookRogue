using UnityEngine;

public class VfxManager : Singleton<VfxManager>
{
    [SerializeField] private RectTransform adventurePanel;
    
    [SerializeField] private GameObject basicAttackVfxPrefab;
    [SerializeField] private GameObject blockedAttackVfxPrefab;

    public void BasicAttackVfx(RectTransform targetPosition)
    {
        GameObject vfx = Instantiate(basicAttackVfxPrefab);
        vfx.transform.SetParent(adventurePanel, false);
        vfx.transform.position = targetPosition.transform.position;
    }

    public void BlockedAttackVfx(RectTransform targetPosition)
    {
        GameObject vfx = Instantiate(blockedAttackVfxPrefab);
        vfx.transform.SetParent(adventurePanel, false);
        vfx.transform.position = targetPosition.transform.position;
    }
}
