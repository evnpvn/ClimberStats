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
            
            //HtmlDocument doc = Webget.Load(url);

            HtmlDocument doc = Webget.Load(url);
            
            if(doc != null)
            {
                //There's an issue with climber objects being created that will have null values
                //I guess it's no big deal if the climber isn't added to the list at the end.
                Climber climber = new Climber();

                try // find nodes
                {
                    HtmlNode nodeName = doc.DocumentNode.SelectSingleNode("//h1[@class='name']");
                    if(nodeName != null)
                    {
                        climber.FullName = nodeName.InnerHtml.Trim();
                    }
                    HtmlNode nodeNation = doc.DocumentNode.SelectSingleNode("//dt[@class='nation']");
                    if(nodeNation != null)
                    {
                        climber.Nationality = nodeNation.InnerHtml;
                    }
                    HtmlNode nodeHeight = doc.DocumentNode.SelectSingleNode("//dt[@class='height']");
                    if(nodeHeight != null)
                    {
                        double parsedHeight = 0;
                        if (double.TryParse(nodeHeight.InnerHtml, out parsedHeight))
                        { climber.Height = parsedHeight; }
                        else { climber.Height = 0; }               
                    }
                    HtmlNode nodeWeight = doc.DocumentNode.SelectSingleNode("//dt[@class='weight']");
                    if(nodeWeight != null)
                    {
                        double parsedWeight = 0;
                        if (double.TryParse(nodeWeight.InnerHtml, out parsedWeight))
                        { climber.Weight = parsedWeight; }
                        else { climber.Weight = 0; } 
                    }
                    HtmlNode nodeBday = doc.DocumentNode.SelectSingleNode("//dt[@class='birthdate']");
                    if(nodeBday != null)
                    {
                        int parsedBday = 0;
                        if (int.TryParse(nodeBday.InnerHtml, out parsedBday))
                        { climber.BirthYear = parsedBday; }
                        else { climber.BirthYear = 0; }
                    }
                    Console.WriteLine(climber.FirstName);
                    Console.WriteLine(climber.LastName);
                    Console.WriteLine(climber.Bmi);
                    allClimbers.Add(climber);
                }
                catch //a node doesn't exist (a single nonexistant node shold stop processing it)
                {
                    Console.WriteLine("No Climber found");
                }
            }
            else
            {
                Console.WriteLine("No Athlete found");
            }  
        }    
    }
}