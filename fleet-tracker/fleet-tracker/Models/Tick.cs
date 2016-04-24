namespace fleet_tracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fleetdb.Tick")]
    public partial class Tick
    {
        public int ID { get; set; }

        public int? DeviceID { get; set; }

        public int? InvoiceID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Lat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Long { get; set; }

        [StringLength(255)]
        public string Message { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Device Device { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
