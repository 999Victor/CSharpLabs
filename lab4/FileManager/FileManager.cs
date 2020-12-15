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

namespace FileManager
{
    public partial class FileManagerSetvice : ServiceBase
    {
        MenuOption option;
        public FileManager()
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
