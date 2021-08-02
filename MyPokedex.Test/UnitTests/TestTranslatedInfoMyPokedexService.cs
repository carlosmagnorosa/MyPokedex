using Microsoft.Extensions.Options;
using Moq;
using MyPokedex.Core;
using MyPokedex.Core.Config;
using MyPokedex.Core.External.FunTranslations;
using MyPokedex.Core.PokeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyPokedex.Test.UnitTests
{

    public class TestTranslatedInfoMyPokedexService
    {
        [Theory]
        [InlineData("yoda desc", true, "lake")]
        [InlineData("yoda desc", true, "cave")]
        [InlineData("yoda desc", false, "cave")]
        [InlineData("shakespeare desc", false, "lake")]
        public async Task DifferentPokemonTypes_UseCorrectTranslation(string expected, bool isLegendary, string habitatName)
        {
            // Arrange
            var pokemon = new Core.Model.BasicPokemon()
            {
                Description = "Lorem ipsum",
                Habitat = habitatName,
                IsLegendary = isLegendary,
                Name = "mewtwo"
            };
            var pokeApiService = new Mock<IPokeApiService>();
            pokeApiService.Setup(s => s.GetPokemonInfo(It.IsAny<string>())).Returns(Task.FromResult(pokemon));

            var funtranslationService = new Mock<IFunTranslationService>();
            funtranslationService.Setup(s =>  s.TranslateEnglishToShakespeare(It.IsAny<string>())).Returns(Task.FromResult("shakespeare desc"));
            funtranslationService.Setup(s => s.TranslateEnglishToYoda(It.IsAny<string>())).Returns(Task.FromResult("yoda desc"));

            IOptions<TranslatedPokemonOptions> translatedPokemonOptions = Options.Create<TranslatedPokemonOptions>(
                                                                            new TranslatedPokemonOptions() { HabitatNameRule = "cave" });
            MyPokedexService sut = new MyPokedexService(pokeApiService.Object, funtranslationService.Object, translatedPokemonOptions);

            // Act
            var result = await sut.GetPokedexTranslatedInfo(string.Empty);

            // Assert
            Assert.Equal(expected, result.Description);
        }
       
    }
}
