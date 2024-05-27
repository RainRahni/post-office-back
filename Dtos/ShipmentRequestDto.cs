using post_office_back.Models.Enums;
using post_office_back.Models;

namespace post_office_back.Dtos
{
    public class ShipmentRequestDto
    {
        public String ShipmentNumber { get; set; }
        public String DestinationAirport { get; set; }
        public String FlightNumber { get; set; }
        public String FlightDate { get; set; }
        public ICollection<BagDto> Bags { get; set; } = new List<BagDto>();
        public bool IsFinalized { get; set; } = false;
        public ShipmentRequestDto() { }
        public ShipmentRequestDto(string shipmentNumber, string destinationAirport, string flightNumber, string flightDate)
        {
            ShipmentNumber = shipmentNumber;
            DestinationAirport = destinationAirport;
            FlightNumber = flightNumber;
            FlightDate = flightDate;
        }
    }
}
