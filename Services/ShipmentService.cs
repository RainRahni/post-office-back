using post_office_back.Models.Enums;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
namespace post_office_back.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly DataContext _dataContext;
        private readonly ValidationService _validationService;
        private readonly IMapper _mapper;

        public ShipmentService(DataContext dataContext, ValidationService validationService, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _validationService = validationService;
        }
        public HttpResponseMessage CreateShipment(ShipmentDto shipmentDto)
        {
            if (_validationService.validateShipement(shipmentDto) == false)
            {
                Console.WriteLine("adas");
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }

            Console.WriteLine("adasasdasdasdas");
            Shipment shipment = _mapper.Map<Shipment>(shipmentDto);

            _dataContext.Shipments.Add(shipment);
            _dataContext.SaveChanges();

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

    }
}
