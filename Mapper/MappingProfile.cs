using AutoMapper;
using post_office_back.Dtos;
using post_office_back.Models;

namespace post_office_back.Mapper
{

    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ShipmentDto, Shipment>();
        }
    }
}
