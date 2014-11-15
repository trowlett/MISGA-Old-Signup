using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for Handicaps
/// </summary>
public class Handicap
{
        public string ID { get; set; }
        public string Name { get; set;}
        public string Index { get; set; }
        public DateTime Date { get; set; }

        public Handicap(string id, string name, string index, DateTime date)
        {
            ID = id;
            Name = name;
            Index = index;
            Date = date;
        }
        public Handicap() { }
        private static readonly char[] delimiterChars = { ',' };
        private static string _filename;
        private static DateTime _hcpDate;

        public static List<Handicap> LoadHandicaps(string fileName, DateTime hcpDate)
        {
            _filename = fileName;
            _hcpDate = hcpDate;

            List<Handicap> target = new List<Handicap>();
//            _hcpDate = System.IO.File.GetLastWriteTime(fileName);

            string[] lines = System.IO.File.ReadAllLines(_filename);
            string prev = "";
            foreach (String line in lines)
            {
                if (line.Length > 0)
                {
                    string[] fields = line.Split(delimiterChars);
                    if (fields.Length > 7)
                    {
                        if (fields[4] != prev)
                        {
                            if (fields[8].Trim() != "CourseName1")
                            {
                                prev = fields[4];
                                Handicap e = new Handicap()
                                {
                                    ID = fields[4],
                                    Name = fields[6] + ", " + fields[7],
                                    Date = _hcpDate
                                };
 //                               e.Index = "NONE";
                                e.Index = ToHcp(fields[8].Trim());

                                target.Add(e);
                            }
                        }
                    }
                }
            }
            return target;

        }

        private static string ToHcp(string h)
        {
            string rst = "";
            foreach (char x in h)
            {
                if ((x == ' ') || (x == 65533))
                {
                }
                else
                {
                    rst += x;
                }
            }
            return rst;
        }


        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}",ID, Name, Index, Date.ToString());
        }
    }
 