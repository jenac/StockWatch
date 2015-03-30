using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace StockWatch.WinSvc
{
    [RunInstaller(true)]
    public partial class DataServiceInstaller : System.Configuration.Install.Installer
    {
        public DataServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
