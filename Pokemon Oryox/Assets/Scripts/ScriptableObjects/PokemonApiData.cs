using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PokeApiData", menuName = "ScriptableObjects/PokeApiData", order = 1)]
public class PokemonApiData : ScriptableObject
{
    public int id;
    public string speciesName;
    public pokemonType type1;
    public pokemonType type2;

    public Sprite sprite;
    public Sprite shiny;
}
