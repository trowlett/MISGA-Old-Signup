using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SysEvent
/// </summary>
public class SysEvent
{
    public string Id { get; set; }
    public DateTime EDate { get; set; }
    public string EType { get; set; }
    public string EHostID { get; set; }
    public string ETitle { get; set; }
    public string ECost { get; set; }
    public string ETime { get; set; }
    public DateTime EDeadline { get; set; }
    public string ESpecialRule { get; set; }
    public string EGuest { get; set; }
    public int EPlayerLimit { get; set; }
    public string EHostPhone { get; set; }
    public DateTime EPostDate { get; set; }

    public bool CanSignUp(DateTime lastDate)
    {
        return this.EType != "MISGA" && this.EDate <= lastDate;
    }
}