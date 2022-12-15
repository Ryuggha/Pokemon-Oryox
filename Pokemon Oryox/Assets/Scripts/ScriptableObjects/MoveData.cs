using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "move", menuName = "ScriptableObjects/MoveData")]
public class MoveData : ScriptableObject
{
    public string moveName;
    public bool isPasive;
    public pokemonType type;
    public string description;
    public string coste;
    public string range;
    public string notes;
}
