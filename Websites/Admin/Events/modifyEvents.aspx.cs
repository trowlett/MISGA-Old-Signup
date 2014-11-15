using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events_modifyEvents : System.Web.UI.Page
{
    public MrSchedule Schedule { get; set; }
    public DateTime displayDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        SignupDates sd = new SignupDates();
        displayDate = sd.getDisplayDate();
//        displayDate = new DateTime(2013, 11, 2);
        load_schedule();

    }

    protected void load_schedule()
    {
        this.Schedule = MrSchedule.LoadFromDB();
        this.ScheduleRepeater.DataSource = new MrSchedule[] { this.Schedule };
        this.ScheduleRepeater.DataBind();
    }

}