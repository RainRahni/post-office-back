namespace post_office_back.Dtos
{
    public class BagCreationDto
    {
        public String ShipmentNumber { get; set; } = string.Empty;
        public String BagNumber { get; set; } = string.Empty;
        public BagCreationDto(string shipmentNumber, string bagNumber)
        {
            ShipmentNumber = shipmentNumber;
            BagNumber = bagNumber;
        }
    }
}
