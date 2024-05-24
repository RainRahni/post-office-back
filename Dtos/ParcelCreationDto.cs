namespace post_office_back.Dtos
{
    public class ParcelCreationDto
    {
        public String ParcelNumber { get; set; } = string.Empty;
        public String RecipientName { get; set; } = string.Empty;       
        public String DestinationCountry { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public String BagNumber { get; set; } = string.Empty;
    }
}
