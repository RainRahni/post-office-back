using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace post_office_back.Models
{
    [Table("Parcels")]
    public class Parcel
    {
        [Key]
        public String ParcelNumber { get; set; } = string.Empty;
        public String RecipientName { get; set; } = string.Empty;
        [ForeignKey("ParcelBagBagNumber")]

        public String ParcelBagBagNumber = string.Empty;
        public ParcelBag ParcelBag = null!;
        public String DestinationCountry { get; set; } = string.Empty;
        private decimal _weight;
        private decimal _price;
        public decimal Weight
        {
            get { return _weight; }
            set { _weight = Math.Round(value, 3); }
        }
        public decimal Price
        {
            get { return _price; }
            set { _price = Math.Round(value, 2); }
        }
        public Parcel(string parcelNumber, string recipientName, string destinationCountry, decimal weight, decimal price)
        {
            ParcelNumber = parcelNumber;
            RecipientName = recipientName;
            Weight = weight;
            Price = price;
            DestinationCountry = destinationCountry;
        }
    }
}
