using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for PlayersList
/// </summary>
[Table(Name = "PlayersList")]
public class PlayersList
{
    [Column(IsPrimaryKey = true)]
    public DateTime TransDate;
    [Column]
    public String EventID;
    [Column]
    public int PlayerID;
    [Column]
    public string Action;
    [Column]
    public string Carpool;
    [Column]
    public int Marked;
    [Column]
    public string SpecialRule;
    [Column]
    public int GuestID;
}