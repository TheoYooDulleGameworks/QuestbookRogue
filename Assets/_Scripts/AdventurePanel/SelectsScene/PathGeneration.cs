using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PathGeneration : MonoBehaviour
{
    [Header("Managing")]
    public int AvailableDepth = 0;
    public Path CurrentPath = null;

    [Header("Depth_1")]
    [SerializeField] private Path path1_A;
    [SerializeField] private Path path1_B;
    [SerializeField] private Path path1_C;
    [SerializeField] private Path path1_D;
    [Header("Depth_2")]
    [SerializeField] private Path path2_Q;
    [SerializeField] private Path path2_W;
    [SerializeField] private Path path2_E;
    [SerializeField] private Path path2_R;
    [SerializeField] private Path path2_T;
    [Header("Depth_3")]
    [SerializeField] private Path path3_A;
    [SerializeField] private Path path3_B;
    [SerializeField] private Path path3_C;
    [SerializeField] private Path path3_D;
    [Header("Depth_4")]
    [SerializeField] private Path path4_Q;
    [SerializeField] private Path path4_W;
    [SerializeField] private Path path4_E;
    [SerializeField] private Path path4_R;
    [SerializeField] private Path path4_T;
    [Header("Depth_5")]
    [SerializeField] private Path path5_A;
    [SerializeField] private Path path5_B;
    [SerializeField] private Path path5_C;
    [SerializeField] private Path path5_D;
    [Header("Depth_6")]
    [SerializeField] private Path path6_Q;
    [SerializeField] private Path path6_W;
    [SerializeField] private Path path6_E;
    [SerializeField] private Path path6_R;
    [SerializeField] private Path path6_T;
    [Header("Depth_7")]
    [SerializeField] private Path path7_A;
    [SerializeField] private Path path7_B;
    [SerializeField] private Path path7_C;
    [SerializeField] private Path path7_D;
    [Header("Depth_8")]
    [SerializeField] private Path path8_W;
    [SerializeField] private Path path8_E;
    [SerializeField] private Path path8_R;
    [Header("Depth_9")]
    [SerializeField] private Path path9;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetPaths();
        }
    }

    private void SetPaths()
    {
        // Depth 1 //////////////////////////////////////////////////////////////////////////////////////////////////////////

        bool D1_abc = false;
        bool D1_abcd = false;
        bool D1_bcd = false;

        int path1random = Random.Range(0, 3);
        if (path1random == 0)
        {
            path1_A.ActivatePath();
            path1_B.ActivatePath();
            path1_C.ActivatePath();
            path1_D.DeActivatePath();

            D1_abc = true;
        }
        else if (path1random == 1)
        {
            path1_A.ActivatePath();
            path1_B.ActivatePath();
            path1_C.ActivatePath();
            path1_D.ActivatePath();

            D1_abcd = true;
        }
        else
        {
            path1_A.DeActivatePath();
            path1_B.ActivatePath();
            path1_C.ActivatePath();
            path1_D.ActivatePath();

            D1_bcd = true;
        }

        // Depth 2 //////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (D1_abc)
        {
            path2_T.DeActivatePath();

            int path2random = Random.Range(0, 2);
            if (path2random == 0)
            {
                path2_Q.ActivatePath();
                path2_W.ActivatePath();
                path2_E.ActivatePath();
                path2_R.DeActivatePath();
            }
            else
            {
                path2_Q.DeActivatePath();
                path2_W.ActivatePath();
                path2_E.ActivatePath();
                path2_R.ActivatePath();
            }
        }
        else if (D1_abcd)
        {
            path2_W.ActivatePath();
            path2_E.ActivatePath();
            path2_R.ActivatePath();

            int path2randomQ = Random.Range(0, 2);
            if (path2randomQ == 0)
            {
                path2_Q.ActivatePath();
            }
            else
            {
                path2_Q.DeActivatePath();
            }

            int path2randomT = Random.Range(0, 2);
            if (path2randomT == 0)
            {
                path2_T.ActivatePath();
            }
            else
            {
                path2_T.DeActivatePath();
            }
        }
        else if (D1_bcd)
        {
            path2_Q.DeActivatePath();

            int path2random = Random.Range(0, 2);
            if (path2random == 0)
            {
                path2_W.ActivatePath();
                path2_E.ActivatePath();
                path2_R.ActivatePath();
                path2_T.DeActivatePath();
            }
            else
            {
                path2_W.DeActivatePath();
                path2_E.ActivatePath();
                path2_R.ActivatePath();
                path2_T.ActivatePath();
            }
        }

        // Depth 3 //////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (path2_Q.CheckOnPath() && path2_W.CheckOnPath() && path2_E.CheckOnPath() && path2_R.CheckOnPath() && path2_T.CheckOnPath())
        {
            // QWERT

            path3_A.ActivatePath();
            path3_B.ActivatePath();
            path3_C.ActivatePath();
            path3_D.ActivatePath();
        }
        else if (!path2_Q.CheckOnPath() && path2_W.CheckOnPath() && path2_E.CheckOnPath() && !path2_R.CheckOnPath() && !path2_T.CheckOnPath())
        {
            // WE

            path3_D.DeActivatePath();
            path3_B.ActivatePath();

            int path3random = Random.Range(0, 3);
            if (path3random == 0)
            {
                path3_A.ActivatePath();
                path3_C.DeActivatePath();
            }
            else if (path3random == 1)
            {
                path3_A.DeActivatePath();
                path3_C.ActivatePath();
            }
            else
            {
                path3_A.ActivatePath();
                path3_C.ActivatePath();
            }

        }
        else if (!path2_Q.CheckOnPath() && !path2_W.CheckOnPath() && path2_E.CheckOnPath() && path2_R.CheckOnPath() && !path2_T.CheckOnPath())
        {
            // ER

            path3_A.DeActivatePath();
            path3_C.ActivatePath();

            int path3random = Random.Range(0, 3);
            if (path3random == 0)
            {
                path3_B.ActivatePath();
                path3_D.DeActivatePath();
            }
            else if (path3random == 1)
            {
                path3_B.DeActivatePath();
                path3_D.ActivatePath();
            }
            else
            {
                path3_B.ActivatePath();
                path3_D.ActivatePath();
            }
        }
        else if (!path2_Q.CheckOnPath() && path2_W.CheckOnPath() && path2_E.CheckOnPath() && path2_R.CheckOnPath() && !path2_T.CheckOnPath())
        {
            // WER

            path3_B.ActivatePath();
            path3_C.ActivatePath();

            int path3randomA = Random.Range(0, 2);
            if (path3randomA == 0)
            {
                path3_A.ActivatePath();
            }
            else
            {
                path3_A.DeActivatePath();
            }

            int path3randomD = Random.Range(0, 2);
            if (path3randomD == 0)
            {
                path3_D.ActivatePath();
            }
            else
            {
                path3_D.DeActivatePath();
            }
        }
        else if (path2_Q.CheckOnPath() && path2_W.CheckOnPath() && path2_E.CheckOnPath() && !path2_R.CheckOnPath() && !path2_T.CheckOnPath())
        {
            // QWE

            path3_D.DeActivatePath();
            path3_A.ActivatePath();
            path3_B.ActivatePath();
            path3_C.ActivatePath();
        }
        else if (path2_Q.CheckOnPath() && path2_W.CheckOnPath() && path2_E.CheckOnPath() && path2_R.CheckOnPath() && !path2_T.CheckOnPath())
        {
            // QWER

            path3_A.ActivatePath();
            path3_B.ActivatePath();
            path3_C.ActivatePath();

            int path3randomD = Random.Range(0, 2);
            if (path3randomD == 0)
            {
                path3_D.ActivatePath();
            }
            else
            {
                path3_D.DeActivatePath();
            }

        }
        else if (!path2_Q.CheckOnPath() && !path2_W.CheckOnPath() && path2_E.CheckOnPath() && path2_R.CheckOnPath() && path2_T.CheckOnPath())
        {
            // ERT

            path3_A.DeActivatePath();
            path3_B.ActivatePath();
            path3_C.ActivatePath();
            path3_D.ActivatePath();
        }
        else if (!path2_Q.CheckOnPath() && path2_W.CheckOnPath() && path2_E.CheckOnPath() && path2_R.CheckOnPath() && path2_T.CheckOnPath())
        {
            // WERT

            path3_B.ActivatePath();
            path3_C.ActivatePath();
            path3_D.ActivatePath();

            int path3randomA = Random.Range(0, 2);
            if (path3randomA == 0)
            {
                path3_A.ActivatePath();
            }
            else
            {
                path3_A.DeActivatePath();
            }
        }

        // Depth 4 //////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (path3_A.CheckOnPath() && path3_B.CheckOnPath() && path3_C.CheckOnPath() && path3_D.CheckOnPath())
        {
            // ABCD

            path4_W.ActivatePath();
            path4_E.ActivatePath();
            path4_R.ActivatePath();

            int path4randomQ = Random.Range(0, 2);
            if (path4randomQ == 0)
            {
                path4_Q.ActivatePath();
            }
            else
            {
                path4_Q.DeActivatePath();
            }

            int path4randomT = Random.Range(0, 2);
            if (path4randomT == 0)
            {
                path4_T.ActivatePath();
            }
            else
            {
                path4_T.DeActivatePath();
            }
        }
        else if (!path3_A.CheckOnPath() && path3_B.CheckOnPath() && path3_C.CheckOnPath() && !path3_D.CheckOnPath())
        {
            // BC

            path4_Q.DeActivatePath();
            path4_W.ActivatePath();
            path4_E.ActivatePath();
            path4_R.ActivatePath();
            path4_T.DeActivatePath();
        }
        else if (path3_A.CheckOnPath() && path3_B.CheckOnPath() && !path3_C.CheckOnPath() && !path3_D.CheckOnPath())
        {
            // AB

            path4_Q.ActivatePath();
            path4_W.ActivatePath();
            path4_E.ActivatePath();
            path4_R.DeActivatePath();
            path4_T.DeActivatePath();
        }
        else if (!path3_A.CheckOnPath() && !path3_B.CheckOnPath() && path3_C.CheckOnPath() && path3_D.CheckOnPath())
        {
            // CD

            path4_Q.DeActivatePath();
            path4_W.DeActivatePath();
            path4_E.ActivatePath();
            path4_R.ActivatePath();
            path4_T.ActivatePath();
        }
        else if (path3_A.CheckOnPath() && path3_B.CheckOnPath() && path3_C.CheckOnPath() && !path3_D.CheckOnPath())
        {
            // ABC

            path4_T.DeActivatePath();

            path4_Q.ActivatePath();
            path4_W.ActivatePath();
            path4_E.ActivatePath();

            int path4randomR = Random.Range(0, 2);
            if (path4randomR == 0)
            {
                path4_R.ActivatePath();
            }
            else
            {
                path4_R.DeActivatePath();
            }
        }
        else if (!path3_A.CheckOnPath() && path3_B.CheckOnPath() && path3_C.CheckOnPath() && path3_D.CheckOnPath())
        {
            // BCD

            path4_Q.DeActivatePath();

            path4_E.ActivatePath();
            path4_R.ActivatePath();
            path4_T.ActivatePath();

            int path4randomW = Random.Range(0, 2);
            if (path4randomW == 0)
            {
                path4_W.ActivatePath();
            }
            else
            {
                path4_W.DeActivatePath();
            }
        }

        // Depth 5 //////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (path4_Q.CheckOnPath() && path4_W.CheckOnPath() && path4_E.CheckOnPath() && path4_R.CheckOnPath() && path4_T.CheckOnPath())
        {
            // QWERT

            path5_A.ActivatePath();
            path5_B.ActivatePath();
            path5_C.ActivatePath();
            path5_D.ActivatePath();
        }
        else if (!path4_Q.CheckOnPath() && path4_W.CheckOnPath() && path4_E.CheckOnPath() && !path4_R.CheckOnPath() && !path4_T.CheckOnPath())
        {
            // WE

            path5_D.DeActivatePath();
            path5_B.ActivatePath();

            int path5random = Random.Range(0, 3);
            if (path5random == 0)
            {
                path5_A.ActivatePath();
                path5_C.DeActivatePath();
            }
            else if (path5random == 1)
            {
                path5_A.DeActivatePath();
                path5_C.ActivatePath();
            }
            else
            {
                path5_A.ActivatePath();
                path5_C.ActivatePath();
            }

        }
        else if (!path4_Q.CheckOnPath() && !path4_W.CheckOnPath() && path4_E.CheckOnPath() && path4_R.CheckOnPath() && !path4_T.CheckOnPath())
        {
            // ER

            path5_A.DeActivatePath();
            path5_C.ActivatePath();

            int path5random = Random.Range(0, 3);
            if (path5random == 0)
            {
                path5_B.ActivatePath();
                path5_D.DeActivatePath();
            }
            else if (path5random == 1)
            {
                path5_B.DeActivatePath();
                path5_D.ActivatePath();
            }
            else
            {
                path5_B.ActivatePath();
                path5_D.ActivatePath();
            }
        }
        else if (!path4_Q.CheckOnPath() && path4_W.CheckOnPath() && path4_E.CheckOnPath() && path4_R.CheckOnPath() && !path4_T.CheckOnPath())
        {
            // WER

            path5_B.ActivatePath();
            path5_C.ActivatePath();

            int path5randomA = Random.Range(0, 2);
            if (path5randomA == 0)
            {
                path5_A.ActivatePath();
            }
            else
            {
                path5_A.DeActivatePath();
            }

            int path5randomD = Random.Range(0, 2);
            if (path5randomD == 0)
            {
                path5_D.ActivatePath();
            }
            else
            {
                path5_D.DeActivatePath();
            }
        }
        else if (path4_Q.CheckOnPath() && path4_W.CheckOnPath() && path4_E.CheckOnPath() && !path4_R.CheckOnPath() && !path4_T.CheckOnPath())
        {
            // QWE

            path5_D.DeActivatePath();
            path5_A.ActivatePath();
            path5_B.ActivatePath();
            path5_C.ActivatePath();
        }
        else if (path4_Q.CheckOnPath() && path4_W.CheckOnPath() && path4_E.CheckOnPath() && path4_R.CheckOnPath() && !path4_T.CheckOnPath())
        {
            // QWER

            path5_A.ActivatePath();
            path5_B.ActivatePath();
            path5_C.ActivatePath();

            int path5randomD = Random.Range(0, 2);
            if (path5randomD == 0)
            {
                path5_D.ActivatePath();
            }
            else
            {
                path5_D.DeActivatePath();
            }

        }
        else if (!path4_Q.CheckOnPath() && !path4_W.CheckOnPath() && path4_E.CheckOnPath() && path4_R.CheckOnPath() && path4_T.CheckOnPath())
        {
            // ERT

            path5_A.DeActivatePath();
            path5_B.ActivatePath();
            path5_C.ActivatePath();
            path5_D.ActivatePath();
        }
        else if (!path4_Q.CheckOnPath() && path4_W.CheckOnPath() && path4_E.CheckOnPath() && path4_R.CheckOnPath() && path4_T.CheckOnPath())
        {
            // WERT

            path5_B.ActivatePath();
            path5_C.ActivatePath();
            path5_D.ActivatePath();

            int path5randomA = Random.Range(0, 2);
            if (path5randomA == 0)
            {
                path5_A.ActivatePath();
            }
            else
            {
                path5_A.DeActivatePath();
            }
        }

        // Depth 6 //////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (path5_A.CheckOnPath() && path5_B.CheckOnPath() && path5_C.CheckOnPath() && path5_D.CheckOnPath())
        {
            // ABCD

            path6_W.ActivatePath();
            path6_E.ActivatePath();
            path6_R.ActivatePath();

            int path6randomQ = Random.Range(0, 2);
            if (path6randomQ == 0)
            {
                path6_Q.ActivatePath();
            }
            else
            {
                path6_Q.DeActivatePath();
            }

            int path6randomT = Random.Range(0, 2);
            if (path6randomT == 0)
            {
                path6_T.ActivatePath();
            }
            else
            {
                path6_T.DeActivatePath();
            }
        }
        else if (!path5_A.CheckOnPath() && path5_B.CheckOnPath() && path5_C.CheckOnPath() && !path5_D.CheckOnPath())
        {
            // BC

            path6_Q.DeActivatePath();
            path6_W.ActivatePath();
            path6_E.ActivatePath();
            path6_R.ActivatePath();
            path6_T.DeActivatePath();
        }
        else if (path5_A.CheckOnPath() && path5_B.CheckOnPath() && !path5_C.CheckOnPath() && !path5_D.CheckOnPath())
        {
            // AB

            path6_Q.ActivatePath();
            path6_W.ActivatePath();
            path6_E.ActivatePath();
            path6_R.DeActivatePath();
            path6_T.DeActivatePath();
        }
        else if (!path5_A.CheckOnPath() && !path5_B.CheckOnPath() && path5_C.CheckOnPath() && path5_D.CheckOnPath())
        {
            // CD

            path6_Q.DeActivatePath();
            path6_W.DeActivatePath();
            path6_E.ActivatePath();
            path6_R.ActivatePath();
            path6_T.ActivatePath();
        }
        else if (path5_A.CheckOnPath() && path5_B.CheckOnPath() && path5_C.CheckOnPath() && !path5_D.CheckOnPath())
        {
            // ABC

            path6_T.DeActivatePath();

            path6_Q.ActivatePath();
            path6_W.ActivatePath();
            path6_E.ActivatePath();

            int path6randomR = Random.Range(0, 2);
            if (path6randomR == 0)
            {
                path6_R.ActivatePath();
            }
            else
            {
                path6_R.DeActivatePath();
            }
        }
        else if (!path5_A.CheckOnPath() && path5_B.CheckOnPath() && path5_C.CheckOnPath() && path5_D.CheckOnPath())
        {
            // BCD

            path6_Q.DeActivatePath();

            path6_E.ActivatePath();
            path6_R.ActivatePath();
            path6_T.ActivatePath();

            int path6randomW = Random.Range(0, 2);
            if (path6randomW == 0)
            {
                path6_W.ActivatePath();
            }
            else
            {
                path6_W.DeActivatePath();
            }
        }

        // Depth 7 //////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (path6_Q.CheckOnPath() && path6_W.CheckOnPath() && path6_E.CheckOnPath() && path6_R.CheckOnPath() && path6_T.CheckOnPath())
        {
            // QWERT

            path7_A.ActivatePath();
            path7_B.ActivatePath();
            path7_C.ActivatePath();
            path7_D.ActivatePath();
        }
        else if (!path6_Q.CheckOnPath() && path6_W.CheckOnPath() && path6_E.CheckOnPath() && !path6_R.CheckOnPath() && !path6_T.CheckOnPath())
        {
            // WE

            path7_D.DeActivatePath();
            path7_B.ActivatePath();

            int path7random = Random.Range(0, 3);
            if (path7random == 0)
            {
                path7_A.ActivatePath();
                path7_C.DeActivatePath();
            }
            else if (path7random == 1)
            {
                path7_A.DeActivatePath();
                path7_C.ActivatePath();
            }
            else
            {
                path7_A.ActivatePath();
                path7_C.ActivatePath();
            }

        }
        else if (!path6_Q.CheckOnPath() && !path6_W.CheckOnPath() && path6_E.CheckOnPath() && path6_R.CheckOnPath() && !path6_T.CheckOnPath())
        {
            // ER

            path7_A.DeActivatePath();
            path7_C.ActivatePath();

            int path7random = Random.Range(0, 3);
            if (path7random == 0)
            {
                path7_B.ActivatePath();
                path7_D.DeActivatePath();
            }
            else if (path7random == 1)
            {
                path7_B.DeActivatePath();
                path7_D.ActivatePath();
            }
            else
            {
                path7_B.ActivatePath();
                path7_D.ActivatePath();
            }
        }
        else if (!path6_Q.CheckOnPath() && path6_W.CheckOnPath() && path6_E.CheckOnPath() && path6_R.CheckOnPath() && !path6_T.CheckOnPath())
        {
            // WER

            path7_B.ActivatePath();
            path7_C.ActivatePath();

            int path7randomA = Random.Range(0, 2);
            if (path7randomA == 0)
            {
                path7_A.ActivatePath();
            }
            else
            {
                path7_A.DeActivatePath();
            }

            int path7randomD = Random.Range(0, 2);
            if (path7randomD == 0)
            {
                path7_D.ActivatePath();
            }
            else
            {
                path7_D.DeActivatePath();
            }
        }
        else if (path6_Q.CheckOnPath() && path6_W.CheckOnPath() && path6_E.CheckOnPath() && !path6_R.CheckOnPath() && !path6_T.CheckOnPath())
        {
            // QWE

            path7_D.DeActivatePath();
            path7_A.ActivatePath();
            path7_B.ActivatePath();
            path7_C.ActivatePath();
        }
        else if (path6_Q.CheckOnPath() && path6_W.CheckOnPath() && path6_E.CheckOnPath() && path6_R.CheckOnPath() && !path6_T.CheckOnPath())
        {
            // QWER

            path7_A.ActivatePath();
            path7_B.ActivatePath();
            path7_C.ActivatePath();

            int path7randomD = Random.Range(0, 2);
            if (path7randomD == 0)
            {
                path7_D.ActivatePath();
            }
            else
            {
                path7_D.DeActivatePath();
            }

        }
        else if (!path6_Q.CheckOnPath() && !path6_W.CheckOnPath() && path6_E.CheckOnPath() && path6_R.CheckOnPath() && path6_T.CheckOnPath())
        {
            // ERT

            path7_A.DeActivatePath();
            path7_B.ActivatePath();
            path7_C.ActivatePath();
            path7_D.ActivatePath();
        }
        else if (!path6_Q.CheckOnPath() && path6_W.CheckOnPath() && path6_E.CheckOnPath() && path6_R.CheckOnPath() && path6_T.CheckOnPath())
        {
            // WERT

            path7_B.ActivatePath();
            path7_C.ActivatePath();
            path7_D.ActivatePath();

            int path7randomA = Random.Range(0, 2);
            if (path7randomA == 0)
            {
                path7_A.ActivatePath();
            }
            else
            {
                path7_A.DeActivatePath();
            }
        }

        // Depth 8 //////////////////////////////////////////////////////////////////////////////////////////////////////////

        path8_W.ActivatePath();
        path8_E.ActivatePath();
        path8_R.ActivatePath();

        // Depth 9 //////////////////////////////////////////////////////////////////////////////////////////////////////////

        path9.ActivatePath();

        List<Path> activatedPaths = GetComponentsInChildren<Path>().Where(path => path.CheckOnPath()).ToList();

        foreach (Path path in activatedPaths)
        {
            int randomQuestType = Random.Range(1, 10);

            QuestType selectedQuestType = (QuestType)randomQuestType;

            path.AssignQuestType(selectedQuestType);
        }
    }
}
