using post_office_back.Dtos;
using post_office_back.Models.Enums;

namespace post_office_back.Services
{
    public interface IShipmentService
    {
        HttpResponseMessage CreateShipment(ShipmentCreationDto shipmentDto);
        HttpResponseMessage FinalizeShipment(string shipmentNumber);
    }
}
