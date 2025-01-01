using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathNode : MonoBehaviour
{
    [SerializeField] private RectTransform sealRect;
    [SerializeField] private RectTransform sealOutline;


    [SerializeField] private RectTransform trailUp;
    [SerializeField] private RectTransform trailRight;

    public void FullActivateNode()
    {
        gameObject.SetActive(true);
        trailUp.gameObject.SetActive(true);
        trailRight.gameObject.SetActive(true);
    }

    public void LeftWingNode()
    {
        gameObject.SetActive(true);
        trailUp.gameObject.SetActive(false);
        trailRight.gameObject.SetActive(true);
    }

    public void RightWingNode()
    {
        gameObject.SetActive(true);
        trailUp.gameObject.SetActive(true);
        trailRight.gameObject.SetActive(false);
    }

    public void LastNode()
    {
        gameObject.SetActive(true);
        trailUp.gameObject.SetActive(false);
        trailRight.gameObject.SetActive(false);
    }

    public void DeActivateNode()
    {
        gameObject.SetActive(false);
    }

    public bool CheckNode()
    {
        return gameObject.activeSelf;
    }
}
