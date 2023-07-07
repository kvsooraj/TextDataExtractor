using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TextDataExtractor.Models;
using TextDataExtractor.Services;

namespace TextDataExtractor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextDataController : ControllerBase
    {
        private readonly IDataExtractor _textDataExtractor;

        public TextDataController(IDataExtractor textDataExtractor)
        {
            _textDataExtractor = textDataExtractor;
        }

        [HttpPost]
        public ActionResult<ExtractionResult> ExtractData([FromBody] string text)
        {
            var extractionResult = _textDataExtractor.ExtractData(text);

            if (extractionResult == null)
            {
                return BadRequest("Failed to extract data from the input text.");
            }
            else if (extractionResult.IsExtractionError)
            {
                return BadRequest(extractionResult.Message);
            }

            return Ok(extractionResult);
        }
    }




}
