namespace post_office_back
{
    public class Constants
    {
        public const string shipmentNumberPattern = @"^[A-Za-z0-9]{3}-[A-Za-z0-9]{6}$";
        public const string flightNumberPattern = @"^[A - Za - z]{2}\d{4}$";
        public const string invalidParametersMessage = "Invalid input!";
        public const string bagNumberPattern = @"^[a-zA-Z0-9]{1,15}$";
        public const string parcelNumberPattern = @"^[A-Za-z]{2}\d{6}[A-Za-z]{2}$";
        public const string destinationCountryPattern = @"^[A-Za-z]{2}$";
        public const int recipientNameMaxLength = 100;
    }
}
