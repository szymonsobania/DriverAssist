using System;
using System.Collections.Generic;
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
                        Coordinates.Add(new Coordinate { Lat = Double.Parse(coords[0]), Lng = Double.Parse(coords[1]) });
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