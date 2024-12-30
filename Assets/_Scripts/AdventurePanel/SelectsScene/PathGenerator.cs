using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform nodeParentRect;

    [Header("Assets")]
    [SerializeField] private GameObject evenDepthPrefab;
    [SerializeField] private GameObject oddDepthPrefab;

    private void Start()
    {
        GeneratePaths(24);
    }

    public void GeneratePaths(int depthQuantity)
    {
        float baseX = 0;
        float baseY = 0;
        float offsetX = 184;
        float offsetYSmall = 96;
        float offsetYLarge = 192;

        for (int i = 0; i < depthQuantity; i++)
        {
            GameObject newDepth;
            RectTransform newDepthRect;
            
            if (i % 2 == 0) // 짝수
            {
                newDepth = Instantiate(evenDepthPrefab);
            }
            else // 홀수
            {
                newDepth = Instantiate(oddDepthPrefab);
            }

            newDepth.transform.SetParent(nodeParentRect, false);
            newDepthRect = newDepth.GetComponent<RectTransform>();

            if (i == 0) // 0
            {
                baseX = 0;
                baseY = 0;
            }
            else if (i % 2 == 0) // 짝수
            {
                baseY += offsetYLarge;
            }
            else // 홀수
            {
                baseX += offsetX;
                baseY += offsetYSmall;
            }

            newDepthRect.anchoredPosition = new Vector3(baseX, baseY, 0);
            newDepth.GetComponent<NodesController>().OnOffNodes(i);
        }
    }
}