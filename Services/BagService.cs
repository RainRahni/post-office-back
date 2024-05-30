using AutoMapper;
using Microsoft.EntityFrameworkCore;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using System.Net;

namespace post_office_back.Services
{
    public class BagService : IBagService
    {
        private readonly IDataContext _dataContext;
        private readonly IValidationService _validationService;

        public BagService(IDataContext dataContext, IValidationService validationService)
        {
            _dataContext = dataContext;
            _validationService = validationService;
        }
        public void CreateBag(BagCreationDto bagCreationDto)
        {
            String bagNumber = bagCreationDto.BagNumber;
            String shipmentNumber = bagCreationDto.ShipmentNumber;
            _validationService.ValidateBagCreation(shipmentNumber, bagNumber);
            Bag bag = new Bag(bagCreationDto.BagNumber);
            Shipment existingShipment = _dataContext.Shipments.Include(s => s.Bags).First(s => s.ShipmentNumber.Equals(shipmentNumber));

            bag.ShipmentNumber = shipmentNumber;
            bag.Shipment = existingShipment;
            existingShipment.Bags.Add(bag);
            _dataContext.SaveChanges();
        }
        public void AddLetters(LetterAddingDto letterAddingDto)
        {
            _validationService.ValidateLetterAdding(letterAddingDto);
            Bag currentBag = _dataContext.Bags.Find(letterAddingDto.BagNumber);
            if (currentBag is LetterBag letterBag)
            {
                letterBag.AddLetters(letterAddingDto.NumberOfLetters);
            }
            else if (currentBag is Bag)
            {
                LetterBag letterB = new LetterBag(currentBag.BagNumber);
                letterB.AddLetters(letterAddingDto.NumberOfLetters);
                Shipment shipment = _dataContext.Shipments.Include(s => s.Bags)
                    .First(s => s.ShipmentNumber.Equals(letterAddingDto.ShipmentNumber));
                shipment.Bags.Remove(currentBag);
                shipment.Bags.Add(letterB);
                _dataContext.Bags.Remove(currentBag);
                _dataContext.Bags.Add(letterB);

            }
            _dataContext.SaveChanges();
        }
    }
}
