using post_office_back.Dtos;

namespace post_office_back.Services
{
    public interface IParcelService
    {
        void CreateParcel(ParcelCreationDto parcelCreationDto);
    }
}
