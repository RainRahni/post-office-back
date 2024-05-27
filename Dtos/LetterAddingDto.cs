namespace post_office_back.Dtos
{
    public class LetterAddingDto
    {
        public string BagNumber { get; set; } = string.Empty;
        public string ShipmentNumber {  get; set; } = string.Empty;
        public int NumberOfLetters { get; set; }
    }
}
