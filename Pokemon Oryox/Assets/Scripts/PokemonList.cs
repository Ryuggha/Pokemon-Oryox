using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonList : MonoBehaviour
{
    public static PokemonList instance;

    public Dictionary<int, PokemonObject> allMyPokemon { get; private set; }

    [SerializeField] GameObject statsScreen;

    private void Awake()
    {
        if (instance == null) instance = this;
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

        FileDataHandler.instance.Save();
    }

    public void activateStatsScreen()
    {
        statsScreen.SetActive(true);
    }
}
