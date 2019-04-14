using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace SoccerStats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            DirectoryInfo directory = new DirectoryInfo(currentDirectory);

            string fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");
            List<GameResult> fileContents = ReadSoccerResults(fileName);

            fileName = Path.Combine(directory.FullName, "players.json");
            List<Player> players = DeserializePlayers(fileName);

            //called the method GetTopTenPlayers which we passed a list of players and assigned the result to list variable topTenPlayers
            List<Player> topTenPlayers = GetTopTenPlayers(players);

            foreach(Player player in topTenPlayers)
            {
                Console.WriteLine("Name: " + player.FirstName + " Points per game: " + player.PointsPerGame);
            }

            fileName = Path.Combine(directory.FullName, "topten.json");
            SerializePlayersToFile(topTenPlayers, fileName);

            Console.WriteLine(GetGoogleHomePage());

        }

        public static string ReadFile(string fileName)
        {   
            using (StreamReader reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }   
    
        public static List<GameResult> ReadSoccerResults(string fileName)
        {
            List<GameResult> soccerResults = new List<GameResult>();

            //StreamReader instantiated to read the SoccerGameResults file 
            using (StreamReader reader = new StreamReader(fileName))
            {
                //line variable created to contain each line of the file
                string line = "";

                //To read the header fields
                reader.ReadLine();

                //while Readline is assinging a value to line variable that is not null
                while((line = reader.ReadLine()) != null)
                {
                    //split the string line up by each field and assign it to the value variable
                    string[] values = line.Split(',');

                    //instantiate a new gameResult object so we can use the new property
                    GameResult gameResult = new GameResult();
                    //create a variable for gameDate.
                    DateTime gameDate;
                    //Try to parse the first string variable in the values string array
                    //assign the parsed value to the gameDate variable
                    if(DateTime.TryParse(values[0], out gameDate))
                    {
                        //if all that worked then take the parsed gameDate and set it
                        //to the GameDate property on the gameResult object
                        gameResult.GameDate = gameDate;
                    }

                    gameResult.TeamName = values[1];

                    HomeOrAway homeOrAway;
                    if (Enum.TryParse(values[2], out homeOrAway))
                    {
                        gameResult.HomeOrAway = homeOrAway;
                    }

                    int parseInts;
                    if(Int32.TryParse(values[3], out parseInts))
                    {
                        gameResult.Goals = parseInts;
                    }

                    if(Int32.TryParse(values[4], out parseInts))
                    {
                        gameResult.GoalAttempts = parseInts;
                    }

                    if(Int32.TryParse(values[5], out parseInts))
                    {
                        gameResult.ShotsOnGoal = parseInts;
                    }

                    if(Int32.TryParse(values[6], out parseInts))
                    {
                        gameResult.ShotsOffGoal = parseInts;
                    }

                    double possessionPercent;
                    if(double.TryParse(values[7], out possessionPercent))
                    {
                        gameResult.PossessionPercent = possessionPercent;
                    }

                    soccerResults.Add(gameResult);
                }
            }

            return soccerResults;
        }
    

        //new method which returns a list of 'Player' objects and takes a fileName as a parameter
        //FYI we just overrode the fileName so both files can't be used at once
        public static List<Player> DeserializePlayers(string fileName)
        {
            //create a new instance of the players list
            //need to do this I think because we will use this 
            //instance to deserialize the data against
            //I think moreso we need access to the player methods (properties)
            List<Player> players = new List<Player>();        

            //in order to deserialize we need to read the file.
                //so created a streamreader object and a JsonTextReader object
            using (StreamReader reader = new StreamReader(fileName))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                //created a new serializer Object. Need to do this to use call the methods against the instance
                JsonSerializer serializer = new JsonSerializer();
                players = serializer.Deserialize<List<Player>>(jsonReader);
            }
            
            return players;
        }

        public static List<Player> GetTopTenPlayers(List<Player> players)
        {
            //create a new variable instance called topTenPlayers to use as a subset of the original all players list
            List<Player> topTenPlayers = new List<Player>();
            //we call the sort method against hte players object. The sort method takes the playercomparer parameter
            players.Sort(new PlayerComparer());

            players.Reverse();

            int counter = 0;
            foreach(Player player in players)
            {
                topTenPlayers.Add(player);
                counter++;
                if (counter == 10)
                {
                    break;
                }
            }
            return topTenPlayers;
        }

        public static void SerializePlayersToFile(List<Player> players, string fileName)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter writer = new StreamWriter(fileName))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, players);
            }
        }
    
        public static string GetGoogleHomePage()
        {
            WebClient webClient = new WebClient();
            byte [] googleHome = webClient.DownloadData("https://www.google.com");

            using (MemoryStream stream = new MemoryStream(googleHome))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
            
        }
    }
}