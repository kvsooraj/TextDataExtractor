using TextDataExtractor.Models;

namespace TextDataExtractor.Services
{
    public interface IDataExtractor
    {
        ExtractionResult ExtractData(string text);

    }
}