using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "move", menuName = "ScriptableObjects/MoveData")]
public class MoveData : ScriptableObject
{
    public string moveName;
    public bool isPasive;
    public pokemonType type;
    [SerializeField, @TextAreaAttribute(5, 15)] public string description;
    public string coste;
    public string range;
    [SerializeField, @TextAreaAttribute(3, 15)] public string notes;
}
