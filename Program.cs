using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace ClimberStats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateClimberList();
        }
        
        public static void CreateClimberList()
        {
            List<Climber> allClimbers = new List<Climber>();

            string url = "https://www.ifsc-climbing.org/index.php?option=com_ifsc&view=athlete&id=60694";
            HtmlWeb Webget = new HtmlWeb();
            HtmlDocument doc = Webget.Load(url);
            
            Climber climber = new Climber();

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//h1[@class='name']"))
            {
                climber.FullName = node.InnerHtml.Trim();
            }
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//dt[@class='nation']"))
            {
                climber.Nationality = node.InnerHtml;
            }
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//dt[@class='height']"))
            {
                //TODO - add error handling for this.
                //also need error handling for divide by zero
                //and null values
                    //If it returns false then don't assign the value back to climber.Height
                double parsedHeight = 0;
                if (double.TryParse(node.InnerHtml, out parsedHeight))
                { climber.Height = parsedHeight; }
                else { climber.Height = 0; }               
            }
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//dt[@class='weight']"))
            {
                double parsedWeight = 0;
                if (double.TryParse(node.InnerHtml, out parsedWeight))
                { climber.Weight = parsedWeight; }
                else { climber.Weight = 0; } 
            }
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//dt[@class='birthdate']"))
            {
                int parsedBday = 0;
                if (int.TryParse(node.InnerHtml, out parsedBday))
                { climber.BirthYear = parsedBday; }
                else { climber.BirthYear = 0; }
            }
            Console.WriteLine(climber.FirstName);
            Console.WriteLine(climber.LastName);
            Console.WriteLine(climber.Bmi);
            allClimbers.Add(climber);
        }    
    }
}