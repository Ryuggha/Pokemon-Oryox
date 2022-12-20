using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonObject
{
    public PokemonApiData pokemonData;
    public string nickname;

    public pokemonType type1;
    public pokemonType type2;

    public List<MoveData> moves;
    public List<MoveData> pasives;

    public NatureData nature;
    public string natureProperty1;
    public string natureProperty2;

    public ConstitutionData constitution;

    public bool isShiny;
    public bool isTribal;

    public PokemonObject(PokemonApiData pokemonApiData)
    {
        this.pokemonData = pokemonApiData;
    }

    public static string toUpperString(string str)
    {
        if (str.Length == 0)
            return "";
        if (str.Length == 1)
            return char.ToUpper(str[0]) + "";
        else
            return char.ToUpper(str[0]) + str.Substring(1);
    }
}
