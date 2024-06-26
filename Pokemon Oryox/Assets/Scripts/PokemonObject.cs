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
    public List<MoveData> equipedMoves;
    public List<MoveData> pasives;

    public NatureData nature;
    public string natureProperty1;
    public string natureProperty2;

    public ConstitutionData constitution;

    public bool isShiny;
    public bool isTribal;

    public string mov;
    public string initiative;
    public string turnCounter;
    public string luck;
    public string attack;
    public string defense;
    public string spAttack;
    public string spDefense;
    public string linkUses;
    public string respect;
    public string affect;
    public string admiration;
    public string syncrony;
    public string discipline;
    public string hp;
    public string ep;
    public string pp;
    public string trainerPasives;
    public string abilityPasives;

    public PokemonObject(PokemonApiData pokemonApiData)
    {
        moves = new List<MoveData>();
        equipedMoves = new List<MoveData>();
        pasives = new List<MoveData>();
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

    public string getName()
    {
        if (nickname == "") return PokemonObject.toUpperString(pokemonData.speciesName);
        return nickname;
    }
}
