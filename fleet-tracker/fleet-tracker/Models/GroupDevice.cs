namespace fleet_tracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fleetdb.GroupDevice")]
    public partial class GroupDevice
    {
        public int ID { get; set; }

        public int? DeviceID { get; set; }

        public int? GroupID { get; set; }

        public virtual Device Device { get; set; }

        public virtual Group Group { get; set; }
    }
}
