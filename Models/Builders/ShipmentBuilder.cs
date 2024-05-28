using post_office_back.Models.Enums;

namespace post_office_back.Models.Builders
{
    public class ShipmentBuilder
    {
        private Shipment _shipment = new Shipment();
        public ShipmentBuilder AddShipmentNumber(string shipmentNumber) 
        {
            _shipment.ShipmentNumber = shipmentNumber;
            return this;
        }
        public ShipmentBuilder AddDestinationAirport(Airport destinationAirport)
        {
            _shipment.DestinationAirport = destinationAirport;
            return this;
        }
        public ShipmentBuilder AddFlightNumber(string flightNumber)
        {
            _shipment.FlightNumber= flightNumber;
            return this;
        }
        public ShipmentBuilder AddFlightDate(DateTime flightDate)
        {
            _shipment.FlightDate = flightDate;
            return this;
        }
        public ShipmentBuilder AddIsFinalized(bool isFinalized)
        {
            _shipment.IsFinalized = isFinalized;
            return this;
        }
        public Shipment Build()
        {
            return _shipment;
        }
    }
}
