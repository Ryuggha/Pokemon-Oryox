using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using PokeApiJSON;
using UnityEditor;

public class CreatePokemonData : MonoBehaviour
{

    private void Start()
    {
        createPokemonData();
    }

    public void createPokemonData()
    {
        WebClient client = new WebClient();
        string pokeJson = client.DownloadString("https://pokeapi.co/api/v2/pokemon/");
        var data = JsonConvert.DeserializeObject<PokemonBulkData>(pokeJson);
        createPokemonObjectData(data.results);
    }

    private void createPokemonObjectData(List<Result> results)
    {
        foreach (var pokemon in results)
        {
            PokemonApiData o = ScriptableObject.CreateInstance<PokemonApiData>();
            o.name = pokemon.name;
            

            AssetDatabase.CreateAsset(o, $"Assets/Data/Pokemon/{pokemon.name}.asset");
            
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static pokemonType parsePokemonType(string rawType)
    {
        switch (rawType)
        {
            default: return pokemonType.none;
        }
    }

}
