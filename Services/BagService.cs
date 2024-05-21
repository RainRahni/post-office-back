using AutoMapper;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using System.Net;

namespace post_office_back.Services
{
    public class BagService : IBagService
    {
        private readonly DataContext _dataContext;
        private readonly ValidationService _validationService;

        public BagService(DataContext dataContext, ValidationService validationService)
        {
            _dataContext = dataContext;
            _validationService = validationService;
        }
        public HttpResponseMessage CreateBag(BagCreationDto bagCreationDto)
        {
            String bagNumber = bagCreationDto.BagNumber;
            String shipmentNumber = bagCreationDto.ShipmentNumber;
            try
            {
                _validationService.validateBagCreation(shipmentNumber, bagNumber);
            }
            catch (ArgumentException ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message)
                };
                return response;
            }
            Bag bag = new Bag(bagCreationDto.BagNumber);
            Shipment existingShipment = _dataContext.Shipments.First(s => s.ShipmentNumber.Equals(bagCreationDto.ShipmentNumber));

           existingShipment.Bags.Add(bag);

            _dataContext.Bags.Add(bag);
            _dataContext.SaveChanges();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
