using Microsoft.VisualBasic;
using post_office_back.Models.Enums;
using post_office_back.Models;

namespace post_office_back.Dtos
{
    public class ShipmentCreationDto
    {
        public string ShipmentNumber { get; set; } = string.Empty;
        public string DestinationAirport { get; set; } = String.Empty;
        public string FlightNumber { get; set; } = string.Empty;
        public DateTime FlightDate { get; set; }
        public bool IsFinalized { get; set; } = false;
        public ShipmentCreationDto() { }
        public ShipmentCreationDto(string shipmentNumber, Airport destinationAirport, string flightNumber, DateTime flightDate)
        {
            ShipmentNumber = shipmentNumber;
            DestinationAirport = destinationAirport.ToString();
            FlightNumber = flightNumber;
            FlightDate = flightDate;
        }

      
    }

}
