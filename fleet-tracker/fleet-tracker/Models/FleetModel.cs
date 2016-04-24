namespace fleet_tracker.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FleetModel : DbContext
    {
        public FleetModel()
            : base("name=FleetModel")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupDevice> GroupDevices { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Tick> Ticks { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountType>()
                .HasMany(e => e.Accounts)
                .WithOptional(e => e.AccountType)
                .HasForeignKey(e => e.TypeID);

            modelBuilder.Entity<Route>()
                .Property(e => e.OriginLat)
                .HasPrecision(10, 6);

            modelBuilder.Entity<Route>()
                .Property(e => e.OriginLong)
                .HasPrecision(10, 6);

            modelBuilder.Entity<Route>()
                .Property(e => e.DestinationLat)
                .HasPrecision(10, 6);

            modelBuilder.Entity<Route>()
                .Property(e => e.DestinationLong)
                .HasPrecision(10, 6);

            modelBuilder.Entity<Tick>()
                .Property(e => e.Lat)
                .HasPrecision(10, 6);

            modelBuilder.Entity<Tick>()
                .Property(e => e.Long)
                .HasPrecision(10, 6);

            modelBuilder.Entity<VehicleType>()
                .HasMany(e => e.Vehicles)
                .WithOptional(e => e.VehicleType)
                .HasForeignKey(e => e.TypeID);
        }
    }
}
