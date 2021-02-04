using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;

namespace LY.ExamMonitor.Data.Configuration
{
    public class ExamMonitorMigrationsConfiguration : DbMigrationsConfiguration<ExamMonitorContext> 
    {
        public ExamMonitorMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;

            AutomaticMigrationDataLossAllowed = true;

            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());

            //MigrationsDirectory = "MigrationsConfiguration";
        }

        protected override void Seed(ExamMonitorContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
             
        } 
    }
}
