using UnityEngine;

public class NodesController : MonoBehaviour
{
    [Header("EVEN DEPTH")]
    [SerializeField] private PathNode node_A;
    [SerializeField] private PathNode node_B;
    [SerializeField] private PathNode node_C;
    [SerializeField] private PathNode node_D;
    [SerializeField] private PathNode node_E;
    [SerializeField] private PathNode node_F;
    [SerializeField] private PathNode node_G;

    [Header("ODD DEPTH")]
    [SerializeField] private PathNode node_U;
    [SerializeField] private PathNode node_V;
    [SerializeField] private PathNode node_W;
    [SerializeField] private PathNode node_Y;
    [SerializeField] private PathNode node_X;
    [SerializeField] private PathNode node_Z;

    public void OnOffNodes(int depthCount, int lastCount)
    {

        if (depthCount == 0)
        {
            node_A.DeActivateNode();
            node_B.DeActivateNode();
            node_C.DeActivateNode();
            node_D.FullActivateNode();
            node_E.DeActivateNode();
            node_F.DeActivateNode();
            node_G.DeActivateNode();
        }
        else if (depthCount == 1)
        {
            node_U.DeActivateNode();
            node_V.DeActivateNode();
            node_W.FullActivateNode();
            node_Y.FullActivateNode();
            node_X.DeActivateNode();
            node_Z.DeActivateNode();
        }
        else if (depthCount == 2)
        {
            node_A.DeActivateNode();
            node_B.DeActivateNode();
            node_C.FullActivateNode();
            node_D.FullActivateNode();
            node_E.FullActivateNode();
            node_F.DeActivateNode();
            node_G.DeActivateNode();
        }
        else if (depthCount == 3)
        {
            node_U.DeActivateNode();
            node_V.FullActivateNode();
            node_W.FullActivateNode();
            node_Y.FullActivateNode();
            node_X.FullActivateNode();
            node_Z.DeActivateNode();
        }
        else if (depthCount == 4)
        {
            node_A.DeActivateNode();
            node_B.FullActivateNode();
            node_C.FullActivateNode();
            node_D.FullActivateNode();
            node_E.FullActivateNode();
            node_F.FullActivateNode();
            node_G.DeActivateNode();
        }
        else if (depthCount % 2 == 1)
        {
            node_U.FullActivateNode();
            node_V.FullActivateNode();
            node_W.FullActivateNode();
            node_Y.FullActivateNode();
            node_X.FullActivateNode();
            node_Z.FullActivateNode();
        }
        else
        {
            node_A.FullActivateNode();
            node_B.FullActivateNode();
            node_C.FullActivateNode();
            node_D.FullActivateNode();
            node_E.FullActivateNode();
            node_F.FullActivateNode();
            node_G.FullActivateNode();
        }

        if (depthCount == lastCount)
        {
            node_A.DeActivateNode();
            node_B.DeActivateNode();
            node_C.DeActivateNode();
            node_D.LastNode();
            node_E.DeActivateNode();
            node_F.DeActivateNode();
            node_G.DeActivateNode();
        }
        else if (depthCount == lastCount - 1)
        {
            node_U.DeActivateNode();
            node_V.DeActivateNode();
            node_W.LeftWingNode();
            node_Y.RightWingNode();
            node_X.DeActivateNode();
            node_Z.DeActivateNode();
        }
        else if (depthCount == lastCount - 2)
        {
            node_A.DeActivateNode();
            node_B.DeActivateNode();
            node_C.LeftWingNode();
            node_D.FullActivateNode();
            node_E.RightWingNode();
            node_F.DeActivateNode();
            node_G.DeActivateNode();
        }
        else if (depthCount == lastCount - 3)
        {
            node_U.DeActivateNode();
            node_V.LeftWingNode();
            node_W.FullActivateNode();
            node_Y.FullActivateNode();
            node_X.RightWingNode();
            node_Z.DeActivateNode();
        }
        else if (depthCount == lastCount - 4)
        {
            node_A.DeActivateNode();
            node_B.LeftWingNode();
            node_C.FullActivateNode();
            node_D.FullActivateNode();
            node_E.FullActivateNode();
            node_F.RightWingNode();
            node_G.DeActivateNode();
        }
        else if (depthCount == lastCount - 5)
        {
            node_U.LeftWingNode();
            node_Z.RightWingNode();
        }
        else if (depthCount == lastCount - 6)
        {
            node_A.LeftWingNode();
            node_G.RightWingNode();
        }
    }
}


/*

List<Path> activatedPaths = GetComponentsInChildren<Path>().Where(path => path.CheckOnPath()).ToList();

foreach (Path path in activatedPaths)

*/