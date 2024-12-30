using UnityEngine;

public class NodesController : MonoBehaviour
{
    [Header("EVEN DEPTH")]
    [SerializeField] private RectTransform node_A;
    [SerializeField] private RectTransform node_B;
    [SerializeField] private RectTransform node_C;
    [SerializeField] private RectTransform node_D;
    [SerializeField] private RectTransform node_E;
    [SerializeField] private RectTransform node_F;
    [SerializeField] private RectTransform node_G;

    [Header("ODD DEPTH")]
    [SerializeField] private RectTransform node_U;
    [SerializeField] private RectTransform node_V;
    [SerializeField] private RectTransform node_W;
    [SerializeField] private RectTransform node_Y;
    [SerializeField] private RectTransform node_X;
    [SerializeField] private RectTransform node_Z;

    public void OnOffNodes(int depthCount)
    {
        if (depthCount == 0)
        {
            node_A.gameObject.SetActive(false);
            node_B.gameObject.SetActive(false);
            node_C.gameObject.SetActive(false);
            node_D.gameObject.SetActive(true);
            node_E.gameObject.SetActive(false);
            node_F.gameObject.SetActive(false);
            node_G.gameObject.SetActive(false);
        }
        else if (depthCount == 1)
        {
            node_U.gameObject.SetActive(false);
            node_V.gameObject.SetActive(false);
            node_W.gameObject.SetActive(true);
            node_Y.gameObject.SetActive(true);
            node_X.gameObject.SetActive(false);
            node_Z.gameObject.SetActive(false);
        }
        else if (depthCount == 2)
        {
            node_A.gameObject.SetActive(false);
            node_B.gameObject.SetActive(false);
            node_C.gameObject.SetActive(true);
            node_D.gameObject.SetActive(true);
            node_E.gameObject.SetActive(true);
            node_F.gameObject.SetActive(false);
            node_G.gameObject.SetActive(false);
        }
        else if (depthCount == 3)
        {
            node_U.gameObject.SetActive(false);
            node_V.gameObject.SetActive(true);
            node_W.gameObject.SetActive(true);
            node_Y.gameObject.SetActive(true);
            node_X.gameObject.SetActive(true);
            node_Z.gameObject.SetActive(false);
        }
        else if (depthCount == 4)
        {
            node_A.gameObject.SetActive(false);
            node_B.gameObject.SetActive(true);
            node_C.gameObject.SetActive(true);
            node_D.gameObject.SetActive(true);
            node_E.gameObject.SetActive(true);
            node_F.gameObject.SetActive(true);
            node_G.gameObject.SetActive(false);
        }
        else if (depthCount % 2 == 1)
        {
            node_U.gameObject.SetActive(true);
            node_V.gameObject.SetActive(true);
            node_W.gameObject.SetActive(true);
            node_Y.gameObject.SetActive(true);
            node_X.gameObject.SetActive(true);
            node_Z.gameObject.SetActive(true);
        }
        else
        {
            node_A.gameObject.SetActive(true);
            node_B.gameObject.SetActive(true);
            node_C.gameObject.SetActive(true);
            node_D.gameObject.SetActive(true);
            node_E.gameObject.SetActive(true);
            node_F.gameObject.SetActive(true);
            node_G.gameObject.SetActive(true);
        }
    }
}
