using AutoMapper;
using Microsoft.EntityFrameworkCore;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using post_office_back.Models.Enums;

namespace post_office_back.Services
{
    public class ParcelService : IParcelService
    {
        private readonly IDataContext _dataContext;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;

        public ParcelService(IDataContext dataContext, IValidationService validationService, IMapper mapper)
        {
            _dataContext = dataContext;
            _validationService = validationService;
            _mapper = mapper;
        }
        public void CreateParcel(ParcelCreationDto parcelCreationDto)
        {
            _validationService.ValidateParcelCreation(parcelCreationDto);
            Bag bag = _dataContext.Bags.First(b => b.BagNumber.Equals(parcelCreationDto.BagNumber));
            Parcel parcel = _mapper.Map<Parcel>(parcelCreationDto);
            if (bag.BagType.Equals(BagType.PARCELBAG))
            {
                bag.Parcels.Add(parcel);
            } 
            else
            {
                Bag parcelBag = new Bag(parcelCreationDto.BagNumber);
                parcelBag.BagType = BagType.PARCELBAG;
                string bagNumber = bag.BagNumber;
                Shipment shipment = _dataContext.Shipments.Include(s => s.Bags).First(s => s.Bags.Any(b => b.BagNumber.Equals(bagNumber)));
                shipment.Bags.Remove(bag);
                shipment.Bags.Add(parcelBag);
                _dataContext.Bags.Remove(bag);
                _dataContext.Bags.Add(parcelBag);

            }

            _dataContext.SaveChanges();
        }
    }
}
