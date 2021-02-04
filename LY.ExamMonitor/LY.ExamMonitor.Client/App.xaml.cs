using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using LY.ExamMonitor.Core.Interfaces;
using LY.ExamMonitor.Data;
using LY.ExamMonitor.Data.Respositories;
using LY.ExamMonitor.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace LY.ExamMonitor.Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
              
            var container = ContainerServices();

            var service = container.Resolve<ClientService>();

            service.Write();
        }


        public IContainer ContainerServices()
        {
            var builder = new ContainerBuilder();

            //Assembly[] assemblies = new Assembly[] { Assembly.Load("LY.ExamMonitor.Services") };             
            //builder.RegisterAssemblyTypes(assemblies).Where(type => !type.IsAbstract && typeof(IExamStudentService).IsAssignableFrom(type));

            //builder.RegisterType<ExamMonitorContext>(); 
            //builder.RegisterType<ExamStudentRepository>().As<IExamStudentRepository>();
            //builder.RegisterType<ExamStudentService>().As<IExamStudentService>();

            builder.RegisterType<ClientService>();

            builder.RegisterModule<AutofacModule>();

            var container = builder.Build();

            //ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            return container;
        }

    }
     
    /// <summary>
    ///  It's test class
    /// </summary>
    public class ClientService
    {
        private IExamStudentService _examStudentService;

        public ClientService(IExamStudentService examStudentService)
        {
            _examStudentService = examStudentService;

        }

        public void Write()
        {
            _examStudentService.Init();

            Console.WriteLine("Test...");
        }
    }
}
