namespace TextDataExtractor.Models
{
    public class ExtractedData
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalExcludingTax { get; set; }

    }

}
