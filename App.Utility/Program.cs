using System;
using System.Threading;

namespace App.Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
            IAppLogManager al = new AppLogManager();
            al.WriteLog("0000000000000");
        }
    }
}
