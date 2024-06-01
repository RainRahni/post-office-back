using post_office_back.Models.Enums;

namespace post_office_back.Dtos
{
    public class BagDto(string bagNumber, int itemCount, string bagType, decimal bagPrice)
    {
        public string BagNumber { get; set; } = bagNumber;
        public int ItemCount { get; set; } = itemCount;
        public string BagType { get; set; } = bagType;
        public decimal BagPrice { get; set; } = bagPrice;
    }
}
