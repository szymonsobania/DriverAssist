using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace DriverAssist.Models
{
    public class Coordinate
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Model
    {
        public List<Coordinate> Coordinates { get; set; }

        public Model()
        {
            Coordinates = new List<Coordinate>();

            //DEMO tylko do testow
            String path = HttpContext.Current.Server.MapPath("~/img/gliwice_kato.txt");
            String line;
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        var coords = line.Split(';');
                        Coordinates.Add(new Coordinate { Lat = Double.Parse(coords[0], CultureInfo.InvariantCulture), Lng = Double.Parse(coords[1], CultureInfo.InvariantCulture) });
                        for (int i = 0; i < 1; i++)  // testy wydajnosciowe :p
                        {
                            Coordinates.Add(new Coordinate { Lat = Double.Parse(coords[0], CultureInfo.InvariantCulture), Lng = Double.Parse(coords[1], CultureInfo.InvariantCulture) + 0.00001 * i });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

    }
}