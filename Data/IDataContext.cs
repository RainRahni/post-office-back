using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using post_office_back.Models;

namespace post_office_back.Data
{
    public interface IDataContext
    {
        DbSet<Shipment> Shipments { get; }
        DbSet<Bag> Bags { get; }
        DbSet<Parcel> Parcels { get; }
        int SaveChanges();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
