using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyPokedex.Infrastructure.PokeApi.Model
{

    public record PokemonSpecies
    {
        public string Name { get; init; }
       
        [JsonPropertyName("flavor_text_entries")]       
        public IEnumerable<FlavorTextEntry> FlavorTextEntries { get; init; }
       
        public Habitat Habitat { get; init; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; init; }
    }
}
