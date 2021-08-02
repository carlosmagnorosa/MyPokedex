using System.Threading.Tasks;

namespace MyPokedex.Core.External.FunTranslations
{
    public interface IFunTranslationService
    {
        Task<string> TranslateEnglishToYoda(string englishText);
        Task<string> TranslateEnglishToShakespeare(string englishText);
    }
}
