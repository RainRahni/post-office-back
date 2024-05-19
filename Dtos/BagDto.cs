using post_office_back.Models.Enums;

namespace post_office_back.Dtos
{
    public class BagDto(string bagNumber, int itemCount, BagType bagType)
    {
        public String BagNumber { get; set; } = bagNumber;
        public int ItemCount { get; set; } = itemCount;
        public BagType BagType { get; set; } = bagType;
    }
}
