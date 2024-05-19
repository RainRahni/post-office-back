using System.ComponentModel.DataAnnotations.Schema;

namespace post_office_back.Models
{
    [Table("Parcels")]
    public class Parcel(string parcelNumber, string recipientName, decimal weight, decimal price)
    {
        public String ParcelNumber { get; } = parcelNumber;
        public String RecipientName { get; } = recipientName;
        public decimal Weight { get; set; } = weight;
        public decimal Price { get; set; } = price;
    }
}
