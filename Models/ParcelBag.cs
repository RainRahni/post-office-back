namespace post_office_back.Models
{
    public class ParcelBag(string BagNumber) : Bag(BagNumber)
    {
        public ICollection<Parcel> Parcels { get; set; }
    }
}
