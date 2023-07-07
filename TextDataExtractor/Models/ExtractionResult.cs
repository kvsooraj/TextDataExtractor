using TextDataExtractor.Controllers;

namespace TextDataExtractor.Models
{
    public class ExtractionResult
    {

        public ExtractedData Data { get; set; }
        public bool IsExtractionError { get; set; }
        public string Message { get; set; }

    }
}
