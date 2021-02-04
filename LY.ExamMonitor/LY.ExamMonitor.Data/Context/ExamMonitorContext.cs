using LY.ExamMonitor.Data.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LY.ExamMonitor.Data
{

    public class ExamMonitorContext : SqliteDbContext<ExamMonitorContext>
    {

        private const int CurrentSchemaVersion = 0;

        public ExamMonitorContext():base() { }

        public ExamMonitorContext(string connectionString) : base(connectionString, CurrentSchemaVersion)
        {
        }

        //private EsmDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        //{
        //}

        public static ExamMonitorContext GetEsmDbContext()
        {
            var dbContext = new ExamMonitorContext(@"Data Source=data\data.db");
             
            dbContext.Database.CommandTimeout = 600;

            return dbContext;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            //取消表名复数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ExamMonitorContext, ExamMonitorMigrationsConfiguration>(true));

            base.OnModelCreating(modelBuilder);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    } 
}
