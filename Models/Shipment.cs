using Microsoft.VisualBasic;
using post_office_back.Models.Enums;

namespace post_office_back.Models
{
    public class Shipment(string shipmentNumber, Airport arrivalAirport, string flightNumber, DateAndTime flightDate, ICollection<Bag> bags)
    {
        public String ShipmentNumber { get; set; } = shipmentNumber;
        public Airport ArrivalAirport { get; set; } = arrivalAirport;
        public String FlightNumber { get; set; } = flightNumber;
        public DateAndTime FlightDate { get; set; } = flightDate;
        public ICollection<Bag> Bags { get; set; } = bags;
    }
}
