using Autofac;
using LY.ExamMonitor.Core.Interfaces;
using LY.ExamMonitor.Data;
using LY.ExamMonitor.Data.Respositories;
using LY.ExamMonitor.Services;

namespace LY.ExamMonitor.Client
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExamMonitorContext>().WithParameter(new NamedParameter("connectionString", @"Data Source=data.db")); 

            builder.RegisterType<ExamStudentRepository>().As<IExamStudentRepository>();
            builder.RegisterType<ExamStudentService>().As<IExamStudentService>();

        }
    }
}
