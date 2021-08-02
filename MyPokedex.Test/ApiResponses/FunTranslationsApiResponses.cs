using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Test.ApiResponses
{
    public static class FunTranslationsApiResponses
    {
        public static readonly string YodaDescription = "Lost a planet,  master obiwan has.";
        public static readonly string YodaTranslation = @"{
              ""success"": {
                ""total"": 1
              },
              ""contents"": {
                ""translated"": ""Lost a planet,  master obiwan has."",
                ""text"": ""Master Obiwan has lost a planet"",
                ""translation"": ""yoda""
              }
            }";

        public static readonly string ShakespeareTranslation = @"{
              ""success"": {
                ""total"": 1
              },
              ""contents"": {
                ""translated"": ""Thou has lost a planet."",
                ""text"": ""Master obiwan hath did lose a planet.."",
                ""translation"": ""yoda""
              }
            }";
    }
}
