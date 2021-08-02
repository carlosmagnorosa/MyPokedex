using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MyPokedex.Core.Exceptions;
using MyPokedex.Core.External.FunTranslations;
using MyPokedex.Infrastructure.FunTranslations.Model;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyPokedex.Infrastructure.FunTranslations
{
    public class FunTranslationService : IFunTranslationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly FunTranslationOptions _funTranslationOptions;

        public FunTranslationService(IHttpClientFactory httpClientFactory, IOptions<FunTranslationOptions> funTranslationOptions, IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _funTranslationOptions = funTranslationOptions?.Value ?? throw new ArgumentNullException(nameof(funTranslationOptions));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<string> TranslateEnglishToShakespeare(string englishText)
        {
            var cacheKey = $"shakespeare-{englishText}";
            string cacheEntry;

            if (!_memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                try
                {
                    FunTranslationOutput translationOutput = await Translate(englishText, _funTranslationOptions.ShakespeareEndpoint);
                    cacheEntry = translationOutput.Contents.TranslatedText;
                    _memoryCache.Set(cacheKey, cacheEntry);
                }
                catch (Exception)
                {
                    return englishText;
                }
            }

            return cacheEntry;
        }

        public async Task<string> TranslateEnglishToYoda(string englishText)
        {
            var cacheKey = $"shakespeare-{englishText}";
            string cacheEntry;

            if (!_memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                try
                {
                    FunTranslationOutput translationOutput = await Translate(englishText, _funTranslationOptions.YodaEndpoint);
                    cacheEntry = translationOutput.Contents.TranslatedText;
                    _memoryCache.Set(cacheKey, cacheEntry);
                }
                catch (Exception)
                {
                    return englishText;
                }
            }

            return cacheEntry;
        }


        private async Task<FunTranslationOutput> Translate(string englishText, string endpoint)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var input = new FunTranslationInput(englishText);
            using HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync<FunTranslationInput>(endpoint, input);
            switch (httpResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return await httpResponse.Content.ReadFromJsonAsync<FunTranslationOutput>();
                default:
                    throw new FunTranslationsException();
            }
        }
    }
}
