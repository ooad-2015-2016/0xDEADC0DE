namespace fleet_tracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fleetdb.Invoice")]
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            Ticks = new HashSet<Tick>();
        }

        public int ID { get; set; }

        [StringLength(20)]
        public string Token { get; set; }

        public int? RouteID { get; set; }

        public int? VehicleID { get; set; }

        public int? DeviceID { get; set; }

        public int? DriverID { get; set; }

        public int? GroupID { get; set; }

        public short? Finished { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public virtual Device Device { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Group Group { get; set; }

        public virtual Route Route { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tick> Ticks { get; set; }
    }
}
