using Microsoft.SqlServer.Server;
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

namespace DirectoryMonitorService
{
    public partial class DirectoryMonitorService : ServiceBase
    {
        MenuOption option;
        public DirectoryMonitorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            option = new MenuOption();
            option.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
