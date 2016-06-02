namespace fleet_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                "fleetdb.Group",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Public = c.Short(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Group_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("fleetdb.Group", t => t.Group_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Group_ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("fleetdb.GroupDevice", "GroupID", "fleetdb.Group");
            DropForeignKey("fleetdb.Vehicle", "TypeID", "fleetdb.VehicleType");
            DropForeignKey("fleetdb.Invoice", "VehicleID", "fleetdb.Vehicle");
            DropForeignKey("fleetdb.Vehicle", "GroupID", "fleetdb.Group");
            DropForeignKey("fleetdb.Tick", "InvoiceID", "fleetdb.Invoice");
            DropForeignKey("fleetdb.Tick", "DeviceID", "fleetdb.Device");
            DropForeignKey("fleetdb.Invoice", "RouteID", "fleetdb.Route");
            DropForeignKey("fleetdb.Route", "GroupID", "fleetdb.Group");
            DropForeignKey("fleetdb.Invoice", "GroupID", "fleetdb.Group");
            DropForeignKey("fleetdb.Invoice", "DriverID", "fleetdb.Driver");
            DropForeignKey("fleetdb.Invoice", "DeviceID", "fleetdb.Device");
            DropForeignKey("fleetdb.Driver", "GroupID", "fleetdb.Group");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Group_ID", "fleetdb.Group");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("fleetdb.GroupDevice", "DeviceID", "fleetdb.Device");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("fleetdb.Vehicle", new[] { "GroupID" });
            DropIndex("fleetdb.Vehicle", new[] { "TypeID" });
            DropIndex("fleetdb.Tick", new[] { "InvoiceID" });
            DropIndex("fleetdb.Tick", new[] { "DeviceID" });
            DropIndex("fleetdb.Route", new[] { "GroupID" });
            DropIndex("fleetdb.Invoice", new[] { "GroupID" });
            DropIndex("fleetdb.Invoice", new[] { "DriverID" });
            DropIndex("fleetdb.Invoice", new[] { "DeviceID" });
            DropIndex("fleetdb.Invoice", new[] { "VehicleID" });
            DropIndex("fleetdb.Invoice", new[] { "RouteID" });
            DropIndex("fleetdb.Driver", new[] { "GroupID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_ID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("fleetdb.GroupDevice", new[] { "GroupID" });
            DropIndex("fleetdb.GroupDevice", new[] { "DeviceID" });
            DropTable("dbo.AspNetRoles");
            DropTable("fleetdb.VehicleType");
            DropTable("fleetdb.Vehicle");
            DropTable("fleetdb.Tick");
            DropTable("fleetdb.Route");
            DropTable("fleetdb.Invoice");
            DropTable("fleetdb.Driver");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("fleetdb.Group");
            DropTable("fleetdb.GroupDevice");
            DropTable("fleetdb.Device");
        }
    }
}
