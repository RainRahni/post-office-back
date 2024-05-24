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
        public HttpResponseMessage CreateShipment(ShipmentCreationDto shipmentCreationDto)
        {
            try
            {
                _validationService.ValidateShipementCreation(shipmentCreationDto);
            } 
            catch (ArgumentException ex) 
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message)
                };
                return response;
            }
            Shipment shipment = _mapper.Map<Shipment>(shipmentCreationDto);

            _dataContext.Shipments.Add(shipment);
            _dataContext.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage FinalizeShipment(string shipmentNumber)
        {
            try
            {
                _validationService.ValidateShipmentFinalization(shipmentNumber);
            } 
            catch (ArgumentException ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message)
                };
                return response;
            }
            Shipment shipment = _dataContext.Shipments.First(s => s.ShipmentNumber.Equals(shipmentNumber));
            shipment.IsFinalized = true;
            _dataContext.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
