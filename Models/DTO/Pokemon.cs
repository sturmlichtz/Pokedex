using System;
using System.Collections.Generic;
using System.Web;

namespace Pokedex.Models.DTO
{
    public class Pokemon
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }

    public class PokemonList
    {
        public List<Pokemon> Results { get; set; }
    }


    public class PokemonData
    {
        public string Name { get; set; }
        public Sprites Sprites { get; set; }
        public int Weight { get; set; }
    }

    public class Sprites
    {
        public string front_default { get; set; }
    }
}