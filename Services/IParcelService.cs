using post_office_back.Dtos;

namespace post_office_back.Services
{
    public class IParcelService
    {
        HttpResponseMessage CreateParcel(ParcelCreationDto parcelCreationDto);
    }
}
