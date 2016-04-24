namespace fleet_tracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fleetdb.Vehicle")]
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? TypeID { get; set; }

        [StringLength(255)]
        public string RegistrationNumber { get; set; }

        [StringLength(255)]
        public string ChassisNumber { get; set; }

        public int? GroupID { get; set; }

        public virtual Group Group { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
