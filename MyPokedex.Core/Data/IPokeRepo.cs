using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Core.Data
{
    public interface IPokeRepo
    {
        Task Add(int uid, string pokemon);
        Task<List<string>> Get(int uid);
    }
}
