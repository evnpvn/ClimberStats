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

        public List<string> ParseHTML(string rawClimberHtml)
        {
            //iterate over the string
            //find and return the elements in a list

            //Find a .NET method that can be called against a string to break it up into sections
            //String split could work. If we could split the string right before the element we want.
            //Then we would have a list like this
                //object 0 - string of text before the first element I want
                //object 1 - string of text for the first element I want
                //etc.
                //Trim could work as well before I call split.
            

        }

    }
}