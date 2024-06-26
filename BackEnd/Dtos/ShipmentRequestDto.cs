﻿using post_office_back.Models.Enums;
using post_office_back.Models;

namespace post_office_back.Dtos
{
    public class ShipmentRequestDto
    {
        public string ShipmentNumber { get; set; } = string.Empty;
        public string DestinationAirport { get; set; } = string.Empty;
        public string FlightNumber { get; set; } = string.Empty;
        public string FlightDate { get; set; } = string.Empty;
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
