using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataService
{
    public class ServiceSettings
    {
        private static readonly Lazy<ServiceSettings> _lazyInstance =
            new Lazy<ServiceSettings>(() => new ServiceSettings());

        public static ServiceSettings Instance
        {
            get { return _lazyInstance.Value; }
        }

        public string EmailSettingFile { get; private set; }
        
        private ServiceSettings()
        {
            this.EmailSettingFile = ConfigurationManager.AppSettings["EmailSettingFile"];
        }

        

        
    }



}
