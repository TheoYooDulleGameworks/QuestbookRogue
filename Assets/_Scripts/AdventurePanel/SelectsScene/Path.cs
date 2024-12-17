using UnityEngine;

public class Path : MonoBehaviour
{

    public void ActivatePath()
    {
        gameObject.SetActive(true);
    }

    public void DeActivatePath()
    {
        gameObject.SetActive(false);
    }

    public bool CheckOnPath()
    {
        if (gameObject.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
