using Microsoft.EntityFrameworkCore;
using Moq;
using post_office_back.Data;
using post_office_back.Dtos;
using post_office_back.Models;
using post_office_back.Services;
using post_office_back.Models.Builders;
using post_office_back;
using AutoMapper;

namespace PostOfficeTests
{
    public class BagServiceTests
    {
        private readonly Mock<IValidationService> _mockValidationService;
        private readonly Mock<IDataContext> _mockDataContext;
        private readonly BagService _service;
        private readonly Mock<IMapper> _mockMapper;


        public BagServiceTests()
        {
            _mockValidationService = new Mock<IValidationService>();
            _mockDataContext = new Mock<IDataContext>();
            _mockMapper = new Mock<IMapper>();


            _service = new BagService(_mockDataContext.Object, _mockValidationService.Object, _mockMapper.Object);
        }
        [Fact]
        public void CreateBag_WhenInputCorrect_SaveBag()
        {
            var bagCreationDto = new BagCreationDto("SN123", "BN123");
            var expectedBag = new Bag("BN123");
            var existingShipment = new ShipmentBuilder().AddShipmentNumber("SN123").Build();

            var mockSet = new Mock<DbSet<Bag>>();
            var mockShipmentSet = new Mock<DbSet<Shipment>>();
            var data = new List<Shipment> { existingShipment }.AsQueryable();

            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockValidationService.Setup(v => v.ValidateBagCreation(bagCreationDto.ShipmentNumber, bagCreationDto.BagNumber));
            _mockDataContext.Setup(m => m.Bags).Returns(mockSet.Object);
            _mockDataContext.Setup(m => m.Shipments).Returns(mockShipmentSet.Object);
            _mockMapper.Setup(m => m.Map<Bag>(bagCreationDto)).Returns(expectedBag);


            _service.CreateBag(bagCreationDto);

            var actualBagNumber = bagCreationDto.BagNumber;
            Assert.NotNull(expectedBag);

            Assert.Equal(bagCreationDto.BagNumber, expectedBag.BagNumber);

            Assert.Contains(existingShipment.Bags, bag => bag.BagNumber == expectedBag.BagNumber);

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Once);
        }
        [Fact]
        public void CreateBag_WhenInputInCorrect_NoSave()
        {
            var bagCreationDto = new BagCreationDto("SN123", "BN123");
            var expectedBag = new Bag("BN123");
            var existingShipment = new ShipmentBuilder().AddShipmentNumber("SN123").Build();

            var mockSet = new Mock<DbSet<Bag>>();
            var mockShipmentSet = new Mock<DbSet<Shipment>>();

            _mockValidationService.Setup(v => v.ValidateBagCreation(bagCreationDto.ShipmentNumber, bagCreationDto.BagNumber))
                .Throws(new ArgumentException(Constants.invalidParametersMessage));

            var exception = Assert.Throws<ArgumentException>(() => _service.CreateBag(bagCreationDto));

            Assert.Equal(Constants.invalidParametersMessage, exception.Message);

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Never);
        }
        [Fact]
        public void AddLetters_WhenBasicBag_CreateLetterBag()
        {
            var letterAddingDto = new LetterAddingDto("SN123", "BN123", 2);
            var expectedBag = new Bag("BN123");
            expectedBag.ShipmentNumber = "SN123";
            var existingShipment = new ShipmentBuilder().AddShipmentNumber("SN123").Build();
            var updatedBag = new Bag("BN123");
            updatedBag.ShipmentNumber = "ŚN123";
            var updatedShipment = new ShipmentBuilder().AddShipmentNumber("SN123").Build();
            updatedShipment.Bags.Add(updatedBag);

            var mockSet = new Mock<DbSet<Bag>>();
            var mockShipmentSet = new Mock<DbSet<Shipment>>();
            var data = new List<Shipment> { existingShipment }.AsQueryable();

            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
 

            _mockValidationService.Setup(v => v.ValidateLetterAdding(letterAddingDto));
            _mockDataContext.Setup(m => m.Bags).Returns(mockSet.Object);
            _mockDataContext.Setup(m => m.Shipments).Returns(mockShipmentSet.Object);
            _mockMapper.Setup(m => m.Map<Bag>(letterAddingDto)).Returns(expectedBag);

            mockSet.Setup(m => m.Find(It.IsAny<string>())).Returns(updatedBag);

            _service.AddLetters(letterAddingDto);

            var actualBagNumber = letterAddingDto.BagNumber;
            Assert.NotNull(updatedBag);

            Assert.Equal(updatedBag.BagNumber, actualBagNumber);

            Assert.IsType<Bag>(updatedBag);

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Once);
        }
        [Fact]
        public void AddLetters_WhenLetterbagPresent_AddsLetters()
        {
            
            var letterAddingDto = new LetterAddingDto("SN123", "BN123", 2);
            var existingBag = new Mock<Bag>("BN123");
            existingBag.Setup(b => b.AddLetters(It.IsAny<int>()));
            var existingShipment = new ShipmentBuilder().AddShipmentNumber("SN123").Build();

            var mockSet = new Mock<DbSet<Bag>>();
            var mockShipmentSet = new Mock<DbSet<Shipment>>();
            var data = new List<Shipment> { existingShipment }.AsQueryable();

            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockValidationService.Setup(v => v.ValidateLetterAdding(letterAddingDto));
            _mockDataContext.Setup(m => m.Bags).Returns(mockSet.Object);
            _mockDataContext.Setup(m => m.Shipments).Returns(mockShipmentSet.Object);
            _mockMapper.Setup(m => m.Map<Bag>(letterAddingDto)).Returns(existingBag.Object);

            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns(existingBag.Object);
            _service.AddLetters(letterAddingDto);

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Once);
            existingBag.Verify(b => b.AddLetters(letterAddingDto.NumberOfLetters), Times.Once);
        }



        [Fact]
        public void AddLetters_WhenInputIncorrect_NoLettersAdded()
        {
            var letterAddingDto = new LetterAddingDto("SN123", "BN123", 2);
            var existingBag = new Mock<Bag>("BN123");
            existingBag.Setup(b => b.AddLetters(It.IsAny<int>()));
            var existingShipment = new ShipmentBuilder().AddShipmentNumber("SN123").Build();

            var mockSet = new Mock<DbSet<Bag>>();
            var mockShipmentSet = new Mock<DbSet<Shipment>>();
            var data = new List<Shipment> { existingShipment }.AsQueryable();

            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockShipmentSet.As<IQueryable<Shipment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockValidationService.Setup(v => v.ValidateLetterAdding(letterAddingDto))
                .Throws(new ArgumentException(Constants.invalidParametersMessage));

            var exception = Assert.Throws<ArgumentException>(() => _service.AddLetters(letterAddingDto));

            Assert.Equal(Constants.invalidParametersMessage, exception.Message);

            _mockDataContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}
