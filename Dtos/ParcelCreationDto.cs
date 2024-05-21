namespace post_office_back.Dtos
{
    public class ParcelCreationDto
    {
        public String ParcelNumber { get; set; }
        public String RecipientName { get; set; }
        public String DestinationCountry { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public String BagNumber { get; set; }
    }
}
