using System.Collections.Generic;
using System;

[Serializable]
public class PokemonSerializable
{
    public int index;

    public int speciesId;
    public string nickname;
    public pokemonType type1;
    public pokemonType type2;
    public List<string> moves;
    public List<string> equipedMoves;
    public List<string> pasives;
    public string natureData;
    public string natureProperty1;
    public string natureProperty2;
    public string constitution;

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

    public PokemonSerializable(PokemonObject o, int index)
    {
        this.index = index;
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

        this.equipedMoves = new List<string>();
        foreach (var move in o.equipedMoves)
        {
            this.equipedMoves.Add(move.moveName);
        }

        this.pasives = new List<string>();
        foreach (var pasive in o.pasives)
        {
            this.pasives.Add(pasive.moveName);
        }

        mov = o.mov;
        initiative = o.initiative;
        turnCounter = o.turnCounter;
        luck = o.luck;
        attack = o.attack;
        defense = o.defense;
        spAttack = o.spAttack;
        spDefense = o.spDefense;
        linkUses = o.linkUses;
        respect = o.respect;
        affect = o.affect;
        admiration = o.admiration;
        syncrony = o.syncrony;
        discipline = o.discipline;
        hp = o.hp;
        ep = o.ep;
        pp = o.pp;
        trainerPasives = o.trainerPasives;
        abilityPasives = o.abilityPasives;
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

        r.equipedMoves = new List<MoveData>();
        foreach (var move in this.equipedMoves)
        {
            r.equipedMoves.Add(moveData.Find(x => x.moveName == move));
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

        r.mov = mov;
        r.initiative = initiative;
        r.turnCounter = turnCounter;
        r.luck = luck;
        r.attack = attack;
        r.defense = defense;
        r.spAttack = spAttack;
        r.spDefense = spDefense;
        r.linkUses = linkUses;
        r.respect = respect;
        r.affect = affect;
        r.admiration = admiration;
        r.syncrony = syncrony;
        r.discipline = discipline;
        r.hp = hp;
        r.ep = ep;
        r.pp = pp;
        r.trainerPasives = trainerPasives;
        r.abilityPasives = abilityPasives;

        return r;
    }
}
