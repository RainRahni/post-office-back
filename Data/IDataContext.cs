using Microsoft.EntityFrameworkCore;
using post_office_back.Models;

namespace post_office_back.Data
{
    public interface IDataContext
    {
        DbSet<Shipment> Shipments { get; }
        DbSet<Bag> Bags { get; }
        DbSet<Parcel> Parcels { get; }
        int SaveChanges();
    }
}
