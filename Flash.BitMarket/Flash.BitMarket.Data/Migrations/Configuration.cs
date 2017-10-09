using System.Data.Entity.Migrations;

namespace Flash.BitMarket.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MainDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MainDbContext context)
        {
        }
    }
}
