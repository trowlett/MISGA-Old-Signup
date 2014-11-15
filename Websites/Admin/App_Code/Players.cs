using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Data.SqlTypes;

[Table(Name = "Players")]
public class Players
{
    [Column(IsPrimaryKey = true)]
    public int PlayerID;
    [Column]
    public string Name;
    [Column]
    public string LName;
    [Column]
    public string FName;
    [Column]
    public string Hcp;
    [Column]
    public string MemberID;
    [Column]
    public int Sex;
    [Column]
    public string Title;
    [Column]
    public DateTime HDate;
    [Column]
    public int Delete;
}