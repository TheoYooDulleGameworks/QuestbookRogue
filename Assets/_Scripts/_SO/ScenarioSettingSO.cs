using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioSettingSO", menuName = "Scriptable Objects/MetaProgressions/ScenarioSettingSO")]
public class ScenarioSettingSO : ScriptableObject
{
    [Header("Profile")]
    public string scenarioName;
}