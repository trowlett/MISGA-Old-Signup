using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Signups_purge_prior_year : System.Web.UI.Page
{
    public DateTime currDate;
    public DateTime currYearBegin;
    public int purgedEntries;
    public int currYear;

    protected void Page_Load(object sender, EventArgs e)
    {
        MrTimeZone etz = new MrTimeZone();
        currDate = etz.eastTimeNow();
        currYearBegin = new DateTime (currDate.Year, 1, 1, 0, 0, 1);
        currYear = currDate.Year;
        lblHdr1.Text = string.Format("Purging all SIgnup Entries prior to {0}", currYear);

    }
    protected int purgeEntries()
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        int countOfEntries;
        countOfEntries = 0;

        var sup = from pl in db.PlayersList
                  orderby pl.EventID
                  select pl;

        foreach (PlayersList item in sup)
        {
            int y = Convert.ToInt32(item.EventID.Substring(3,2))+2000;
            if (y < currYear)
            {
                db.PlayersList.DeleteOnSubmit(item);
                countOfEntries++;
            }
        }
        db.SubmitChanges();
        return countOfEntries;
    }

    protected void btnDoIt_Click(object sender, EventArgs e)
    {

        lblResult.Text = string.Format("{0} Signup Entries Deleted", purgeEntries());
        btnDoIt.Visible = false;
        lblResult.Visible = true;
        lblErrorMsg.Visible = false;
    }
}