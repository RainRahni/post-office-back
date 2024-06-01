using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using post_office_back;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using post_office_back.Services;

namespace PostOfficeTests
{
    public class ParcelServiceTest
    {
        private readonly Mock<IValidationService> _mockValidationService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IDataContext> _mockDataContext;
        private readonly ParcelService _service;

        public ParcelServiceTest()
        {
            _mockValidationService = new Mock<IValidationService>();
            _mockMapper = new Mock<IMapper>();
            _mockDataContext = new Mock<IDataContext>();

            _service = new ParcelService(_mockDataContext.Object, _mockValidationService.Object, _mockMapper.Object);
        }
        /*[Fact]
        public void CreateParcel_WhenInputCorrect_SaveParcel()
        {
            var parcelCreationDto = new ParcelCreationDto
            {
                ParcelNumber = "PN123",
                RecipientName = "John Doe",
                DestinationCountry = "Estonia",
                Weight = 1.5m,
                Price = 10.0m,
                BagNumber = "BN123"
            };
            var existingBag = new ParcelBag("BN123");

            var mockSet = new Mock<DbSet<Parcel>>();
            var mockBagSet = new Mock<DbSet<Bag>>();
            var data = new List<Bag> { existingBag }.AsQueryable();

            mockBagSet.As<IQueryable<Bag>>().Setup(m => m.Provider).Returns(data.Provider);
            mockBagSet.As<IQueryable<Bag>>().Setup(m => m.Expression).Returns(data.Expression);
            mockBagSet.As<IQueryable<Bag>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockBagSet.As<IQueryable<Bag>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockValidationService.Setup(v => v.ValidateParcelCreation(parcelCreationDto));
            _mockDataContext.Setup(m => m.Parcels).Returns(mockSet.Object);
            _mockDataContext.Setup(m => m.Bags).Returns(mockBagSet.Object);

            _service.CreateParcel(parcelCreationDto);

            Assert.NotEmpty(existingBag.Parcels); // Check if a Parcel was added
            var actualParcel = existingBag.Parcels.First();
            Assert.Equal(parcelCreationDto.ParcelNumber, actualParcel.ParcelNumber); // Check if the correct Parcel was added

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Once);

        }*/

        [Fact]
        public void CreateParcel_WhenInputIncorrect_SaveParcel()
        {
            var parcelCreationDto = new ParcelCreationDto
            {
                ParcelNumber = "PN123",
                RecipientName = "John Doe",
                DestinationCountry = "Estonia",
                Weight = 1.5m,
                Price = 10.0m,
                BagNumber = "BN123"
            };
            var expectedParcel = new Parcel("PN123", "John Doe", "Estonia", 1.5m, 10.0m);
            var existingBag = new ParcelBag("BN123");

            var mockSet = new Mock<DbSet<Parcel>>();
            var mockBagSet = new Mock<DbSet<Bag>>();
            var data = new List<Bag> { existingBag }.AsQueryable();

            mockBagSet.As<IQueryable<Bag>>().Setup(m => m.Provider).Returns(data.Provider);
            mockBagSet.As<IQueryable<Bag>>().Setup(m => m.Expression).Returns(data.Expression);
            mockBagSet.As<IQueryable<Bag>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockBagSet.As<IQueryable<Bag>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockValidationService.Setup(v => v.ValidateParcelCreation(parcelCreationDto))
                .Throws(new ArgumentException(Constants.invalidParametersMessage));

            var exception = Assert.Throws<ArgumentException>(() => _service.CreateParcel(parcelCreationDto));

            Assert.Equal(Constants.invalidParametersMessage, exception.Message);

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}
