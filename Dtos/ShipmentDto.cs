using Microsoft.VisualBasic;
using post_office_back.Models.Enums;
using post_office_back.Models;

namespace post_office_back.Dtos
{
    public class ShipmentDto(string shipmentNumber, Airport destinationAirport, string flightNumber, DateAndTime flightDate)
    {
        public String ShipmentNumber { get; set; } = shipmentNumber;
        public Airport DestinationAirport { get; set; } = destinationAirport;
        public String FlightNumber { get; set; } = flightNumber;
        public DateAndTime FlightDate { get; set; } = flightDate;
        public ICollection<BagDto> Bags { get; set; }
        public bool IsFinalized { get; set; } = false;
    }
}
