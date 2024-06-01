using post_office_back.Dtos;
using post_office_back.Models;
using post_office_back.Models.Enums;

namespace post_office_back.Services
{
    public interface IShipmentService
    {
        void CreateShipment(ShipmentCreationDto shipmentDto);
        void FinalizeShipment(string shipmentNumber);
        List<ShipmentRequestDto> ReadAllShipments();  
    }
}
