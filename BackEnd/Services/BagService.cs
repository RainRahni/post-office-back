using AutoMapper;
using Microsoft.EntityFrameworkCore;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using post_office_back.Models.Enums;

namespace post_office_back.Services
{
    public class BagService : IBagService
    {
        private readonly IDataContext _dataContext;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;

        public BagService(IDataContext dataContext, IValidationService validationService, IMapper mapper)
        {
            _dataContext = dataContext;
            _validationService = validationService;
            _mapper = mapper;
        }
        public void CreateBag(BagCreationDto bagCreationDto)
        {
            string bagNumber = bagCreationDto.BagNumber;
            string shipmentNumber = bagCreationDto.ShipmentNumber;
            _validationService.ValidateBagCreation(shipmentNumber, bagNumber);
            Shipment existingShipment = _dataContext.Shipments.Include(s => s.Bags).First(s => s.ShipmentNumber.Equals(shipmentNumber));

            Bag bag = _mapper.Map<Bag>(bagCreationDto);
            existingShipment.Bags.Add(bag);
            _dataContext.SaveChanges();
        }
        public void AddLetters(LetterAddingDto letterAddingDto)
        {
            _validationService.ValidateLetterAdding(letterAddingDto);
            Bag currentBag = _dataContext.Bags.Find(letterAddingDto.BagNumber);
            if (currentBag.BagType.Equals(BagType.LETTERBAG))
            {
                currentBag.AddLetters(letterAddingDto.NumberOfLetters);
            }
            else 
            {
                Bag letterB = _mapper.Map<Bag>(letterAddingDto);
                letterB.BagType = BagType.LETTERBAG;
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
