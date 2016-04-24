namespace fleet_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "fleetdb.Account",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255),
                        Password = c.String(maxLength: 255),
                        LastActive = c.DateTime(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        TypeID = c.Int(),
                        GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("fleetdb.AccountType", t => t.TypeID)
                .ForeignKey("fleetdb.Group", t => t.GroupID)
                .Index(t => t.TypeID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "fleetdb.AccountType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "fleetdb.Group",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Public = c.Short(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "fleetdb.Driver",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        SocialNumber = c.String(maxLength: 20),
                        GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("fleetdb.Group", t => t.GroupID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "fleetdb.Invoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Token = c.String(maxLength: 20),
                        RouteID = c.Int(),
                        VehicleID = c.Int(),
                        DeviceID = c.Int(),
                        DriverID = c.Int(),
                        GroupID = c.Int(),
                        Finished = c.Short(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        StartedAt = c.DateTime(),
                        FinishedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("fleetdb.Device", t => t.DeviceID)
                .ForeignKey("fleetdb.Driver", t => t.DriverID)
                .ForeignKey("fleetdb.Group", t => t.GroupID)
                .ForeignKey("fleetdb.Route", t => t.RouteID)
                .ForeignKey("fleetdb.Vehicle", t => t.VehicleID)
                .Index(t => t.RouteID)
                .Index(t => t.VehicleID)
                .Index(t => t.DeviceID)
                .Index(t => t.DriverID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "fleetdb.Device",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "fleetdb.GroupDevice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeviceID = c.Int(),
                        GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("fleetdb.Device", t => t.DeviceID)
                .ForeignKey("fleetdb.Group", t => t.GroupID)
                .Index(t => t.DeviceID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "fleetdb.Tick",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeviceID = c.Int(),
                        InvoiceID = c.Int(),
                        Lat = c.Decimal(precision: 10, scale: 6, storeType: "numeric"),
                        Long = c.Decimal(precision: 10, scale: 6, storeType: "numeric"),
                        Message = c.String(maxLength: 255),
                        CreatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("fleetdb.Device", t => t.DeviceID)
                .ForeignKey("fleetdb.Invoice", t => t.InvoiceID)
                .Index(t => t.DeviceID)
                .Index(t => t.InvoiceID);
            
            CreateTable(
                "fleetdb.Route",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Origin = c.String(maxLength: 255),
                        OriginLat = c.Decimal(precision: 10, scale: 6, storeType: "numeric"),
                        OriginLong = c.Decimal(precision: 10, scale: 6, storeType: "numeric"),
                        Destination = c.String(maxLength: 255),
                        DestinationLat = c.Decimal(precision: 10, scale: 6, storeType: "numeric"),
                        DestinationLong = c.Decimal(precision: 10, scale: 6, storeType: "numeric"),
                        GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("fleetdb.Group", t => t.GroupID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "fleetdb.Vehicle",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        TypeID = c.Int(),
                        RegistrationNumber = c.String(maxLength: 255),
                        ChassisNumber = c.String(maxLength: 255),
                        GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("fleetdb.Group", t => t.GroupID)
                .ForeignKey("fleetdb.VehicleType", t => t.TypeID)
                .Index(t => t.TypeID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "fleetdb.VehicleType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("fleetdb.Vehicle", "TypeID", "fleetdb.VehicleType");
            DropForeignKey("fleetdb.Invoice", "VehicleID", "fleetdb.Vehicle");
            DropForeignKey("fleetdb.Vehicle", "GroupID", "fleetdb.Group");
            DropForeignKey("fleetdb.Invoice", "RouteID", "fleetdb.Route");
            DropForeignKey("fleetdb.Route", "GroupID", "fleetdb.Group");
            DropForeignKey("fleetdb.Invoice", "GroupID", "fleetdb.Group");
            DropForeignKey("fleetdb.Invoice", "DriverID", "fleetdb.Driver");
            DropForeignKey("fleetdb.Tick", "InvoiceID", "fleetdb.Invoice");
            DropForeignKey("fleetdb.Tick", "DeviceID", "fleetdb.Device");
            DropForeignKey("fleetdb.Invoice", "DeviceID", "fleetdb.Device");
            DropForeignKey("fleetdb.GroupDevice", "GroupID", "fleetdb.Group");
            DropForeignKey("fleetdb.GroupDevice", "DeviceID", "fleetdb.Device");
            DropForeignKey("fleetdb.Driver", "GroupID", "fleetdb.Group");
            DropForeignKey("fleetdb.Account", "GroupID", "fleetdb.Group");
            DropForeignKey("fleetdb.Account", "TypeID", "fleetdb.AccountType");
            DropIndex("fleetdb.Vehicle", new[] { "GroupID" });
            DropIndex("fleetdb.Vehicle", new[] { "TypeID" });
            DropIndex("fleetdb.Route", new[] { "GroupID" });
            DropIndex("fleetdb.Tick", new[] { "InvoiceID" });
            DropIndex("fleetdb.Tick", new[] { "DeviceID" });
            DropIndex("fleetdb.GroupDevice", new[] { "GroupID" });
            DropIndex("fleetdb.GroupDevice", new[] { "DeviceID" });
            DropIndex("fleetdb.Invoice", new[] { "GroupID" });
            DropIndex("fleetdb.Invoice", new[] { "DriverID" });
            DropIndex("fleetdb.Invoice", new[] { "DeviceID" });
            DropIndex("fleetdb.Invoice", new[] { "VehicleID" });
            DropIndex("fleetdb.Invoice", new[] { "RouteID" });
            DropIndex("fleetdb.Driver", new[] { "GroupID" });
            DropIndex("fleetdb.Account", new[] { "GroupID" });
            DropIndex("fleetdb.Account", new[] { "TypeID" });
            DropTable("fleetdb.VehicleType");
            DropTable("fleetdb.Vehicle");
            DropTable("fleetdb.Route");
            DropTable("fleetdb.Tick");
            DropTable("fleetdb.GroupDevice");
            DropTable("fleetdb.Device");
            DropTable("fleetdb.Invoice");
            DropTable("fleetdb.Driver");
            DropTable("fleetdb.Group");
            DropTable("fleetdb.AccountType");
            DropTable("fleetdb.Account");
        }
    }
}
