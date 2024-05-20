using Microsoft.Extensions.FileSystemGlobbing.Internal;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models.Enums;
using System.Text.RegularExpressions;

namespace post_office_back.Services
{
    public class ValidationService
    {
        private readonly DataContext _dataContext;
        public ValidationService(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        internal bool validateShipement(ShipmentDto shipmentDto)
        {
            bool isCorrectShipmentNumber = Regex.IsMatch(shipmentDto.ShipmentNumber, Constants.shipmentNumberPattern);
            bool isUniqueShipmentNumber = _dataContext.Shipments.Any(e => e.ShipmentNumber != shipmentDto.ShipmentNumber);
            bool isCorrectEnumValue = Enum.IsDefined(typeof(Airport), shipmentDto.DestinationAirport);
            bool isCorrectFlightNumber = Regex.IsMatch(shipmentDto.FlightNumber, Constants.flightNumberPattern);
            bool isNotInPast = shipmentDto.FlightDate < DateTime.Now;

            return isCorrectShipmentNumber && isUniqueShipmentNumber && isCorrectEnumValue && isCorrectFlightNumber && isNotInPast;
        }
    }
}
