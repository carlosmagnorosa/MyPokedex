using Microsoft.Extensions.Configuration;
using MyPokedex.Core.PokeApi;
using System;

namespace MyPokedex.Api.Infrastructure
{
    internal class PokeApiSettings : IPokeApiSettings
    {

        private string _endpoint;
        private string _flavorTextLanguage;

        public PokeApiSettings(IConfiguration configuration)
        {
            _endpoint = configuration["External:Pokeapi:Endpoint"] ?? throw new ArgumentNullException(nameof(_endpoint));
            _flavorTextLanguage = configuration["External:Pokeapi:FlavorTextLanguage"] ?? throw new ArgumentNullException(nameof(_endpoint));
        }

        public string Endpoint { get => _endpoint; }

        public string FlavorTextLanguage { get => _flavorTextLanguage; }
    }
}