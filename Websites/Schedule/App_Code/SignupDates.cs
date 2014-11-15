using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for SignupDates
/// </summary>
public class SignupDates
{
    private DateTime sysToday;
    private DateTime LastDate;
    private DateTime changeDate;
    private DateTime displayDate;
    private const string keyLastDate = "LastDate";
    private const string keyDaysToShow = "DaysToShow";
    private const int defaultDisplayDays = 45;
    private int daysToShow = 0;

    public DateTime getDisplayDate()
    {
        MrTimeZone ETZ = new MrTimeZone();
        sysToday = ETZ.eastTimeNow();
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        MRParams entry = db.MRParams.FirstOrDefault(p => p.Key == keyLastDate);
        if (entry != null)
        {
            LastDate = Convert.ToDateTime((string)entry.Value);
            changeDate = (DateTime)entry.ChangeDate;
        }
        else
        {
            LastDate = new DateTime(2012, 4, 12, 23, 59, 59, System.Globalization.CultureInfo.InvariantCulture.Calendar);
            changeDate = ETZ.eastTimeNow();
            MRParams newParm = new MRParams();
            newParm.Key = keyLastDate;
            newParm.Value = LastDate.ToString();
            newParm.ChangeDate = changeDate;
            db.MRParams.InsertOnSubmit(newParm);
            db.SubmitChanges();
        }
        MRParams edts = db.MRParams.FirstOrDefault(p => p.Key == keyDaysToShow);
        daysToShow = defaultDisplayDays;
        if (edts != null)
        {
            daysToShow = Convert.ToInt32((string)edts.Value);
        }
        else
        {
            changeDate = ETZ.eastTimeNow();
            MRParams newParm = new MRParams();
            newParm.Key = keyDaysToShow;
            newParm.Value = daysToShow.ToString();
            newParm.ChangeDate = changeDate;
            db.MRParams.InsertOnSubmit(newParm);
            db.SubmitChanges();
        }
        displayDate = ETZ.eastTimeNow().AddDays(daysToShow);
        if (LastDate < displayDate)
        {
            displayDate = LastDate;
        }
        return displayDate;

    } 

    public SignupDates()
    {
        //
        // TODO: Add constructor logic here
        //
    }


}