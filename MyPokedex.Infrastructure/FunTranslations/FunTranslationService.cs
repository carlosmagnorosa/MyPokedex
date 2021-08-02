using Microsoft.Extensions.Options;
using MyPokedex.Core.Exceptions;
using MyPokedex.Core.External.FunTranslations;
using MyPokedex.Infrastructure.FunTranslations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Infrastructure.FunTranslations
{
    public class FunTranslationService : IFunTranslationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly FunTranslationOptions _funTranslationOptions;

        public FunTranslationService(IHttpClientFactory httpClientFactory, IOptions<FunTranslationOptions> funTranslationOptions)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _funTranslationOptions = funTranslationOptions?.Value ?? throw new ArgumentNullException(nameof(funTranslationOptions));
        }

        public async Task<string> TranslateEnglishToShakespeare(string englishText)
        {
            FunTranslationOutput translationOutput = await Translate(englishText, _funTranslationOptions.ShakespeareEndpoint);
            return translationOutput.Contents.TranslatedText;
        }

        public async Task<string> TranslateEnglishToYoda(string englishText)
        {
            FunTranslationOutput translationOutput = await Translate(englishText, _funTranslationOptions.YodaEndpoint);
            return translationOutput.Contents.TranslatedText;
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
