namespace post_office_back.Dtos
{
    public class BagCreationDto
    {
        public string ShipmentNumber { get; set; } = string.Empty;
        public string BagNumber { get; set; } = string.Empty;
        public BagCreationDto(string shipmentNumber, string bagNumber)
        {
            ShipmentNumber = shipmentNumber;
            BagNumber = bagNumber;
        }
    }
}
