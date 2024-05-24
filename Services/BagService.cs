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
                _validationService.ValidateBagCreation(shipmentNumber, bagNumber);
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
            Console.WriteLine(existingShipment.Bags.Count());

            existingShipment.Bags.Add(bag);
            Console.WriteLine(existingShipment.Bags.Count());

            _dataContext.Bags.Add(bag);
            _dataContext.SaveChanges();
            Console.WriteLine(_dataContext.Shipments.First(s => s.ShipmentNumber.Equals(bagCreationDto.ShipmentNumber)).Bags.Count());
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        public HttpResponseMessage AddLetters(LetterAddingDto letterAddingDto)
        {
            try
            {
                _validationService.ValidateLetterAdding(letterAddingDto);
            }
            catch (ArgumentException ex)
            {

                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message)
                };
                return response;
            }
            Bag currentBag = _dataContext.Bags.First(b => b.BagNumber.Equals(letterAddingDto.BagNumber));
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
            return new HttpResponseMessage(HttpStatusCode.OK);


        }
    }
}
