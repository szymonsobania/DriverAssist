using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;
using AuthWebApi.Models;
using WebGrease.Css.Extensions;

namespace AuthWebApi.Services
{
    public class PassagesRepository
    {
        private Dictionary<Guid, PassageData> _passages = new Dictionary<Guid, PassageData>();

        public List<Passage> GetPassages(string authToken)
        {
            bool tokenExists = AuthRepository.IsTokenExist(authToken);
            //#if DEBUG
            //            tokenExists = true;
            //#endif
            if (!tokenExists) return new List<Passage>();

            string login = AuthRepository.GetLogin(authToken);
            using (var context = new PP_testEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                var user = context.Uzytkownicies.First(u => u.email == login);
                IQueryable<Passage> result;
                if (user.administrator)
                {
                    result = context.Przejazdy_fs
                        .Select(p => new Passage
                        {
                            Date = p.data_przejazdu,
                            UserID = p.id_uzytk,
                            PassageGuid = p.id_przejazdu,
                            Car = context.Pojazdies.Where(poj => poj.id_pojazdu == p.id_pojazdu).Select(poj => poj.marka).FirstOrDefault(),
                            Time = context.Tagis.Where(tag => tag.id_przejazdu == p.id_przejazdu && tag.skrot == "Time")
                                .Select(tag => tag.komentarz).FirstOrDefault(),
                            Length = context.Tagis.Where(tag => tag.id_przejazdu == p.id_przejazdu && tag.skrot == "Dist")
                                .Select(tag => tag.komentarz).FirstOrDefault()
                        });
                }
                else
                {
                    result = context.Przejazdy_fs.Where(p => p.id_uzytk == user.id_uzytk)
                        .Select(p => new Passage
                        {
                            Date = p.data_przejazdu,
                            UserID = p.id_uzytk,
                            PassageGuid = p.id_przejazdu,
                            Car = context.Pojazdies.Where(poj => poj.id_pojazdu == p.id_pojazdu).Select(poj => poj.marka).FirstOrDefault(),
                            Time = context.Tagis.Where(tag => tag.id_przejazdu == p.id_przejazdu && tag.skrot == "Time")
                                .Select(tag => tag.komentarz).FirstOrDefault(),
                            Length = context.Tagis.Where(tag => tag.id_przejazdu == p.id_przejazdu && tag.skrot == "Dist")
                                .Select(tag => tag.komentarz).FirstOrDefault()
                        });
                }
                result.ForEach((p, i) => p.Passage_ID = i);
                return result.ToList();
            }
        }

        public PassageData GetStatistic(string authToken, string passageGuid)
        {
            bool tokenExists = AuthRepository.IsTokenExist(authToken);
            //#if DEBUG
            //            tokenExists = true;
            //#endif
            if (!tokenExists) return new PassageData();

            string login = AuthRepository.GetLogin(authToken);
            var guid = new Guid(passageGuid);
            if (_passages.ContainsKey(guid))
                return _passages[guid];
            _passages.Add(guid, new PassageData());
            using (var context = new PP_testEntities())
            {
                var user = context.Uzytkownicies.First(u => u.email == login);
                var przejazd = context.Przejazdy_fs.FirstOrDefault(p => p.id_przejazdu == guid);
                if (przejazd == null || przejazd.id_uzytk != user.id_uzytk) return new PassageData();

                string fileName = DateTime.Now.ToString("yyyyMMddHHmmtt") + ReadingsRepository.RandomString(5);
                string tmpFilePath = Path.Combine(fileName);
                File.WriteAllBytes(tmpFilePath, przejazd.dane_przejazdu);
                var result = _passages[guid];
                using (var con = new SQLiteConnection("Data Source=" + tmpFilePath))
                {
                    con.Open();

                    string cmdstr = "SELECT * FROM location_data ORDER BY TIMESTAMP";
                    using (var cmd = new SQLiteCommand(cmdstr, con))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            long timestamp;
                            double lat, lng;
                            while (rdr.Read())
                            {
                                timestamp = rdr.GetInt64(1);
                                lat = rdr.GetDouble(2);
                                lng = rdr.GetDouble(3);
                                result.LocationData.Add(new Tuple<long, double, double>(timestamp, lat, lng));
                            }
                        }
                    }

                    cmdstr = "SELECT * FROM accelerometer_data ORDER BY TIMESTAMP";
                    using (var cmd = new SQLiteCommand(cmdstr, con))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            long timestamp;
                            double x, y, z;
                            while (rdr.Read())
                            {
                                timestamp = rdr.GetInt64(1);
                                x = rdr.GetDouble(2);
                                y = rdr.GetDouble(3);
                                z = rdr.GetDouble(4);
                                result.AccData.Add(new Tuple<long, double, double, double>(timestamp, x, y, z));
                            }
                        }
                    }

                    cmdstr = "SELECT * FROM gyroscope_data ORDER BY TIMESTAMP";
                    using (var cmd = new SQLiteCommand(cmdstr, con))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            long timestamp;
                            double x, y, z;
                            while (rdr.Read())
                            {
                                timestamp = rdr.GetInt64(1);
                                x = rdr.GetDouble(2);
                                y = rdr.GetDouble(3);
                                z = rdr.GetDouble(4);
                                result.GyroData.Add(new Tuple<long, double, double, double>(timestamp, x, y, z));
                            }
                        }
                    }

                    cmdstr = "SELECT * FROM light_data ORDER BY TIMESTAMP";
                    using (var cmd = new SQLiteCommand(cmdstr, con))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            long timestamp;
                            double intensity;
                            while (rdr.Read())
                            {
                                timestamp = rdr.GetInt64(1);
                                intensity = rdr.GetDouble(2);
                                result.LightData.Add(new Tuple<long, double>(timestamp, intensity));
                            }
                        }
                    }
                }
                File.Delete(tmpFilePath);

                return result;
            }
        }
    }
}