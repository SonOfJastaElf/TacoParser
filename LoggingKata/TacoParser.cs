﻿using System.Security.Cryptography.X509Certificates;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();

        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                // Log that and return null
                // Do not fail if one record parsing fails, return null
                return null; // TODO Implement
            }


            var lat = double.Parse(cells[0]);

            var lon = double.Parse(cells[1]);

            var name = cells[2];

            var tacoBell = new TacoBell();

            Point p = new Point();
            p.Latitude = lat;
            p.Longitude = lon;
            tacoBell.Location = p;
            tacoBell.Name = name;
            return tacoBell;
        }
    }
}