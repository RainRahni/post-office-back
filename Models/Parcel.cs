using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace post_office_back.Models
{
    [Table("Parcels")]
    public class Parcel
    {
        [Key]
        public String ParcelNumber { get; }
        public String RecipientName { get; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public Parcel(string parcelNumber, string recipientName, decimal weight, decimal price)
        {
            ParcelNumber = parcelNumber;
            RecipientName = recipientName;
            Weight = weight;
            Price = price;
        }
    }
}
