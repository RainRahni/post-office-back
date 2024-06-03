using Microsoft.EntityFrameworkCore;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using post_office_back.Models.Enums;
using System.Text.RegularExpressions;

namespace post_office_back.Services
{
    public class ValidationService : IValidationService
    {
        private readonly DataContext _dataContext;
        public ValidationService(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public void ValidateBagCreation(string shipmentNumber, string bagNumber)
        {   
            bool isCorrectShipmentNumber = Regex.IsMatch(shipmentNumber, Constants.shipmentNumberPattern);

            bool isCorrectBagNumber = Regex.IsMatch(bagNumber, Constants.bagNumberPattern);

            bool isUniqueBagNumber = !_dataContext.Bags.Any(b  => b.BagNumber == bagNumber);

            bool isShipmentAvailable = _dataContext.Shipments.Any(s => s.ShipmentNumber.Equals(shipmentNumber) && !s.IsFinalized);

            if (!(isCorrectShipmentNumber && isCorrectBagNumber && isUniqueBagNumber && isShipmentAvailable))
            {
                throw new ArgumentException(Constants.invalidParametersMessage);
            }
        }

        public void ValidateParcelCreation(ParcelCreationDto parcelCreationDto)
        {
            bool isCorrectParcelNumber = Regex.IsMatch(parcelCreationDto.ParcelNumber, Constants.parcelNumberPattern);

            bool isCorrectBagNumber = Regex.IsMatch(parcelCreationDto.BagNumber, Constants.bagNumberPattern);

            bool isCorrectDestinationCounrty = Regex.IsMatch(parcelCreationDto.DestinationCountry, Constants.destinationCountryPattern);

            bool isCorrectRecipientNameLength = parcelCreationDto.RecipientName.Length <= Constants.recipientNameMaxLength;

            bool isBagPresent = _dataContext.Bags.Any(b => b.BagNumber.Equals(parcelCreationDto.BagNumber));

            bool isNotFinalizedShipment = _dataContext.Shipments.Any(s => s.Bags.Any(b => b.BagNumber.Equals(parcelCreationDto.BagNumber))
                && !s.IsFinalized);

            bool isCorrectBagType = _dataContext.Bags.Any(b => b.BagNumber.Equals(parcelCreationDto.BagNumber)
                && (b.Discriminator.Equals(BagType.BAG.ToString()) || b.Discriminator.Equals(BagType.PARCELBAG.ToString())));

            if (!(isCorrectParcelNumber && isCorrectBagNumber && isCorrectDestinationCounrty && isCorrectRecipientNameLength && isBagPresent 
                && isNotFinalizedShipment && isCorrectBagType))
            {
                throw new ArgumentException(Constants.invalidParametersMessage);
            }
        }

        public void ValidateShipementCreation(ShipmentCreationDto shipmentCreationDto)
        {
            bool isCorrectShipmentNumber = Regex.IsMatch(shipmentCreationDto.ShipmentNumber, Constants.shipmentNumberPattern);

            bool isUniqueShipmentNumber = !_dataContext.Shipments.Any(s => s.ShipmentNumber.Equals(shipmentCreationDto.ShipmentNumber));

            bool isCorrectEnumValue = Enum.IsDefined(typeof(Airport), shipmentCreationDto.DestinationAirport);

            bool isCorrectFlightNumber = Regex.IsMatch(shipmentCreationDto.FlightNumber, Constants.flightNumberPattern);

            bool isNotInPast = shipmentCreationDto.FlightDate > DateTime.Now;

            if (!(isCorrectShipmentNumber && isUniqueShipmentNumber && isCorrectEnumValue && isCorrectFlightNumber && isNotInPast))
            {
                throw new ArgumentException(Constants.invalidParametersMessage);
            };
        }

        public void ValidateShipmentFinalization(string shipmentNumber)
        {
            bool isCorrectShipmentNumber = Regex.IsMatch(shipmentNumber, Constants.shipmentNumberPattern);

            bool isNotInPast = _dataContext.Shipments.Any(s => s.ShipmentNumber.Equals(shipmentNumber) && s.FlightDate > DateTime.Now);

            bool isNotFinalized = _dataContext.Shipments.Any(s => s.ShipmentNumber.Equals(shipmentNumber) && !s.IsFinalized);

            if (isCorrectShipmentNumber && isNotInPast && isNotFinalized) {
                List<Bag> Bags = (List<Bag>)_dataContext.Shipments.Include(s => s.Bags).First(s => s.ShipmentNumber.Equals(shipmentNumber)).Bags;
                int bagsLength = Bags.Count();
                bool isEmpty = bagsLength == 0;
                for (int i = 0; i < bagsLength; i++)
                {
                    Bag currentBag = Bags.ElementAt(i);
                    if (currentBag is ParcelBag parcelBag)
                    {
                        _dataContext.Entry(parcelBag)
                            .Collection(pb => pb.Parcels)
                            .Load();

                        isEmpty = parcelBag.Parcels.Count() == 0;
                    }
                    if (isEmpty)
                    {
                        throw new ArgumentException(Constants.cannotFinalizeShipmentMessage);
                    }
                }
            }
            else
            {
                throw new ArgumentException(Constants.cannotFinalizeShipmentMessage);
            }
           

        }
        public void ValidateLetterAdding(LetterAddingDto letterAddingDto)
        {
            bool isCorrectBagNumber = Regex.IsMatch(letterAddingDto.BagNumber, Constants.bagNumberPattern);

            bool isCorrectShipmentNumber = Regex.IsMatch(letterAddingDto.ShipmentNumber, Constants.shipmentNumberPattern);

            if (!(isCorrectBagNumber && isCorrectShipmentNumber))
            {
                throw new ArgumentException(Constants.invalidParametersMessage);
            }
        }
    }
}
