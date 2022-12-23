public enum pokemonType
{
    none,
    steel,
    water,
    bug,
    dragon,
    electric,
    ghost,
    fire,
    fairy,
    ice,
    fight,
    normal,
    grass,
    psychic,
    rock,
    dark,
    ground,
    poison,
    flying,
    light,
    demon,
    shadow,
    cosmic
}

public static class PokemonTypeClass{
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

    public static string translateEnglishSpanish(string input)
    {
        switch (input)
        {
            case "steel": return "acero";
            case "water": return "agua";
            case "bug": return "bicho";
            case "dragon": return "dragón";
            case "electric": return "eléctrico";
            case "ghost": return "fantasma";
            case "fire": return "fuego";
            case "fairy": return "hada";
            case "ice": return "hielo";
            case "fight": return "lucha";
            case "normal": return "normal";
            case "grass": return "planta";
            case "psychic": return "psíquico";
            case "rock": return "roca";
            case "dark": return "siniestro";
            case "ground": return "tierra";
            case "poison": return "veneno";
            case "flying": return "volador";
            case "light": return "luz";
            case "demon": return "demonio";
            case "shadow": return "oscuridad";
            case "cosmic": return "cósmico";
            default: return "";
        }
    }

    public static string translateSpanishEnglish(string input)
    {
        switch (input)
        {
            case "acero": return "steel";
            case "agua": return "water";
            case "bicho": return "bug";
            case "dragón": return "dragon";
            case "eléctrico": return "electric";
            case "fantasma": return "ghost";
            case "fuego": return "fire";
            case "hada": return "fairy";
            case "hielo": return "ice";
            case "lucha": return "fight";
            case "normal": return "normal";
            case "planta": return "grass";
            case "psíquico": return "psychic";
            case "roca": return "rock";
            case "siniestro": return "dark";
            case "tierra": return "ground";
            case "veneno": return "poison";
            case "volador": return "flying";
            case "luz": return "light";
            case "demonio": return "demon";
            case "oscuridad": return "shadow";
            case "cósmico": return "cosmic";
            default: return "";
        }
    }

    public static int getTypeIndex(pokemonType type)
    {
        switch (type)
        {
            case pokemonType.steel: return 0;
            case pokemonType.water: return 1;
            case pokemonType.bug: return 2;
            case pokemonType.dragon: return 3;
            case pokemonType.electric: return 4;
            case pokemonType.ghost: return 5;
            case pokemonType.fire: return 6;
            case pokemonType.fairy: return 7;
            case pokemonType.ice: return 8;
            case pokemonType.fight: return 9;
            case pokemonType.normal: return 10;
            case pokemonType.grass: return 11;
            case pokemonType.psychic: return 12;
            case pokemonType.rock: return 13;
            case pokemonType.dark: return 14;
            case pokemonType.ground: return 15;
            case pokemonType.poison: return 16;
            case pokemonType.flying: return 17;
            case pokemonType.light: return 18;
            case pokemonType.demon: return 19;
            case pokemonType.shadow: return 20;
            case pokemonType.cosmic: return 21;
            default: return -1;
        }
    }

    public static pokemonType getTypeByIndex(int index)
    {
        switch (index)
        {
            case 0: return pokemonType.steel;
            case 1: return pokemonType.water;
            case 2: return pokemonType.bug;
            case 3: return pokemonType.dragon;
            case 4: return pokemonType.electric;
            case 5: return pokemonType.ghost;
            case 6: return pokemonType.fire;
            case 7: return pokemonType.fairy;
            case 8: return pokemonType.ice;
            case 9: return pokemonType.fight;
            case 10: return pokemonType.normal;
            case 11: return pokemonType.grass;
            case 12: return pokemonType.psychic;
            case 13: return pokemonType.rock;
            case 14: return pokemonType.dark;
            case 15: return pokemonType.ground;
            case 16: return pokemonType.poison;
            case 17: return pokemonType.flying;
            case 18: return pokemonType.light;
            case 19: return pokemonType.demon;
            case 20: return pokemonType.shadow;
            case 21: return pokemonType.cosmic;
            case -1:
            default: return pokemonType.none;
        }
    }
}