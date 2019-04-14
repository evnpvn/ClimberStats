using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace ClimberStats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(GetMaddieClimber());
        }

        public static string GetMaddieClimber()
        {
            WebClient webClient = new WebClient();
            byte [] maddieIfscPagedwnload = webClient.DownloadData("https://www.ifsc-climbing.org/index.php?option=com_ifsc&view=athlete&id=60694");

            using (MemoryStream stream = new MemoryStream(maddieIfscPagedwnload))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
            
        }
    }
}