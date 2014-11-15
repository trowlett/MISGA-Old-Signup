using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MrMember
/// </summary>
public class MrMember
{
	public int pID { get; set; }
	public string name { get; set; }
	public string fname { get; set; }
	public string lname { get; set; }
	public string hcp { get; set; }
	public string memberNumber { get; set; }
	public int gender { get; set; }
	public string title { get; set; }
	public int active { get; set; }
	public DateTime hdate { get; set; }
    public int del { get; set; }

	public bool IsFemale()
	{
		return (this.gender == 2);
	}
    public bool IsDeleted()
    {
        return (this.del == 1);
    }
    public bool IsUpdated(DateTime dt)
    {
        return (this.hdate == dt) ;
    }

    public string IsHandicapCurrent()
    {
        MrTimeZone etz = new MrTimeZone();
        DateTime now = etz.eastTimeNow();
        int day = now.Day < 15 ? 1 : 15;
        DateTime begin = new DateTime(now.Year,now.Month,day,0,0,0);
        int lastDay = (day == 1) ? 14 : now.AddMonths(1).AddDays(-1).Day;
        DateTime end = new DateTime(now.Year,now.Month,lastDay,23,59,59);
        if ((this.hdate >= begin) && (this.hdate <= end))
        {
            return "current";
        }
        else {
            return "update";
        }
    }

/*        if (this.hdate == dt)
        {
            return "Green";
        }
        else
        {
            return "Red";
        } */
    }

