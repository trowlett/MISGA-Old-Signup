using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signups_Carpool : System.Web.UI.Page
{
    public SignupList sl { get; set; }
    public int DBCount { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        ShowPlayersListDB();
        if (DBCount == 0) { Panel1.Visible = false; } else { Panel1.Visible = true; }

    }

    public void ShowPlayersListDB()
    {
        this.sl = SignupList.LoadPlayersListDB();
        this.PlayersListRepeater.DataSource = new SignupList[] { this.sl };
        this.PlayersListRepeater.DataBind();
//        int dbCount = this.sl.Entries.Count;
//        DBCount = dbCount==0 ? "No": dbCount.ToString();
        DBCount = this.sl.Entries.Count;
    }

    protected void lbDeleteAll_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (SignupEntry se in this.sl.Entries)
        {
            se.SDelete = true;
            i++;
        }
        int EntriesDeleted = DeleteDBEntries();
        Server.Transfer("signupDB.aspx");

    }
    protected int DeleteDBEntries()
    {
        // delete from SignUp database entries in SL that are marked to be deleted
        int n = 0;
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        foreach (SignupEntry se in this.sl.Entries)
        {
            if (se.SDelete)
            {
                DateTime timestamp = se.STDate;
                string eid = se.SeventId;
                int pid = se.SPlayerID;
                var oldentry = db.PlayersList.Single(pe => pe.TransDate == timestamp && pe.EventID == eid && pe.PlayerID == pid);
                db.PlayersList.DeleteOnSubmit(oldentry);
                n++;
            }
        }
        if (n > 0)
        {
            db.SubmitChanges();
        }
        return n;

    }
    protected void Button_Click(object sender, EventArgs e)
    {
        ShowPlayersListDB();
    }
    protected void CheckBox1_OnCheckedChanged(object sender, EventArgs e)
    {
        foreach (SignupEntry se in this.sl.Entries)
        {
        }
     
    }

}