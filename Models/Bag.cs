using System.ComponentModel.DataAnnotations.Schema;

namespace post_office_back.Models
{
    [Table("Bags")]
    public class Bag(string bagNumber)
    {
        public String BagNumber { get; set; } = bagNumber;
    }
}
