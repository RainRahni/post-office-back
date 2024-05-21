namespace post_office_back.Models
{
    public class ParcelBag : Bag
    {
        public ICollection<Parcel> Parcels { get; set; }
        public ParcelBag(string bagNumber) : base(bagNumber)
        {
            Parcels = new List<Parcel>();
        }
    }
}
