namespace Flash.BitMarket.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teesting2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Quotes", newName: "QuoteTests");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.QuoteTests", newName: "Quotes");
        }
    }
}
