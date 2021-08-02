using System.Text.Json.Serialization;

namespace MyPokedex.Infrastructure.FunTranslations.Model
{
    public record Contents
    {
        [JsonPropertyName("translated")]
        public string TranslatedText { get; init; }
        [JsonPropertyName("translation")]
        public string TranslationUsed { get; init; }
    }
}