using ConfigurationManager;
using DAL.Repositories;
using FileManager;
using SL.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var manager = new ConfigManager();

            var pathConfig = manager.GetConfig<PathConfig>(basePath + "config.xml");
            var logConfig = manager.GetConfig<LogConfig>(basePath + "appSettings.json");

            try
            {
                var unitOfWork = new UnitOfWork(pathConfig.PathToDB);
                ProductService employeeService = new ProductService(unitOfWork);
                var employeesInfo = employeeService.GetListOfEmployees();

                GenerateXML persons = new GenerateXML(pathConfig.Source + "\\Products.xml");
                persons.XmlGenerate(employeesInfo);
            }
            catch (Exception ex)
            {
                using (var writer = new StreamWriter(logConfig.Log, true))
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }

        protected override void OnStop()
        {
        }
    }
}
