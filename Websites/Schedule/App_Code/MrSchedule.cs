using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for MrSchedule
/// </summary>
public class MrSchedule
{
    private static readonly char[] delimiterChars = { ';' };

    private Collection<SysEvent> events = new Collection<SysEvent>();

    public Collection<SysEvent> Events
    {
        get
        {
            return this.events;
        }
    }

    public string FileName { get; private set; }

    public DateTime CreateTime { get; private set; }

    public int ScheduleYear { get; set; }

    public static MrSchedule LoadFromDB()
    {
        MrSchedule target = new MrSchedule();

        MrTimeZone mtz = new MrTimeZone();
        DateTime etzNow = mtz.eastTimeNow();
        DateTime yesterday = etzNow.Subtract(TimeSpan.FromDays(1));
        
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        var qevents = 
            from ev in db.Events
            where (ev.Date >= yesterday)
            orderby ev.EventID
            select ev;

        foreach (Events item in qevents)
        {
            SysEvent e = new SysEvent()
            {
                Id = item.EventID.Trim(),
                EDate = item.Date,
                EType = item.Type.Trim(),
                ETitle = item.Title,
                ECost = item.Cost.Trim(),
                ETime = item.Date.ToString("h:mm t").ToLower(),
                EDeadline = item.Deadline,
                EPlayerLimit = item.PlayerLimit,
                EHostPhone = item.HostPhone,
                ESpecialRule = item.SpecialRule.Trim(),
                EGuest = item.Guest.Trim(),
                EHostID = item.HostID,
                EPostDate = item.PostDate
            };
            if (item.PostDate == null) e.EPostDate = DateTime.MinValue;
            target.ScheduleYear = e.EDate.Year;
            target.Events.Add(e);
        }

        return target;
    }
}