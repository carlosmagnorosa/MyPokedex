﻿using System.Text.Json.Serialization;

namespace MyPokedex.Infrastructure.PokeApi
{
    public record FlavorTextEntry
    {
        [JsonPropertyName("flavor_text")]       
        public string FlavorText { get; init; }
        
        public FlavorTextLanguage Language { get; init; }
    }
}