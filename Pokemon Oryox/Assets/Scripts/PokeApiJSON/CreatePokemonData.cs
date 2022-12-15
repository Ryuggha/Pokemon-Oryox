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
            o.speciesName = pokemon.name;
            

            AssetDatabase.CreateAsset(o, $"Assets/Data/Pokemon/{pokemon.name}.asset");
            
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static pokemonType parsePokemonType(string rawType)
    {
        switch (rawType)
        {
            case "steel": return pokemonType.steel;
            case "water": return pokemonType.steel;
            case "bug": return pokemonType.steel;
            case "dragon": return pokemonType.steel;
            case "electric": return pokemonType.steel;
            case "ghost": return pokemonType.steel;
            case "fire": return pokemonType.steel;
            case "fairy": return pokemonType.steel;
            case "ice": return pokemonType.steel;
            case "fight": return pokemonType.steel;
            case "normal": return pokemonType.steel;
            case "grass": return pokemonType.steel;
            case "psychic": return pokemonType.steel;
            case "rock": return pokemonType.steel;
            case "dark": return pokemonType.steel;
            case "ground": return pokemonType.steel;
            case "poison": return pokemonType.steel;
            case "flying": return pokemonType.steel;
            case "light": return pokemonType.steel;
            case "demon": return pokemonType.steel;
            case "shadow": return pokemonType.steel;
            default: return pokemonType.none;
        }
    }

}
