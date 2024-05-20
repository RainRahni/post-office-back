using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace post_office_back.Models
{
    [Table("Bags")]
    public class Bag
    {
        [Key]
        public String BagNumber { get; set; }
        public Bag(string bagNumber)
        {
            BagNumber = bagNumber;
        }

    }
}
