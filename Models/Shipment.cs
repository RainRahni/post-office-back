using Microsoft.VisualBasic;
using post_office_back.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace post_office_back.Models
{
    [Table("Shipments")]
    public class Shipment
    {
        public Shipment(string shipmentNumber, Airport destinationAirport, string flightNumber, DateAndTime flightDate)
        {
            ShipmentNumber = shipmentNumber;
            DestinationAirport = destinationAirport;
            FlightNumber = flightNumber;
            FlightDate = flightDate;
        }
        public String ShipmentNumber { get; set; }
        public Airport DestinationAirport { get; set; }
        public String FlightNumber { get; set; }
        public DateAndTime FlightDate { get; set; }
        public ICollection<Bag> Bags { get; set; }
        public bool IsFinalized { get; set; } = false;
    }
}
