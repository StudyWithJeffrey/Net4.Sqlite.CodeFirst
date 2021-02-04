using SQLite.CodeFirst;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace LY.ExamMonitor.Data
{


    internal class CreateOrMigrateDBInitializer<TContext> : SqliteDropCreateDatabaseWhenModelChanges<TContext> where TContext : DbContext
    {


        private readonly int _currentSchemaVersion;

        public CreateOrMigrateDBInitializer(DbModelBuilder modelBuilder, int currentSchemaVersion) :
            base(modelBuilder)
        {
            
            _currentSchemaVersion = currentSchemaVersion;
        }

        /// <summary>
        /// 数据库创建初始化数据
        /// context.Set<Customer>().Add(new Customer() { Id = Guid.NewGuid().ToString(), Name = "FirstCustomer", Age = 23 });
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(TContext context)
        {
            base.Seed(context);

            context.Database.ExecuteSqlCommand(
                $"PRAGMA user_version={_currentSchemaVersion}");

        }

        /// <summary>
        /// Initialize Database
        /// </summary>
        /// <param name="context"></param>
        public override void InitializeDatabase(TContext context)
        {
            base.InitializeDatabase(context);
            if (context.Database.Exists())
                Migrate(context);
        }

        private static void Migrate(DbContext context)
        {
            var currentDatabaseVersion =
                context.Database.SqlQuery<int>("PRAGMA user_version").First();

            if (!Directory.Exists("migrations"))
            {
                return;
            }

            var scriptFiles = Directory.GetFiles("migrations/", "*.sql");

            foreach (var scriptFile in scriptFiles)
            {
                var count = MigrateWithScriptFileFromVersion(context, scriptFile,
                     currentDatabaseVersion);

                if (count >= 0)
                {
                    File.Delete(scriptFile);
                }
            }
        }

        private static int MigrateWithScriptFileFromVersion(DbContext context, string scriptFile, int currentVersion)
        {
            var filenamePrefix = Path.GetFileName(scriptFile)?.Split('.').First();

            if (!int.TryParse(filenamePrefix, out var targetVersion) || targetVersion <= currentVersion)
            {
                return -1;
            }

            var migrationScript = File.ReadAllText(scriptFile);
            return context.Database.ExecuteSqlCommand(migrationScript);
        }
    }

}
