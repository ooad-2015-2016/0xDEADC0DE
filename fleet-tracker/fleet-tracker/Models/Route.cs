namespace fleet_tracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fleetdb.Route")]
    public partial class Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Origin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OriginLat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OriginLong { get; set; }

        [StringLength(255)]
        public string Destination { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DestinationLat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DestinationLong { get; set; }

        public int? GroupID { get; set; }

        public virtual Group Group { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
