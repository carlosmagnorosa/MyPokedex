using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Core.PokeApi
{
    public interface IPokeApiSettings
    {
        string Endpoint { get;}
        string FlavorTextLanguage { get; }
    }
}
