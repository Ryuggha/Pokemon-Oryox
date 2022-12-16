using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using PokeApiJSON;
using UnityEditor;
using System.IO;

public class CreatePokemonData : MonoBehaviour
{
    [SerializeField] private int desiredDepth = 7;

    private void Start()
    {
        createPokemonData();
    }

    public void createPokemonData()
    {
        WebClient client = new WebClient();
        List<PokeApiPokemonJSON> pokemonList = recursivePokemonData(client, "https://pokeapi.co/api/v2/pokemon/", 0);
        createPokemonObjectData(pokemonList, client);
    }

    public List<PokeApiPokemonJSON> recursivePokemonData(WebClient client, string nextCall, int depth)
    {
        List<PokeApiPokemonJSON> list = new List<PokeApiPokemonJSON>();

        string pokeList = client.DownloadString(nextCall);
        PokemonBulkData data = JsonConvert.DeserializeObject<PokemonBulkData>(pokeList);

        foreach (var pokemon in data.results)
        {
            string pokeJson = client.DownloadString(pokemon.url);
            list.Add(JsonConvert.DeserializeObject<PokeApiPokemonJSON>(pokeJson));
        }

        if (data.next != null && depth < desiredDepth) list.AddRange(recursivePokemonData(client, data.next, depth + 1));

        return list;
    }

    private void createPokemonObjectData(List<PokeApiPokemonJSON> pokemonList, WebClient client)
    {
        foreach (var pokemon in pokemonList)
        {
            PokemonApiData o = ScriptableObject.CreateInstance<PokemonApiData>();

            o.id = pokemon.id;
            o.speciesName = pokemon.name;
            o.type1 = parsePokemonType(pokemon.types[0].type.name);
            if (pokemon.types.Count > 1) o.type2 = parsePokemonType(pokemon.types[1].type.name);

            client.DownloadFile(pokemon.sprites.front_default, $"Assets/Resources/PokemonSprites/Sprites/{pokemon.name}.png");
            client.DownloadFile(pokemon.sprites.front_shiny, $"Assets/Resources/PokemonSprites/Shinies/{pokemon.name}.png");
            AssetDatabase.Refresh();
            o.sprite = Resources.Load<Sprite>($"PokemonSprites/Sprites/{pokemon.name}");
            o.shiny = Resources.Load<Sprite>($"PokemonSprites/Shinies/{pokemon.name}");

            AssetDatabase.CreateAsset(o, $"Assets/Data/Pokemon/{pokemon.id} - {pokemon.name}.asset");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static pokemonType parsePokemonType(string rawType)
    {
        switch (rawType)
        {
            case "steel": return pokemonType.steel;
            case "water": return pokemonType.water;
            case "bug": return pokemonType.bug;
            case "dragon": return pokemonType.dragon;
            case "electric": return pokemonType.electric;
            case "ghost": return pokemonType.ghost;
            case "fire": return pokemonType.fire;
            case "fairy": return pokemonType.fairy;
            case "ice": return pokemonType.ice;
            case "fight": return pokemonType.fight;
            case "normal": return pokemonType.normal;
            case "grass": return pokemonType.grass;
            case "psychic": return pokemonType.psychic;
            case "rock": return pokemonType.rock;
            case "dark": return pokemonType.dark;
            case "ground": return pokemonType.ground;
            case "poison": return pokemonType.poison;
            case "flying": return pokemonType.flying;
            case "light": return pokemonType.light;
            case "demon": return pokemonType.demon;
            case "shadow": return pokemonType.shadow;
            case "cosmic": return pokemonType.cosmic;
            default: return pokemonType.none;
        }
    }

}
