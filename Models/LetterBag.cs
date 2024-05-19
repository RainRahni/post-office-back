namespace post_office_back.Models
{
    public class LetterBag(string BagNumber) : Bag(BagNumber)  
    {
        public uint CountOfLetters {get; set;}
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
    }
}
