namespace post_office_back
{
    public static class Constants
    {
        public static string shipmentNumberPattern = @"^[A-Za-z0-9]{3}-[A-Za-z0-9]{6}$";
        public static string flightNumberPattern = @"^[A - Za - z]{2}\d{4}$";
        public static string invalidParametersMessage = "Invalid input!";
    }
}
