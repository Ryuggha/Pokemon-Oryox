using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nature", menuName = "ScriptableObjects/NatureData")]
public class NatureData : ScriptableObject
{
    public string natureName;
    public string[] possiblePositive;
    public string[] possibleNegative;
}
