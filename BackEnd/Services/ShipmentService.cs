using post_office_back.Models.Enums;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public List<ShipmentRequestDto> ReadAllShipments() 
        {
            List<Shipment> shipments = _dataContext.Shipments.Include(s => s.Bags).ToList(); //.ElementAt(0).Bags.Count();
            List<ShipmentRequestDto> requestedShipments = new List<ShipmentRequestDto>();
            foreach (var shipment in shipments)
            {
                List<Bag> bags = shipment.Bags.ToList();
                ShipmentRequestDto reqShipment = _mapper.Map<ShipmentRequestDto>(shipment);
                List<BagDto> bagDtos = CreateBagDtos(bags);
                reqShipment.Bags = bagDtos;
                requestedShipments.Add(reqShipment);
            }
            return requestedShipments;
        }
        private List<BagDto> CreateBagDtos(List<Bag> bags)
        {
            List<BagDto> bagDtos = new List<BagDto>();
            foreach (var bag in bags)
            {
                string bagNumber = bag.BagNumber;
                int itemCount = 0;
                string bagType = BagType.BAG.ToString();
                decimal bagPrice = 0;
                if (bag is LetterBag letterBag)
                {
                    itemCount = letterBag.CountOfLetters;
                    bagType = BagType.LETTERBAG.ToString();
                    bagPrice = letterBag.Price * itemCount;
                }
                else if (bag is ParcelBag parcelBag)
                {
                    _dataContext.Entry(parcelBag)
                        .Collection(pb => pb.Parcels)
                        .Load();
                    itemCount = parcelBag.Parcels.Count();

                    bagType = BagType.PARCELBAG.ToString();
                    bagPrice = parcelBag.Parcels.Sum(p => p.Price);
                }
                BagDto bagDto = new BagDto(bagNumber, itemCount, bagType, bagPrice);
                bagDtos.Add(bagDto);
            }
            return bagDtos;
        }
    }
}
