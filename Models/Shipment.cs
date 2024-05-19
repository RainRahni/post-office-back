using Microsoft.VisualBasic;

namespace post_office_back.Models
{
    public class Shipment(string shipmentNumber, Airport departureAirport, string flightNumber, DateAndTime flightDate, ICollection<Bag> bags)
    {
        public String ShipmentNumber { get; set; } = shipmentNumber;
        public Airport DepartureAirport { get; set; } = departureAirport;
        public String FlightNumber { get; set; } = flightNumber;
        public DateAndTime FlightDate { get; set; } = flightDate;
        public ICollection<Bag> Bags { get; set; } = bags;
    }
}
