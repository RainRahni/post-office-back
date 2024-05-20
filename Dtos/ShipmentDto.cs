using Microsoft.VisualBasic;
using post_office_back.Models.Enums;
using post_office_back.Models;

namespace post_office_back.Dtos
{
    public class ShipmentDto
    {
        public ShipmentDto() { }
        public ShipmentDto(string shipmentNumber, Airport destinationAirport, string flightNumber, DateTime flightDate)
        {
            ShipmentNumber = shipmentNumber;
            DestinationAirport = destinationAirport.ToString();
            FlightNumber = flightNumber;
            FlightDate = flightDate;
            IsFinalized = false;
        }
        public String ShipmentNumber { get; set; }
        public String DestinationAirport { get; set; }
        public String FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        //public ICollection<BagDto> Bags { get; set; }
        public bool IsFinalized { get; set; }
      
    }

}
