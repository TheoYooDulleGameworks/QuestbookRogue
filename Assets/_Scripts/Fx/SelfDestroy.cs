using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void DestroySelfAndParent()
    {
        Transform parent = transform.parent;
        Transform grandParent = transform.parent.parent;

        Destroy(gameObject);

        if (parent != null)
        {
            Destroy(parent.gameObject);
        }

        if (grandParent != null)
        {
            Destroy(grandParent.gameObject);
        }
    }
}
