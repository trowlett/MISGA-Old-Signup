using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signups_Clear : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMarkedCount.Text = String.Format("There are {0:##,##0} marked records in the Signup (PlayersList) Database.", SignupList.MarkedEntryCount());
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        SignupList.PurgeMarkedEntries();
        lblPurgeCount.Text = "Entries purged = " + SignupList.EntriesPurged.ToString("##,##0");
    }
}