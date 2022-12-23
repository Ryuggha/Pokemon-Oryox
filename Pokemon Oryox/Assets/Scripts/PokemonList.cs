using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonList : MonoBehaviour
{
    private Dictionary<int, PokemonObject> allMyPokemon;

    private void Awake()
    {
        allMyPokemon = new Dictionary<int, PokemonObject>();
    }

    public void addPokemon(PokemonObject o)
    {
        int index = -6;
        bool found = false;
        do
        {
            if (allMyPokemon.ContainsKey(index)) index++;
            else found = true;
        } while (!found);

        allMyPokemon.Add(index, o);
    }
}
