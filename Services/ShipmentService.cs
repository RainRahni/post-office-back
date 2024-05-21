using post_office_back.Models.Enums;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Core;
using System.Net;
using Azure;
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
            try
            {
                _validationService.ValidateShipementCreation(shipmentDto);
            } catch (ArgumentException ex) {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message)
                };
                return response;
            }
            Shipment shipment = _mapper.Map<Shipment>(shipmentDto);

            _dataContext.Shipments.Add(shipment);
            _dataContext.SaveChanges();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

    }
}
