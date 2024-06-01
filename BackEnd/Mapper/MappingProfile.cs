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
            CreateMap<Shipment, ShipmentRequestDto>()
                .ForMember(dest => dest.DestinationAirport, opt => opt.MapFrom(src => src.DestinationAirport.ToString()))
                .ForMember(date => date.FlightDate, opt => opt.MapFrom(src => src.FlightDate.ToString()))
                .ForMember(ship => ship.Bags, opt => opt.Ignore());
        }
    }
}
