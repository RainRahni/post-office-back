using post_office_back.Models.Enums;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Core;
using System.Net;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace post_office_back.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IDataContext _dataContext;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;

        public ShipmentService(IDataContext dataContext, IValidationService validationService, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _validationService = validationService;
        }
        public void CreateShipment(ShipmentCreationDto shipmentCreationDto)
        {
            _validationService.ValidateShipementCreation(shipmentCreationDto);
            Shipment shipment = _mapper.Map<Shipment>(shipmentCreationDto);
            _dataContext.Shipments.Add(shipment);
            _dataContext.SaveChanges();
        }

        public void FinalizeShipment(string shipmentNumber)
        {
            _validationService.ValidateShipmentFinalization(shipmentNumber);
            Shipment shipment = _dataContext.Shipments.Include(s => s.Bags).First(s => s.ShipmentNumber.Equals(shipmentNumber));
            shipment.IsFinalized = true;
            _dataContext.SaveChanges();
        }
        public int ReadAllShipments() 
        {
            return _dataContext.Shipments.Include(s => s.Bags).ToList().ElementAt(0).Bags.Count();
        }
    }
}
