using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_editmember : System.Web.UI.Page
{
    public int ActiveSignups = 0;
    MembersList ml {get; set;}
    private DateTime etzNow;
    private int playerID;
    int mlcount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        etzNow = new MrTimeZone().eastTimeNow();
        lblNow.Text = etzNow.ToString();
        if (!IsPostBack)
        {
            mlcount = ShowMembers();
            lblMemberCount.Text = string.Format("{0} Members Displayed<br />", mlcount);
            Label3.Text = @"Select the Edit button of the member to edit.";
            Label2.Text = "";
            UpdatePanel1.Visible = false;
        }
    }
    protected int ShowMembers()
    {
        this.ml= MembersList.LoadMembers();
        this.MainMemberRepeater.DataSource = new MembersList[] { this.ml };
        this.MainMemberRepeater.DataBind();
        return MembersList.Count;
    }
    protected void Member_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        Label2.Text = "The " + ((Button)e.CommandSource).Text + " button has just been clicked; <br />";
        playerID = Convert.ToInt32(e.CommandArgument);
        Label3.Text = string.Format(@"Player ID ""{0}"" has been selected.", playerID);
//        lblKey.Text = pkey.Trim();
//        lblChangeDate.Text = etzNow.ToString();
        //
        // write method to get all member information and load UpdatePanel1 controls with data
        //
        ActiveSignups = SignupList.CountPlayersActiveSignupEntries(playerID);
        Session["ActiveSignups"] = ActiveSignups.ToString();
        Session["PlayerID"] = playerID.ToString();
        if (ActiveSignups > 0) btnDelete.Enabled = false;
        DisplayMemberInfo(playerID);
        DisplayPanel.Visible = false;
        UpdatePanel1.Visible = true;

    }
    protected void DisplayMemberInfo(int pid)
    {
        lblMemberCount.Visible = false;
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var memb = db.Players.FirstOrDefault(p => p.PlayerID == pid);
        if (memb == null)
        {
            lblPIDMissing.Visible = true;
            Table1.Visible = false;
            return;
        }
        tbPid.Text = memb.PlayerID.ToString();
        tbMName.Text = memb.Name;
        tbMid.Text = memb.MemberID;
        tbHcp.Text = memb.Hcp;
        tbHDate.Text = memb.HDate.ToShortDateString();
        tbTitle.Text = memb.Title;
        ddlGender.SelectedIndex = memb.Sex - 1;
        tbLName.Text = memb.LName;
        tbFName.Text = memb.FName;
        ddlDel.SelectedIndex = memb.Delete;


//        if (prm == null) return "";
//        return prm.Value;

    }
    private bool IsInputValid(MrMember memb)
    {
        lblErrorMsg.Text = "";
        bool ok = true;
        memb.name = tbMName.Text;
        memb.memberNumber = tbMid.Text.Trim();
        memb.title = tbTitle.Text.Trim();
        memb.lname = tbLName.Text.Trim();
        memb.fname = tbFName.Text.Trim();
        memb.gender = ddlGender.SelectedIndex + 1;
        memb.hcp = tbHcp.Text.Trim();
        memb.hdate = Convert.ToDateTime(tbHDate.Text);
        memb.pID = Convert.ToInt32(tbPid.Text);
        if (ddlDel.SelectedIndex == 1)
        {
            // Want to delete Member Record
            // make sure there are no active signup records
            //
            if (ActiveSignups > 0)
            {
                // have entries for this player in signup database
                // string verb = (ActiveSignups == 1) ? "is" : "are";
                lblErrorMsg.Text = string.Format("Cannot mark member for deletion because there {0} {1} active signup entires",(ActiveSignups == 1)?"is":"are",ActiveSignups);
                ddlDel.SelectedIndex = 0;           // reset drop down list
                ok = false;
            }
            else
            {
                ok = true;
            }
        }
        return ok;
    }
    private void savePlayerInfo(MrMember memb)
    {
        string action = "";
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var item = db.Players.FirstOrDefault(p => p.PlayerID == memb.pID);
        if (item != null)
        {
            item.Name = memb.name;
            item.LName = memb.lname;
            item.FName = memb.fname;
            item.Hcp = memb.hcp;
            item.HDate = memb.hdate;
            item.MemberID = memb.memberNumber;
            item.Sex = memb.gender;
            item.Title = memb.title;
            item.Delete = memb.del;
            action = "updated";

        }
        else
        {
            Players p = new Players()
            {
                PlayerID = memb.pID,
                Name = memb.name,
                LName = memb.lname,
                FName = memb.fname,
                Hcp = memb.hcp,
                HDate = memb.hdate,
                MemberID = memb.memberNumber,
                Sex = memb.gender,
                Title = memb.title,
                Delete = memb.del
            };
            db.Players.InsertOnSubmit(p);
            action = "inserted";
        }
        db.SubmitChanges();
        Label2.Text = string.Format("{0} {1} {2} in database.", memb.pID, memb.name, action);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MrMember mInfo = new MrMember();
        ActiveSignups = Convert.ToInt32(Session["ActiveSignups"]);
        playerID = Convert.ToInt32(Session["PlayerID"]);
        if (IsInputValid(mInfo))
        {
            savePlayerInfo(mInfo);
            UpdatePanel1.Visible = false;
            DisplayPanel.Visible = true;
            mlcount = ShowMembers();
            lblMemberCount.Visible = false;
        }
        else
        {
            lblErrorMsg.Visible = true;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        MrMember mInfo = new MrMember();
        ActiveSignups = Convert.ToInt32(Session["ActiveSignups"]);
        playerID = Convert.ToInt32(Session["PlayerID"]);
        if (IsInputValid(mInfo))
        {
            deletePlayerInfo(mInfo);
            UpdatePanel1.Visible = false;
            DisplayPanel.Visible = true;
            Response.Redirect("editmember.aspx", true);
        }
        else{
            lblErrorMsg.Visible = true;
        }
//        Server.Transfer("editmember.aspx");
    }

    protected void deletePlayerInfo(MrMember memb)
    {
        ActiveSignups = Convert.ToInt32(Session["ActiveSignups"]);
        playerID = Convert.ToInt32(Session["PlayerID"]);

        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var item = db.Players.FirstOrDefault(p => p.PlayerID == memb.pID);
        if (item != null)
        {
            db.Players.DeleteOnSubmit(item);
            var se = 
                from pl in db.PlayersList
                where ((pl.PlayerID == memb.pID) && (pl.Marked == 1))
                select pl;
            db.PlayersList.DeleteAllOnSubmit(se);
            db.SubmitChanges();
            Label2.Text = string.Format("Player {0} {1} Deleted.", item.PlayerID, item.Name);
        }
        else
        {
            lblErrorMsg.Text = string.Format("Player {0} {1} NOT deleted because player does not exist in database!", memb.pID, memb.name);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ActiveSignups = Convert.ToInt32(Session["ActiveSignups"]);
        playerID = Convert.ToInt32(Session["PlayerID"]);
        Server.Transfer("editmember.aspx");
    }
}