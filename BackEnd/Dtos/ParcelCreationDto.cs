namespace post_office_back.Dtos
{
    public class ParcelCreationDto
    {
        public string ParcelNumber { get; set; } = string.Empty;
        public string RecipientName { get; set; } = string.Empty;       
        public string DestinationCountry { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string BagNumber { get; set; } = string.Empty;
    }
}
