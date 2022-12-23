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

}
