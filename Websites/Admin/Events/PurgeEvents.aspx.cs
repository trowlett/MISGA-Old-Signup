using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events_PurgeEvents : System.Web.UI.Page
{
    public MrTimeZone today;
    MrSchedule dbEvents { get; set; }
    public int choice;
    public int countOfEvents;
    public DateTime firstDate;
    public DateTime lastDate;
    public bool purgeAll;
    string todayText;

    protected void Page_Load(object sender, EventArgs e)
    {
        LookAtEventsDB();
        if (countOfEvents == 0)
            {
                DBInfo.Text = string.Format("Events Database is Empty.");
            }
            else
            {
                DBInfo.Text = string.Format("Events Database has {0} records ranging from:  {1:D}  to  {2:D}.", countOfEvents, firstDate, lastDate);
            }
        if (!IsPostBack)
        {
            today = new MrTimeZone();
            todayText = today.eastTimeNow().ToString("d");
            tbDate.Text = todayText;
            tbDate.Enabled = false;
            lblDelete.Visible = false;
            purgeAll = true;
            Panel1.Visible = false;
        };

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (RadioButtonList1.SelectedIndex == 0)
        {
            Panel1.Visible = false;
        }
        if (RadioButtonList1.SelectedIndex == 1)
        {
            Panel1.Visible = true;
            tbDate.Enabled = true;
            today = new MrTimeZone();
            todayText = today.eastTimeNow().ToString("d");
            tbDate.Text = todayText;
        }
    }
    protected void btnDoIt_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            ChoiceLbl.Text = "Purging all Events";
            purgeAll = true;
        }
        if (RadioButtonList1.SelectedIndex == 1)
        {
            Panel1.Visible = true;
            ChoiceLbl.Text = string.Format("Events prior to {0:D} will be deleted.", today);

            lastDate = Convert.ToDateTime((string)tbDate.Text);
            lastDate = lastDate.AddDays(1).AddSeconds(-1);
            firstDate = new DateTime(2011, 1, 1, 0, 0,1);
        }
        ChoiceLbl.Visible = true;
        if (firstDate > lastDate)
        {
            firstDate = lastDate;
            lastDate = lastDate.AddHours(7);
        }
        if (countOfEvents > 0)
        {

            int purgedEvents = PurgeEvents(firstDate, lastDate);
            ChoiceLbl.Text = string.Format("{0} Events purged from database.", purgedEvents);
        }
        else
        {
            ChoiceLbl.Text = string.Format("No Events purged because the Events database is empty.");
        }

    }

    protected int PurgeEvents(DateTime StartDate, DateTime EndDate)
    {
        int result = 0;
        DateTime vDate;
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var query = from q in db.Events
                    where ((q.Date >= StartDate) && (q.Date <= EndDate))
                    select q;
        foreach (var item in query)
        {
            vDate = item.Date;
            db.Events.DeleteOnSubmit(item);
            result++;
        }
        db.SubmitChanges();
        return result;
    }

    protected void LookAtEventsDB()
    {
        countOfEvents = 0;

        this.dbEvents = MrSchedule.LoadFromDB();
        countOfEvents = dbEvents.Events.Count;
        if (countOfEvents > 0)
        {
            firstDate = dbEvents.Events[0].EDate;
            lastDate = dbEvents.Events[countOfEvents-1].EDate;
        }
    }

    protected void CBAll_CheckedChanged(object sender, EventArgs e)
    {         
        purgeAll = CBAll.Checked;
        if (purgeAll)
        {
            lblDelete.Text = string.Format("All {0} Events will be deleted", countOfEvents);
        }
        else
        {
            lblDelete.Text = string.Format("Less than all Events will be deleted");
        }
        lblDelete.Visible = true;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
     }


}