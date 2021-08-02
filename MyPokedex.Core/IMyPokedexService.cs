using MyPokedex.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Core
{
    public interface IMyPokedexService
    {
        Task<BasicPokemon> GetPokedexBasicInfo(string name);
        Task<BasicPokemon> GetPokedexTranslatedInfo(string name);
        Task<BasicPokemon> GetPokedexTranslatedInfo(BasicPokemon pokemon);
    }
}
