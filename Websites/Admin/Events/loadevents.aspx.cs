using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events_loadevents : System.Web.UI.Page
{
    public DateTime LastDate { get; set; }
    public MrSchedule Schedule { get; set; }
    private string filename;
    private int countOfEvents;
    public string scheduleDate;


    protected void load_schedule()
    {
        this.Schedule = MrSchedule.LoadFromCsv(filename);
        this.scheduleDate = this.Schedule.CreateTime.ToLongDateString();
        countOfEvents = this.Schedule.Events.Count;
        //        this.GridView1.DataSource = new MrSchedule[] { this.Schedule };
        //        this.GridView1.DataBind();
        //        this.ScheduleRepeater.DataSource = new MrSchedule[] { this.Events };
        //        this.ScheduleRepeater.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        filename = Server.MapPath("~\\App_Data\\schedule.txt");
        lblFileName.Text = string.Format("From FileName = {0}", filename);
        if (IsPostBack)
        {
            //  MrResources mr = new MrResources();
            //  path = Server.MapPath(mr.Root);
            BtnLoadText.Enabled = true;
            lblDbLoadStatus.Text = "";
        }

    }

    protected void BtnLoadText_Click(object sender, EventArgs e)
    {
        load_schedule();
        lblDbLoadStatus.Text = string.Format("{0} Events now in the database.  File date:  {1}",countOfEvents, scheduleDate);
        BtnLoadText.Enabled = false;
        DataBind();
        SystemParameters.Update(SystemParameters.ScheduleDate, scheduleDate);
    }
}