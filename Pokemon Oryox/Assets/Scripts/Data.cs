using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data instance { get; private set; }

    public List<PokemonApiData> pokeData;
    public List<NatureData> natureData;
    public List<ConstitutionData> constitutionData;
    public List<MoveData> moveRawData;

    public Dictionary<int, List<MoveData>> activeMoveData;
    public Dictionary<int, List<MoveData>> pasiveData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (activeMoveData == null)
        {
            activeMoveData = new Dictionary<int, List<MoveData>>();
            pasiveData = new Dictionary<int, List<MoveData>>();

            foreach (var move in moveRawData)
            {
                if (move.isPasive)
                {
                    if (!pasiveData.ContainsKey(PokemonTypeClass.getTypeIndex(move.type))) pasiveData[PokemonTypeClass.getTypeIndex(move.type)] = new List<MoveData>();
                    pasiveData[PokemonTypeClass.getTypeIndex(move.type)].Add(move);
                }
                else
                {
                    if (!activeMoveData.ContainsKey(PokemonTypeClass.getTypeIndex(move.type))) activeMoveData[PokemonTypeClass.getTypeIndex(move.type)] = new List<MoveData>();
                    activeMoveData[PokemonTypeClass.getTypeIndex(move.type)].Add(move);
                }
            }
        }
    }

}
