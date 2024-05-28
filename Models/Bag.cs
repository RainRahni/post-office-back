using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace post_office_back.Models
{
    [Table("Bags")]
    public class Bag
    {
        [Key]
        public String BagNumber { get; set; }
        [ForeignKey("ShipmentNumber")]
        public string ShipmentNumber { get; set; } = string.Empty;
        public virtual Shipment Shipment { get; set; } = null!;
        public string Discriminator { get; private set; }
        public Bag(string bagNumber)
        {
            BagNumber = bagNumber;

        }

    }
}
