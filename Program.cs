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
            //troubleshoot why it doesn't exist
            //I'm currently running the method which is returning the list of climbers
            //Then I'm trying to run a foreach on that list.

            
            PopulateClimberList();
            foreach(Climber climber in allClimbers)
            {
                Console.WriteLine(climber.FirstName);
                Console.WriteLine(climber.LastName);
                Console.WriteLine(climber.Bmi);
            }
        }
        
        public static List<Climber> PopulateClimberList()
        {
            List<Climber> allClimbers = new List<Climber>();

            string baseUrl = "https://www.ifsc-climbing.org/index.php?option=com_ifsc&view=athlete&id=";
            
            
            for( int id = 1; id < 10; id++) //Generate athlete Ids
            {
                HtmlWeb scraper = new HtmlWeb();
                HtmlDocument doc = scraper.Load(baseUrl + id);
            
                if(doc != null) //otherwise it will retry the loop
                {
                    Climber climber = new Climber();

                    try //find nodes
                    {
                        HtmlNode nodeName = doc.DocumentNode.SelectSingleNode("//h1[@class='name']");
                        if(nodeName != null)
                        {   climber.FullName = nodeName.InnerHtml.Trim(); }

                        HtmlNode nodeNation = doc.DocumentNode.SelectSingleNode("//dt[@class='nation']");
                        if(nodeNation != null)
                        {   climber.Nationality = nodeNation.InnerHtml; }

                        HtmlNode nodeHeight = doc.DocumentNode.SelectSingleNode("//dt[@class='height']");
                        if(nodeHeight != null)
                        {
                            double parsedHeight = 0;
                            if (double.TryParse(nodeHeight.InnerHtml, out parsedHeight))
                            {   climber.Height = parsedHeight; }
                            else {  climber.Height = 0; }               
                        }
                        HtmlNode nodeWeight = doc.DocumentNode.SelectSingleNode("//dt[@class='weight']");
                        if(nodeWeight != null)
                        {
                            double parsedWeight = 0;
                            if (double.TryParse(nodeWeight.InnerHtml, out parsedWeight))
                            {   climber.Weight = parsedWeight; }
                            else {  climber.Weight = 0; } 
                        }
                        HtmlNode nodeBday = doc.DocumentNode.SelectSingleNode("//dt[@class='birthdate']");
                        if(nodeBday != null)
                        {
                            int parsedBday = 0;
                            if (int.TryParse(nodeBday.InnerHtml, out parsedBday))
                            { climber.BirthYear = parsedBday; }
                            else { climber.BirthYear = 0; }
                        }
                        allClimbers.Add(climber);
                        doc = null;
                        GC.Collect();

                        return allClimbers;
                    }
                    catch//()//a node doesn't exist- a single missing node shold stop processing the current doc
                    {   //Need to add the exact exception thrown.
                        //now that I think about it an exception won't be thrown (nevermind maybe they're will be)
                        //retry this with a missing climber see what exception is thrown.
                        Console.WriteLine("No Climber found");
                        //retry the loop - should do this automatically from the catch  
                    }
                }
            }     
        }    
    }
}