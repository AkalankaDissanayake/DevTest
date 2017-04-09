using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utility
{

    public interface IAppLogManager
    {
        void WriteLog(object obj);
    }
    public abstract class BaseAppLogManager : IAppLogManager
    {
        public abstract void WriteLog(object obj);
    }
    public class AppLogManager : BaseAppLogManager
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string _path;
        private static string _fileName;
        public AppLogManager()
        {
            _path = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Logs";
            _fileName = DateTime.Today.ToString("yyyy-MM-dd") + "_Log.log";
            log4net.GlobalContext.Properties["LogFileName"] = _path + "\\" + _fileName;
            XmlConfigurator.Configure();
        }

        public AppLogManager(string path)
        {
            _path = path;
            string fileName = DateTime.Today.ToString("yyyy-MM-dd") + "_Log.log";
            log4net.GlobalContext.Properties["LogFileName"] = path + "\\" + fileName;
            XmlConfigurator.Configure();
        }
        public AppLogManager(string path,string fileName)
        {
            _path = path;
            _fileName = fileName;
            log4net.GlobalContext.Properties["LogFileName"] = _path + "\\" + _fileName;
            XmlConfigurator.Configure();
        }
        public override void WriteLog(object obj)
        {
            Log.Error(obj);
        }
    }
}
