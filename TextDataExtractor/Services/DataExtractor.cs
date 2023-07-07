using System.Linq;
using System.Text.RegularExpressions;
using TextDataExtractor.Models;

namespace TextDataExtractor.Services
{
    public class DataExtractor : IDataExtractor
    {

        private const decimal TaxRate = 0.1m; // 10% tax rate

        public ExtractionResult ExtractData(string text)
        {
            var extractionResult = new ExtractionResult();

            // Extract marked up fields
            var markedUpFields = ExtractMarkedUpFields(text);

            if (!markedUpFields.Any())
            {
                return null;
            }

            // Extract and calculate data 
            if (markedUpFields.Where(x => x.Key == "total").Count() == 0)
            {
                extractionResult.IsExtractionError = true;
                extractionResult.Message = "Total value is missing";
                return extractionResult;
            }

            var costCentre = markedUpFields.Where(x => x.Key == "cost_centre").Select(y => y.Value).FirstOrDefault() ?? "UNKNOWN";
            var totalValue = markedUpFields.Where(x => x.Key == "total").Select(y => y.Value).FirstOrDefault();

            if (!decimal.TryParse(totalValue, out decimal total))
            {
                extractionResult.IsExtractionError = true;
                extractionResult.Message = "Total value is missing";
                return extractionResult;
            }

            var tax = total * TaxRate;
            var totalExcludingTax = total - tax;
            extractionResult.Data = new ExtractedData()
            {
                CostCentre = costCentre,
                Total = total,
                Tax = tax,
                TotalExcludingTax = totalExcludingTax
            };

     
            return extractionResult;
        }

        private List<KeyValuePair<string, string>> ExtractMarkedUpFields(string text)
        {
            var markedUpFields = new List<KeyValuePair<string, string>>();
            var regex = new Regex("<([^>]+)>([^<]+)</\\1>");
            var matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                var fieldName = match.Groups[1].Value;
                var fieldValue = match.Groups[2].Value;

                markedUpFields.Add(new KeyValuePair<string, string>(fieldName, fieldValue));
            }

            return markedUpFields;
        }



    }
}
