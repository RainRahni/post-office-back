using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using post_office_back.Models.Enums;
using post_office_back.Services;
using Constants = post_office_back.Constants;
using post_office_back.Models.Builders;
namespace PostOfficeTests
{
    public class ShipmentServiceTests
    {
        private readonly Mock<IValidationService> _mockValidationService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IDataContext> _mockDataContext;
        private readonly ShipmentService _service;

        public ShipmentServiceTests()
        {
            _mockValidationService = new Mock<IValidationService>();
            _mockMapper = new Mock<IMapper>();
            _mockDataContext = new Mock<IDataContext>();

            _service = new ShipmentService(_mockDataContext.Object, _mockValidationService.Object, _mockMapper.Object);
        }
        [Fact]
        public void CreateShipment_WhenInputCorrect_SaveShipment()
        {
            DateTime flightDate = new DateTime(2025, 5, 20, 0, 0, 0);
            ShipmentCreationDto shipmentCreationDto = new ShipmentCreationDto("000-000000", Airport.TLL, "AA1001", flightDate);

            var mockSet = new Mock<DbSet<Shipment>>();
            var expectedShipment = new Shipment("000-000000", Airport.TLL, "AA1001", flightDate);


            _mockDataContext.Setup(m => m.Shipments).Returns(new Mock<DbSet<Shipment>>().Object);

            _mockValidationService.Setup(v => v.ValidateShipementCreation(shipmentCreationDto));
            _mockMapper.Setup(m => m.Map<Shipment>(shipmentCreationDto)).Returns(expectedShipment);
            mockSet.Setup(m => m.Add(It.IsAny<Shipment>())).Callback<Shipment>(s => expectedShipment = s);

            _service.CreateShipment(shipmentCreationDto);

            Assert.NotNull(expectedShipment);
            Assert.Equal("000-000000", expectedShipment.ShipmentNumber);

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Once);
        }
        [Fact]
        public void CreateShipment_WhenInputInCorrect_NoSave()
        {
            DateTime flightDate = new DateTime(2025, 5, 20, 0, 0, 0);
            ShipmentCreationDto shipmentCreationDto = new ShipmentCreationDto("000", Airport.TLL, "AA1001", flightDate);

            var mockSet = new Mock<DbSet<Shipment>>();


            _mockDataContext.Setup(m => m.Shipments).Returns(new Mock<DbSet<Shipment>>().Object);

            _mockValidationService.Setup(v => v.ValidateShipementCreation(shipmentCreationDto))
                .Throws(new ArgumentException(Constants.invalidParametersMessage)) ;

            var exception = Assert.Throws<ArgumentException>(() => _service.CreateShipment(shipmentCreationDto));

            Assert.Equal(Constants.invalidParametersMessage, exception.Message);

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Never);
        }
        [Fact]
        public void FinalizeShipment_WhenShipmentHasAtLeastOneLetterAndParcelBag_ThenFinalize()
        {
            var shipmentNumber = "000-000000";
            var letterBag = new Bag("LB123");
            letterBag.AddLetters(1);
            var parcel = new Parcel("PN123", "John Doe", "Estonia", 1.5m, 10.0m);
            var parcelBag = new Bag("PB123");
            parcelBag.Parcels.Add(parcel);
            var expectedShipment = new ShipmentBuilder()
                .AddShipmentNumber(shipmentNumber)
                .Build();
            expectedShipment.Bags.Add(letterBag);
            expectedShipment.Bags.Add(parcelBag);

            var data = new List<Shipment> { expectedShipment }.AsQueryable();

            var mockSet = new Mock<DbSet<Shipment>>();
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockDataContext.Setup(m => m.Shipments).Returns(mockSet.Object);
            _mockValidationService.Setup(v => v.ValidateShipmentFinalization(shipmentNumber));

            _service.FinalizeShipment(shipmentNumber);

            Assert.True(expectedShipment.IsFinalized);
            _mockDataContext.Verify(c => c.SaveChanges(), Times.Once);
        }


        [Fact]
        public void FinalizeShipment_WhenShipmentNrIncorrect_ThenNoFinalization()
        {
            var shipmentNumber = "Wrong";
            var letterBag = new Bag("LB123");
            letterBag.AddLetters(1);
            var parcel = new Parcel("PN123", "John Doe", "Estonia", 1.5m, 10.0m);
            var parcelBag = new Bag("PB123");
            parcelBag.Parcels.Add(parcel);
            var expectedShipment = new ShipmentBuilder()
                .AddShipmentNumber(shipmentNumber)
                .Build();
            expectedShipment.Bags.Add(letterBag);
            expectedShipment.Bags.Add(parcelBag);

            var data = new List<Shipment> { expectedShipment }.AsQueryable();

            var mockSet = new Mock<DbSet<Shipment>>();
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockDataContext.Setup(m => m.Shipments).Returns(mockSet.Object);
            _mockValidationService.Setup(v => v.ValidateShipmentFinalization(shipmentNumber))
                .Throws(new ArgumentException(Constants.invalidParametersMessage));

            var exception = Assert.Throws<ArgumentException>(() => _service.FinalizeShipment(shipmentNumber));

            Assert.Equal(Constants.invalidParametersMessage, exception.Message);

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Never);
        }
        [Fact]
        public void FinalizeShipment_WhenBagsEmpty_ThenNoFinalization()
        {
            var shipmentNumber = "000-000000";
            var expectedShipment = new ShipmentBuilder()
                .AddShipmentNumber(shipmentNumber)
                .Build();

            var data = new List<Shipment> { expectedShipment }.AsQueryable();

            var mockSet = new Mock<DbSet<Shipment>>();
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockDataContext.Setup(m => m.Shipments).Returns(mockSet.Object);
            _mockValidationService.Setup(v => v.ValidateShipmentFinalization(shipmentNumber))
                .Throws(new ArgumentException(Constants.invalidParametersMessage));

            var exception = Assert.Throws<ArgumentException>(() => _service.FinalizeShipment(shipmentNumber));

            Assert.Equal(Constants.invalidParametersMessage, exception.Message);

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}