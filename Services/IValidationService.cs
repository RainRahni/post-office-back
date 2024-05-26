using post_office_back.Dtos;

namespace post_office_back.Services
{
    public interface IValidationService
    {
        void ValidateBagCreation(string shipmentNumber, string bagNumber);
        void ValidateParcelCreation(ParcelCreationDto parcelCreationDto);
        void ValidateShipementCreation(ShipmentCreationDto shipmentCreationDto);
        void ValidateShipmentFinalization(string shipmentNumber);
        void ValidateLetterAdding(LetterAddingDto letterAddingDto);
    }
}
