using System.Collections.Generic;
using System;

[Serializable]
public class PokemonSerializable
{
    public int speciesId;
    public string nickname;
    public pokemonType type1;
    public pokemonType type2;
    public List<string> moves;
    public List<string> pasives;
    public string natureData;
    public string natureProperty1;
    public string natureProperty2;
    public string constitution;

    public bool isShiny;
    public bool isTribal;

    public PokemonSerializable(PokemonObject o)
    {
        this.speciesId = o.pokemonData.id;
        this.nickname = o.nickname;
        this.type1 = o.type1;
        this.type2 = o.type2;
        this.natureData = o.nature.natureName;
        this.natureProperty1 = o.natureProperty1;
        this.natureProperty2 = o.natureProperty2;
        this.constitution = o.constitution.constitutionName;
        this.isShiny = o.isShiny;
        this.isTribal = o.isTribal;

        this.moves = new List<string>();
        foreach (var move in o.moves)
        {
            this.moves.Add(move.moveName);
        }

        this.pasives = new List<string>();
        foreach (var pasive in o.pasives)
        {
            this.moves.Add(pasive.moveName);
        }
    }

    public PokemonObject load()
    {
        var pokemonData = Data.instance.pokeData;

        PokemonObject r = new PokemonObject(pokemonData.Find(x => x.id == this.speciesId));

        r.nickname = this.nickname;

        r.type1 = this.type1;
        r.type2 = this.type2;

        var moveData = Data.instance.moveRawData;

        r.moves = new List<MoveData>();       
        foreach (var move in this.moves)
        {
            r.moves.Add(moveData.Find(x => x.moveName == move));
        }

        r.pasives = new List<MoveData>();
        foreach (var pasive in this.pasives)
        {
            r.pasives.Add(moveData.Find(x => x.moveName == pasive));
        }

        var natureData = Data.instance.natureData;
        r.nature = natureData.Find(x => x.natureName == this.natureData);

        r.natureProperty1 = this.natureProperty1;
        r.natureProperty2 = this.natureProperty2;

        var constitutionData = Data.instance.constitutionData;
        r.constitution = constitutionData.Find(x => x.constitutionName == this.constitution);

        r.isShiny = this.isShiny;
        r.isTribal = this.isTribal;

        return r;
    }
}
