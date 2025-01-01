using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioSettingSO", menuName = "Scriptable Objects/MetaProgressions/ScenarioSettingSO")]
public class ScenarioSettingSO : ScriptableObject
{
    [Header("Profile")]
    public string scenarioName;

    [Header("Path Setting")]
    public List<PathSet> pathSets;
}

[System.Serializable]
public class PathSet
{
    [Header("EVEN")]
    public bool nodeA;
    public bool nodeB;
    public bool nodeC;
    public bool nodeD;
    public bool nodeE;
    public bool nodeF;
    public bool nodeG;

    [Header("ODD")]
    public bool nodeU;
    public bool nodeV;
    public bool nodeW;
    public bool nodeX;
    public bool nodeY;
    public bool nodeZ;
}