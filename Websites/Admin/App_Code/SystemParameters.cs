using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SystemParameters
/// </summary>
public class SystemParameters
{
    public const string ScheduleDate = "ScheduleDate";

	public SystemParameters()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void Update(string key, string value)
    {
        MrTimeZone etz = new MrTimeZone();
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        SysParams entry = db.SysParams.FirstOrDefault(p => p.Key.Trim() == key);
        if (entry != null)
        {
            entry.Value = value;
            entry.ChangeDate = etz.eastTimeNow();
        }
        else
        {
            SysParams sp = new SysParams
            {
                Key = key,
                Value = value,
                ChangeDate = etz.eastTimeNow()
            };
            db.SysParams.InsertOnSubmit(sp);
        }
        db.SubmitChanges();
/*)
        MRParams entry = db.MRParams.FirstOrDefault(p => p.Key.Trim() == keyLastDate);
        if (entry != null)
        {
            LastDate = Convert.ToDateTime((string)entry.Value);
            changeDate = (DateTime)entry.ChangeDate;
        }
        else
        {
            int curyear = etz.eastTimeNow().Year;
            LastDate = new DateTime(curyear, 3, 1, 23, 59, 59, System.Globalization.CultureInfo.InvariantCulture.Calendar);
            changeDate = etz.eastTimeNow();
            MRParams newParm = new MRParams();
            newParm.Key = keyLastDate;
            newParm.Value = LastDate.ToString();
            newParm.ChangeDate = changeDate;
            db.MRParams.InsertOnSubmit(newParm);
            db.SubmitChanges();
        }
        lblDateLastChanged.Text = "Date Last Changed: " + changeDate.ToString();
*/
    }
}