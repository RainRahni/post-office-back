namespace post_office_back.Models
{
    public class ParcelBag : Bag
    {
        public ICollection<Parcel> Parcels { get;} = new List<Parcel>();
        public ParcelBag(string bagNumber) : base(bagNumber) { }
    }
}
