namespace fleet_tracker.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class FleetModel : IdentityDbContext<AppUser>
    {
        public FleetModel()
            : base("name=FleetModel")
        {
        }
       
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
            base.OnModelCreating(modelBuilder);            

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

       // public System.Data.Entity.DbSet<AppUserManager.Models.ApplicationUser> AppUsers { get; set; }

        //public System.Data.Entity.DbSet<> IdentityRoles { get; set; }
    }
}
