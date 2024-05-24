namespace post_office_back.Models
{
    public class LetterBag : Bag
    {
        public uint CountOfLetters {get; set;}
        private decimal _weight = 0;
        private decimal _price = 0;
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
        public LetterBag(string bagNumber) : base(bagNumber)
        {

        }
        public void AddLetters(uint numberOfLetters)
        {
            if (numberOfLetters <= 0)
            {
                throw new ArgumentException(Constants.negativeNumberOfLettersMessage);
            }

            CountOfLetters += numberOfLetters;
        }
    }
}
