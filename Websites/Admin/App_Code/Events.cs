using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for Schedule
/// </summary>

[Table(Name="Events")]
public class Events
{
    [Column(IsPrimaryKey = true)]
    public string EventID;
    [Column]
    public DateTime Date;
    [Column]
    public string Type;
    [Column]
    public string HostID;
    [Column]
    public string Title;
    [Column]
    public string Cost;
    [Column]
    public string Time;
    [Column]
    public DateTime Deadline;
    [Column]
    public string SpecialRule;
    [Column]
    public string Guest;
    [Column]
    public int PlayerLimit;
    [Column]
    public string HostPhone;
    [Column]
    public DateTime PostDate;
}