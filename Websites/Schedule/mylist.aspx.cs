using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mylist : System.Web.UI.Page
{
    protected int selectedPlayerID = 0;
    protected int selectedPlayerIndex;
    protected int countOfEntries;
    protected string myLastVisit;
    protected string myID;
    protected string myIndex;

    public PrSchedule PlayerSchedule { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) ddlName.SelectedIndex = GetMyIndexCookie();
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        selectedPlayerIndex = ddlName.SelectedIndex;
        selectedPlayerID = Convert.ToInt32(ddlName.SelectedValue);
        SaveMyID(selectedPlayerIndex, selectedPlayerID);
        lblPID.Text = string.Format("Player ID = {0}", selectedPlayerID);
        countOfEntries = getPlayersEvents(rblEvents.SelectedValue);
        lblPID.Text = string.Format("Events Remaining for {1} = {0}", countOfEntries, ddlName.SelectedItem.Text);
    }
    protected int getPlayersEvents(string type)
    {
        this.PlayerScheduleRepeater.Visible = false;
        int cnt = 0;
        this.PlayerSchedule = PrSchedule.GetPlayerSchedule(selectedPlayerID, type);
        cnt = this.PlayerSchedule.FutureCount;
        int pcnt = this.PlayerSchedule.PrevCount;
        int count = cnt +  pcnt;
        this.PlayerScheduleRepeater.DataSource = new PrSchedule[] { this.PlayerSchedule };
        this.PlayerScheduleRepeater.DataBind();
        if (count > 0)
        {
            this.PlayerScheduleRepeater.Visible = true;
        }


        return cnt;
    }
    protected int GetMyIndexCookie()
    {
        int i = 0;
        if (Request.Cookies["MyUserInfo"] != null)
        {
            myIndex =
                Server.HtmlEncode(Request.Cookies["MyUserInfo"]["MyIndex"]);
           myID =
                Server.HtmlEncode(Request.Cookies["MyUserInfo"]["MyID"]);
            myLastVisit =
                Server.HtmlEncode(Request.Cookies["MyUserInfo"]["lastVisit"]);
            i = Convert.ToInt32(myIndex);
        }
        return i;
    }
 
    protected void SaveMyID(int myIndex, int myID)
    {
        HttpCookie aCookie = new HttpCookie("MyUserInfo");
        aCookie.Values["MyIndex"] = myIndex.ToString();
        aCookie.Values["MyID"] = myID.ToString();
        aCookie.Values["lastVisit"] = DateTime.Now.ToString();
        aCookie.Expires = DateTime.Now.AddDays(120);
        Response.Cookies.Add(aCookie);
    }

}