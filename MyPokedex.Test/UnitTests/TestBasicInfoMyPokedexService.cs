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

    public class TestMyPokedexService
    {
        [Fact]
        public async Task ValidPokemonName_ReturnItsInfo()
        {
            var pokeApiService = new Mock<IPokeApiService>();
            pokeApiService.Setup(s => s.GetPokemonInfo(It.IsAny<string>())).Returns(Task.FromResult(new Core.Model.BasicPokemon()
            {
                Description = "Lorem ipsum",
                Habitat = "cave",
                IsLegendary = true,
                Name = "mewtwo"
            }));

            var funtranslationService = new Mock<IFunTranslationService>();
            IOptions<TranslatedPokemonOptions> translatedPokemonOptions = Options.Create<TranslatedPokemonOptions>(
                                                                            new TranslatedPokemonOptions() {});

            MyPokedexService sut = new MyPokedexService(pokeApiService.Object, funtranslationService.Object, translatedPokemonOptions);

            var result = await sut.GetPokedexBasicInfo("mewtwo");

            Assert.Equal("mewtwo", result.Name);
            Assert.Equal("Lorem ipsum", result.Description);
            Assert.Equal("cave", result.Habitat);
            Assert.True(result.IsLegendary);
        }
    }
}
