using MyPokedex.Core;
using MyPokedex.Test.UnitTests.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyPokedex.Test.UnitTests
{

    public class MyPokedexService_GetBasicInfo
    {
        [Fact]
        public async Task ValidPokemonName_ReturnItsInfo()
        {
            MyPokedexService sut = new MyPokedexService(new FakePokeApiService());

            Assert.Equal("mewtwo", (await sut.GetPokedexBasicInfo("mewtwo")).Name);
        }
    }
}
