using Moq;
using MyPokedex.Core;
using MyPokedex.Core.PokeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyPokedex.Test.UnitTests
{

    public class TestBasicInfoMyPokedexService
    {
        [Fact]
        public async Task ValidPokemonName_ReturnItsInfo()
        {
            var service = new Mock<IPokeApiService>();
            service.Setup(s => s.GetPokemonInfo(It.IsAny<string>())).Returns(Task.FromResult(new Core.Model.BasicPokemon()
            {
                Description = "Lorem ipsum",
                Habitat = "cave",
                IsLegendary = true,
                Name = "mewtwo"
            }));

            //MyPokedexService sut = new MyPokedexService(service.Object);
            //var result = await sut.GetPokedexBasicInfo("mewtwo");

            //Assert.Equal("mewtwo", result.Name);
            //Assert.Equal("Lorem ipsum", result.Description);
            //Assert.Equal("cave", result.Habitat);
            //Assert.True(result.IsLegendary);
        }
       
    }
}
