using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler : MonoBehaviour
{
    public static FileDataHandler instance;

    private PokemonList pokemonList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        pokemonList = GetComponent<PokemonList>();

        Load();
    }

    public void Load()
    {
        FileInfo[] fileInfoList = new DirectoryInfo(Application.persistentDataPath).GetFiles();

        foreach (FileInfo fileInfo in fileInfoList)
        {
            string fullPath = Path.Combine(Application.persistentDataPath, fileInfo.Name);
            PokemonSerializable loadedPokemon = null;
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedPokemon = JsonUtility.FromJson<PokemonSerializable>(dataToLoad);

                PokemonList.instance.allMyPokemon.Add(loadedPokemon.index, loadedPokemon.load());
            }
            catch (Exception e)
            {
                Debug.Log($"Error while loading {e}");
            }
        }
    }

    public void Save()
    {
        if (Directory.Exists(Application.persistentDataPath)) { Directory.Delete(Application.persistentDataPath, true); }
        Directory.CreateDirectory(Application.persistentDataPath);
        foreach (var i in pokemonList.allMyPokemon.Keys)
        {
            try
            {
                var serializableForm = new PokemonSerializable(pokemonList.allMyPokemon[i], i);
                string dataFileName = serializableForm.index.ToString() + ".json";

                string dataToStore = JsonUtility.ToJson(serializableForm, true);

                using (FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath, dataFileName), FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Error while saving {pokemonList.allMyPokemon[i].nickname}\n{e}");
            }
            
        }
    }
}
