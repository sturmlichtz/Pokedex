using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokedex.Models;
using Pokedex.Models.DTO;

namespace Pokedex.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 20)
    {
        var offset = (page - 1) * pageSize;
        var httpClient = new HttpClient();
        var apiUrl = $"https://pokeapi.co/api/v2/pokemon/?offset={offset}&limit={pageSize}";
        var response = await httpClient.GetStringAsync(apiUrl);

        if (response != null)
        {
            var pokemonList = JsonConvert.DeserializeObject<PokemonList>(response);
            foreach (var pokemon in pokemonList.Results)
            {
                var pokemonResponse = await httpClient.GetStringAsync(pokemon.Url);
                if (!string.IsNullOrEmpty(pokemonResponse))
                {
                    var pokemonData = JsonConvert.DeserializeObject<PokemonData>(pokemonResponse);
                    pokemon.ImageUrl = pokemonData.Sprites.front_default;
                }
            }
            return View(pokemonList.Results);
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
