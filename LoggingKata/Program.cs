using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Diagnostics.SymbolStore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;
using System.Reflection.Emit;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable locOne = null;
            ITrackable locTwo = null;
            ITrackable locA = null;
            ITrackable locB = null;

            double dist = 0;
            
            for(int i = 0; i < locations.Length; i++)
            {
                locOne = locations[i];
                var corA = new GeoCoordinate();
                corA.Longitude = locOne.Location.Longitude;
                corA.Latitude = locOne.Location.Latitude;
                
                for(int j = 0; j < locations.Length; j++)
                {
                    locTwo = locations[j];
                    var corB = new GeoCoordinate();
                    corB.Longitude = locTwo.Location.Longitude;
                    corB.Latitude = locTwo.Location.Latitude;
                    
                    if (corA.GetDistanceTo(corB) > dist)
                    {
                        dist = corA.GetDistanceTo(corB);
                        locA = locOne;
                        locB = locTwo;
                    }

                }
            }
            Console.WriteLine($"{locA.Name} and {locB.Name} are the Taco Bell locations that are the farthest apart.");
        }
    }
}
