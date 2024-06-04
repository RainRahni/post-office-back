using post_office_back.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace post_office_back.Models
{
    [Table("Bags")]
    public class Bag
    {
        [Key]
        public string BagNumber { get; set; }
        [ForeignKey("ShipmentNumber")]
        public string ShipmentNumber { get; set; } = string.Empty;
        public virtual Shipment Shipment { get; set; } = null!;
        public BagType BagType { get; set; } = BagType.BAG;
        public ICollection<Parcel> Parcels { get; } = new List<Parcel>(); 
        public int? CountOfLetters { get; set; }
        private decimal _weight = 0;
        private decimal _price = 0;
        public Bag(string bagNumber)
        {
            BagNumber = bagNumber;

        }
        public virtual void AddLetters(int numberOfLetters)
        {
            if (numberOfLetters <= 0 && !BagType.Equals(BagType.PARCELBAG))
            {
                throw new ArgumentException(Constants.negativeNumberOfLettersMessage);
            }

            CountOfLetters += numberOfLetters;
        }
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
    }
}
