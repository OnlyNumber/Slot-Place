using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildSO")]
public class BuildSO : ScriptableObject
{
    public Sprite Icon;

    public string Name;

    public string Description;

    public float Cost;

    public float UpgradeCost;
}
