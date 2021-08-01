using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyPokedex.Infrastructure.PokeApi.Model
{

    public record PokemonSpecies
    {
        public string Name { get; set; }
       
        [JsonPropertyName("flavor_text_entries")]       
        public IEnumerable<FlavorTextEntry> FlavorTextEntries { get; set; }
       
        public Habitat Habitat { get; set; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }
    }
}
