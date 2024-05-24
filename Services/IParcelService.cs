using post_office_back.Dtos;

namespace post_office_back.Services
{
    public interface IParcelService
    {
        HttpResponseMessage CreateParcel(ParcelCreationDto parcelCreationDto);
    }
}
