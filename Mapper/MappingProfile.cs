using AutoMapper;
using post_office_back.Dtos;
using post_office_back.Models;
using post_office_back.Models.Enums;

namespace post_office_back.Mapper
{

    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ShipmentCreationDto, Shipment>()
                .ForMember(dest => dest.DestinationAirport, opt => opt.MapFrom(src => Enum.Parse(typeof(Airport), src.DestinationAirport)));
            CreateMap<ParcelCreationDto, Parcel>();
        }
    }
}
