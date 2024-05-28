namespace post_office_back
{
    public class Constants
    {
        public const string shipmentNumberPattern = @"^[A-Za-z0-9]{3}-[A-Za-z0-9]{6}$"; //Format “XXX-XXXXXX”, where X – letter or digit
        public const string flightNumberPattern = @"^[A-Za-z]{2}\d{4}$"; //Format “LLNNNN”, where L – letter, N – digit
        public const string invalidParametersMessage = "Invalid input!";
        public const string bagNumberPattern = @"^[a-zA-Z0-9]{1,15}$"; //Max length 15 characters, no special symbols allowed
        public const string parcelNumberPattern = @"^[A-Za-z]{2}\d{6}[A-Za-z]{2}$"; //Format “LLNNNNNNLL”, where L – letter, N – digit
        public const string destinationCountryPattern = @"^[A-Za-z]{2}$"; //2 character country code, "EE", "LV" etc.
        public const int recipientNameMaxLength = 100;
        public const string cannotFinalizeShipmentMessage = "Cannot finalize shipement!";
        public const string negativeNumberOfLettersMessage = "Number of letters must be greater than zero.";
    }
}
