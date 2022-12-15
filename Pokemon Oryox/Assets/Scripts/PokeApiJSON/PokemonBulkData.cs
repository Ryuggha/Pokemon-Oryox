using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokeApiJSON
{
    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class PokemonBulkData
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<Result> results { get; set; }
    }

}
