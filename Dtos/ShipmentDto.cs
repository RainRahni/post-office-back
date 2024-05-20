using Microsoft.VisualBasic;
using post_office_back.Models.Enums;
using post_office_back.Models;

namespace post_office_back.Dtos
{
    public class ShipmentDto
    {
        public ShipmentDto() 
        {
        }
        public String ShipmentNumber { get; set; }
        public String DestinationAirport { get; set; }
        public String FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        //public ICollection<BagDto> Bags { get; set; }
        public bool IsFinalized { get; set; }
      
    }

}
