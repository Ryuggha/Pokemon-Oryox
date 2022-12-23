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
        if (desiredDepth > 0) {
            List<PokeApiPokemonJSON> pokemonList = recursivePokemonData(client, "https://pokeapi.co/api/v2/pokemon/", 0);
            createPokemonObjectData(pokemonList, client);
        }

        else
        {
            string pokeJson = client.DownloadString("https://pokeapi.co/api/v2/pokemon/lurantis-totem");
            Debug.Log(JsonConvert.DeserializeObject<PokeApiPokemonJSON>(pokeJson).species);
        }
        
    }

    public List<PokeApiPokemonJSON> recursivePokemonData(WebClient client, string nextCall, int depth)
    {
        List<PokeApiPokemonJSON> list = new List<PokeApiPokemonJSON>();

        string pokeList = client.DownloadString(nextCall);
        PokemonBulkData data = JsonConvert.DeserializeObject<PokemonBulkData>(pokeList);

        foreach (var pokemon in data.results)
        {
            try
            {
                string pokeJson = client.DownloadString(pokemon.url);
                list.Add(JsonConvert.DeserializeObject<PokeApiPokemonJSON>(pokeJson));
            }
            catch
            {
                Debug.Log($"Error: {pokemon.name}");
            }
        }

        if (data.next != null && depth < desiredDepth) list.AddRange(recursivePokemonData(client, data.next, depth + 1));
        //if (data.next != null) list.AddRange(recursivePokemonData(client, data.next, depth + 1));

        return list;
    }

    private void createPokemonObjectData(List<PokeApiPokemonJSON> pokemonList, WebClient client)
    {
        foreach (var pokemon in pokemonList)
        {
            PokemonApiData o = ScriptableObject.CreateInstance<PokemonApiData>();

            o.id = pokemon.id;
            o.speciesName = pokemon.name;
            o.type1 = PokemonTypeClass.parsePokemonType(pokemon.types[0].type.name);
            if (pokemon.types.Count > 1) o.type2 = PokemonTypeClass.parsePokemonType(pokemon.types[1].type.name);

            try 
            { 
                client.DownloadFile(pokemon.sprites.front_default, $"Assets/Resources/PokemonSprites/Sprites/{pokemon.name}.png");
                try { client.DownloadFile(pokemon.sprites.front_shiny, $"Assets/Resources/PokemonSprites/Shinies/{pokemon.name}.png"); }
                catch 
                {
                    client.DownloadFile(pokemon.sprites.front_default, $"Assets/Resources/PokemonSprites/Shinies/{pokemon.name}.png");
                    Debug.Log("Shiny Error: " + o.speciesName); 
                }
                AssetDatabase.Refresh();
                o.sprite = Resources.Load<Sprite>($"PokemonSprites/Sprites/{pokemon.name}");
                o.shiny = Resources.Load<Sprite>($"PokemonSprites/Shinies/{pokemon.name}");

                AssetDatabase.CreateAsset(o, $"Assets/Data/Pokemon/{pokemon.id} - {pokemon.name}.asset");
            }
            catch { Debug.Log("Sprite Error: " + o.speciesName); }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    

}
