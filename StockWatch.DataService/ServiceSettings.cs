using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        public List<string> EmalCc { get; private set; }
        
        private ServiceSettings()
        {
            this.EmailSettingFile = Path.Combine(FileSystem.GetStcokWatchFolderOnGoogleDrive(), "EmailSetting.xml");
            var cc = ConfigurationManager.AppSettings["EmailCc"];
            if (cc != null)
                this.EmalCc = cc.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries).ToList();
            else 
                this.EmalCc =  new List<string>();
        }

        

        
    }



}
