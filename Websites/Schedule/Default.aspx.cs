using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Signup_Default : System.Web.UI.Page
{
	public MrSchedule Schedule { get; set; }
    public DateTime displayDate;
    public int signupOffset { get; set; }
    private MrTimeZone ETZ;
    private DateTime sysToday;
    public string PhysicalYear {get; set;}
    public DateTime BeginActive { get; set; }
    public DateTime EndActive { get; set; }
    private DateTime nullDate = new DateTime(2010, 1, 1, 0, 0, 1);
    public DateTime scheduleDate { get; set; }

	protected void load_schedule()
	{

		this.Schedule = MrSchedule.LoadFromDB();
		this.ScheduleRepeater.DataSource = new MrSchedule[] { this.Schedule };
		this.ScheduleRepeater.DataBind();
	}

    protected DateTime GetParamDateTimeValue(String key)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var prm = db.MRParams.FirstOrDefault(p => p.Key == key);
        if (prm == null) return nullDate;
        DateTime pd = Convert.ToDateTime(prm.Value);
        return pd;
    }


	protected void Page_Load(object sender, EventArgs e)
	{
		litOrg3.Text = ConfigurationManager.AppSettings["Org"];
        Page.Title = ConfigurationManager.AppSettings["Org"] + " Schedule";
        string offset = ConfigurationManager.AppSettings["SignupOffset"];
        ETZ = new MrTimeZone();
        sysToday = ETZ.eastTimeNow();
        int pyear = sysToday.Year;
        DateTime tempDate = new DateTime(pyear, 11, 15, 0, 0, 1);
        if (tempDate < sysToday) pyear++;
        DateTime Sstart = GetParamDateTimeValue("FirstDate");
        DateTime Esignup = GetParamDateTimeValue("LastDate");
        if (Sstart == nullDate)  {BeginActive = new DateTime(pyear, 2, 15, 0, 0, 1); }
        else {
            BeginActive = Sstart;
        }
        if (Esignup == nullDate) { EndActive = new DateTime(pyear, 11, 15, 0, 0, 1); }
        else
        {
            EndActive = Esignup;
        }
//        this.PhysicalYear = pyear.ToString();
        if (sysToday < BeginActive)
            lblBeginActive.Visible = true;
        signupOffset = 0;
        if (offset != null)
            if (offset.Trim() != "")
                signupOffset = Convert.ToInt32(offset);
        
        SignupDates sd = new SignupDates();
        this.displayDate = sd.getDisplayDate();
        this.load_schedule();
        this.PhysicalYear = this.Schedule.ScheduleYear.ToString();

	}
}