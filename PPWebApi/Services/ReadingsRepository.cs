using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AuthWebApi.Models;
using System.IO.Compression;
using Ionic.Zip;
using System.Data.SQLite;
using System.Text;

namespace AuthWebApi.Services
{
    public class ReadingsRepository
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden

        public const int EarthRadiusInMeters = 6371000;

        private static double Degrees2Radius(double degrees)
        {
            return Math.PI * degrees / 180;
        }

        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public void AddTagsToRide(SQLiteConnection con, PP_testEntities context, Przejazdy_fs przejazd)
        {
            long time = 0;
            double vmax = 0;
            string ride = "SELECT START_TIMESTAMP, STOP_TIMESTAMP FROM ride";
            using (SQLiteCommand cmd = new SQLiteCommand(ride, con))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        long stop = rdr.GetInt32(1);
                        long start = rdr.GetInt32(0);
                        time = stop - start;
                    }
                }
            }
            double lastLatitude;
            double lastLongitude;
            long lastTimestamp;
            string ord = "SELECT * FROM location_data ORDER BY TIMESTAMP";

            double sec;
            double minutes;
            double speed;
            double seconds;
            double sum = 0.0;
            using (SQLiteCommand cmd = new SQLiteCommand(ord, con))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    lastTimestamp = rdr.GetInt64(1);
                    lastLatitude = Degrees2Radius(rdr.GetDouble(2));
                    lastLongitude = Degrees2Radius(rdr.GetDouble(3));
                    
                    while (rdr.Read())
                    {
                        long timestamp = rdr.GetInt32(1);
                        double latitude = Degrees2Radius(rdr.GetDouble(2));
                        double longitude = Degrees2Radius(rdr.GetDouble(3));
                        long diffTime = timestamp - lastTimestamp;
                        double diffLat = latitude - lastLatitude;
                        double diffLng = longitude - lastLongitude;

                        lastTimestamp = timestamp;
                        lastLatitude = latitude;
                        lastLongitude = longitude;

                        double a = Math.Pow(Math.Sin(diffLat / 2), 2) + Math.Cos(lastLatitude) * Math.Cos(latitude) * Math.Pow(Math.Sin(diffLng / 2), 2);
                        double b = 2 * Math.Asin(Math.Sqrt(a));
                        double dist = Math.Round(EarthRadiusInMeters * b, 2);
                        double v = (dist / 1000) / ((double)diffTime / 1000 / 60 / 60);
                        if (v > vmax)
                            vmax = v;
                        //Console.WriteLine(string.Format("{0}m - {1:0.00} km/h - {2}ms", dist, v, diffTime));
                        sum += dist;
                    }
                    sec = (double)time / 1000;
                    minutes = sec / 60;
                    speed = (sum / 1000) / (minutes / 60);
                    seconds = (minutes - (int)minutes) * 60;
                    //Console.WriteLine(string.Format("Distance: {0:0.000}km, Time: {1:0}h {2}min {3:0}s, Avg speed: {4:0.00}km/h, Max speed: {5:0.00}km/h",
                    //    (sum / 1000), (int)(minutes / 60), (int)minutes, seconds, speed, vmax));
                }
            }
            var tagVMax = new Tagi();
            tagVMax.id_przejazdu = przejazd.id_przejazdu;
            tagVMax.skrot = Static.Tags.TagCode.MaxSpeed;
            tagVMax.komentarz = string.Format("{0:0.00}km/h", vmax);
            context.Tagis.Add(tagVMax);

            var tagAvgSpeed = new Tagi();
            tagAvgSpeed.id_przejazdu = przejazd.id_przejazdu;
            tagAvgSpeed.skrot = Static.Tags.TagCode.AverageSpeed;
            tagAvgSpeed.komentarz = string.Format("{0:0.00}km/h", speed);
            context.Tagis.Add(tagAvgSpeed);

            var tagDist = new Tagi();
            tagDist.id_przejazdu = przejazd.id_przejazdu;
            tagDist.skrot = Static.Tags.TagCode.Distance;
            tagDist.komentarz = string.Format("{0:0.000}km", sum/1000);
            context.Tagis.Add(tagDist);

            var tagTime = new Tagi();
            tagTime.id_przejazdu = przejazd.id_przejazdu;
            tagTime.skrot = Static.Tags.TagCode.TotalTime;
            tagTime.komentarz = string.Format("{0:0}h {1}min {2:0}s", (int)(minutes / 60), (int)minutes, seconds);
            context.Tagis.Add(tagTime);
        }

        public Response Read(Reading reading)
        {
            bool tokenExist = AuthRepository.IsTokenExist(reading.AuthToken);
#if DEBUG
            tokenExist = true;
#endif
            if (tokenExist)
            {
                try
                {
                    List<int> ridesIds = new List<int>();
                    var file = Convert.FromBase64String(reading.Content);//System.Text.Encoding.Unicode.GetBytes(reading.Content);
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmtt") + RandomString(5);
                    string tmpFilePath = Path.Combine(Path.GetTempPath(), fileName);
                    File.WriteAllBytes(tmpFilePath, file);
                    using (SQLiteConnection con = new SQLiteConnection("Data Source="+tmpFilePath))
                    {
                        con.Open();
                        string ride = "SELECT _id FROM ride";

                        using (SQLiteCommand cmd = new SQLiteCommand(ride, con))
                        {
                            using (SQLiteDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    ridesIds.Add(rdr.GetInt32(0));
                                }
                            }
                        }
                        using (PP_testEntities context = new PP_testEntities())
                        {
                            if (ridesIds.Count == 1)
                            {
                                var przejazd = new Przejazdy_fs();
                                przejazd.dane_przejazdu = file;
                                var userLogins = AuthRepository.GetLogin(reading.AuthToken);
#if DEBUG
                                var user = (from u in context.Uzytkownicies select u).FirstOrDefault();
#else
                                    var user = (from u in context.Uzytkownicies where u.email == userLogins select u).FirstOrDefault();
#endif
                                przejazd.Uzytkownicy = user;
                                przejazd.id_przejazdu = Guid.NewGuid();
                                context.Przejazdy_fs.Add(przejazd);
                                AddTagsToRide(con, context, przejazd);
                            }

                            else if (ridesIds.Count > 1)
                            {
                                foreach (var id in ridesIds)
                                {
                                    string fname = DateTime.Now.ToString("yyyyMMddHHmmtt") + RandomString(5);
                                    string path = Path.Combine(Path.GetTempPath(), fileName);
                                    File.WriteAllBytes(path, file);
                                    var przejazd = new Przejazdy_fs();
                                    using (SQLiteConnection con2 = new SQLiteConnection("Data Source=" + path))
                                    {
                                        con2.Open();
                                        string query = "DELETE FROM location_data where RIDE_ID <> " + id.ToString();
                                        SQLiteCommand cmd = new SQLiteCommand(query, con2);
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();

                                        query = "DELETE FROM accelerometer_data where RIDE_ID <> " + id.ToString();
                                        cmd = new SQLiteCommand(query, con2);
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();

                                        query = "DELETE FROM gyroscope_data where RIDE_ID <> " + id.ToString();
                                        cmd = new SQLiteCommand(query, con2);
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();

                                        query = "DELETE FROM light_data where RIDE_ID <> " + id.ToString();
                                        cmd = new SQLiteCommand(query, con2);
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();

                                        query = "DELETE FROM ride where _id <> " + id.ToString();
                                        cmd = new SQLiteCommand(query, con2);
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();

                                        var userLogins = AuthRepository.GetLogin(reading.AuthToken);
#if DEBUG
                                        var user = (from u in context.Uzytkownicies select u).FirstOrDefault();
#else
                                    var user = (from u in context.Uzytkownicies where u.email == userLogins select u).FirstOrDefault();
#endif
                                        przejazd.Uzytkownicy = user;
                                        przejazd.id_przejazdu = Guid.NewGuid();                                   
                                        przejazd.dane_przejazdu = Encoding.ASCII.GetBytes(System.DateTime.Now.ToString());
                                        context.Przejazdy_fs.Add(przejazd);
                                        AddTagsToRide(con2, context, przejazd);
                                        con2.Close();
                                        GC.Collect();
                                        GC.WaitForPendingFinalizers();
                                    }
                                    przejazd.dane_przejazdu = File.ReadAllBytes(path);
                                }                              
                            }
                            context.SaveChanges();
                        }
                    }
                   
                    File.Delete(tmpFilePath);
                    return new Response() { Result = "OK" };
                }
                catch (Exception e)
                {
                    return new Response() { Result = "Error", Reason = e.Message };
                }
            }
            else
            {
                return new Response() {Result = "Error", Reason = "Zły token sesji"};
            }
        }
    }
}