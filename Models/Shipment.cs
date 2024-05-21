using Microsoft.VisualBasic;
using post_office_back.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace post_office_back.Models
{
    [Table("Shipments")]
    public class Shipment

    {
        [Key]
        public String ShipmentNumber { get; set; }
        public Airport DestinationAirport { get; set; }
        public String FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        public ICollection<Bag> Bags { get; set; }
        public bool IsFinalized { get; set; } = false;
        public Shipment(string shipmentNumber, Airport destinationAirport, string flightNumber, DateTime flightDate)
        {
            ShipmentNumber = shipmentNumber;
            DestinationAirport = destinationAirport;
            FlightNumber = flightNumber;
            FlightDate = flightDate;
            Bags = new List<Bag>();
        }

    }
}
