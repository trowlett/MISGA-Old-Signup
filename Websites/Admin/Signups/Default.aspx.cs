using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signups_Default : System.Web.UI.Page
{
    public SignupList sl { get; set; }

    String evID = "";
    string evDate = "";
    string evType = "";
    string evTitle = "";
    string evTime = "";
    int evPlayerLimit = 0;
    private bool haveGuestEvent = false;
    private int playerCount = 0;

    protected void ShowControls(bool show)
    {
        bool tf = show;
 //       tf = true;
        lblEvent.Visible = tf;
        lblCount.Visible = tf;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList1.Items.Clear();
            LoadButton.Enabled = loadDDL();

            ShowControls(false);
            emailSuccess.Visible = false;
            lbContinue.Visible = false;
        }
    }

    protected void ShowEvent(string id)
    {

        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        var query =
            (from ev in db.Events
             where (ev.EventID == id)
             select new { ev.EventID, ev.Date, ev.Type, ev.Title, ev.Time, ev.PlayerLimit });
        foreach (var item in query)
        {
            evID = item.EventID;
            evDate = item.Date.ToString("ddd, MMM d");
            evType = item.Type;
            evTitle = item.Title;
            evTime = item.Time;
            evPlayerLimit = item.PlayerLimit;

        }
            lblEvent.Text = "Event:  "+evID +" -  "+evType+" - "+evDate+" - "+evTitle+" -  Tee Time at "+evTime;
    }

    protected void LoadEmailBody(string EventID)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        var mixer = db.Events.FirstOrDefault(m => m.EventID == EventID);
        string hostClub = "";
        if (mixer.Type.Trim() == "Home") 
        {
            hostClub = ConfigurationManager.AppSettings["Club"];
        } 
        else 
        {
            hostClub = mixer.Title;
        }

        sourceTo.Text = ConfigurationManager.AppSettings["RepEmail"];

        sourceBodyText.Text = "List of " + playerCount.ToString() +" "+ ConfigurationManager.AppSettings["Org"];
        sourceBodyText.Text += " players and their handicap indexes to play in the mixer on ";
        sourceBodyText.Text += evDate +" at " + hostClub + Environment.NewLine + Environment.NewLine;
        sourceBodyText.Text += "Players marked with an [F] are female." + Environment.NewLine + Environment.NewLine;
       


        string spcRule = "";
        if (mixer.SpecialRule != null)
        {
            spcRule = mixer.SpecialRule.Trim();
        }
        haveGuestEvent = false;
        if (mixer.Guest != null)
        {
            if (mixer.Guest.Trim() != "")
            {
                haveGuestEvent = true;
            }
        }
        int srl = spcRule.Length;
        int i = 0;
        string srHdr = "";
        while (i < srl)
        {
            srHdr += "-";
            i++;
        };
 

        var slist =
            from pl in db.PlayersList
            join pn in db.Players on pl.PlayerID equals pn.PlayerID
            where pl.EventID == EventID && pl.Marked == 0
            orderby pn.Name
            select new { pn.Name, pn.LName, pn.FName, pn.Sex, pn.Hcp, pn.Title, pl.Action, pl.Carpool, pl.Marked, pl.SpecialRule, pl.GuestID };
        string msgBody = "";

                foreach (var item in slist)
                {
 
                    string gsex = "";
                    string gname = "";
                    string gLastName = "";
                    string gFirstName = "";
                    string ghcp = "";
                    int guestID = 0;
                    string gFirstLast = "";
                    if (item.GuestID == 0)
                    {
                        gsex = "";
                    }
                    else
                    {
                        Guest guest = db.Guest.FirstOrDefault(g => g.GuestID == item.GuestID);
                        if (guest == null)
                        {
                            gname = "";
                            gLastName = "";
                            gFirstName = "";
                            gsex = "";
                            ghcp = "";
                            gFirstLast = "";
                        }
                        else
                        {
                            gname = guest.GuestName.Trim();
                            gFirstName = guest.gFname.Trim();
                            gLastName = guest.gLname.Trim();
                            if (guest.gSex == 2) gsex = "[F]";
                            ghcp = guest.gHcp.Trim();
                            guestID = guest.GuestID;
                            gFirstLast = string.Format("{0}, {1}", gLastName.ToUpper(), gFirstName.ToUpper());
                        }
                    }
                    string srule = "";
                    if (item.SpecialRule != null)
                    {
                        srule = item.SpecialRule.Trim();
                    }
                    string sex = "";
                    if (item.Sex == 2) sex = " [F]";
                    string hcp  = "";
                    string playerTitle = "";
                    hcp = item.Hcp.Trim();
                    if (item.Title != null)
                    {
                        if (item.Title.Trim() != "")
                        {
                            playerTitle = " (" + item.Title.Trim() + ")";
                        }
                    }

                    string playerName = string.Format("{0}, {1}{2}", item.LName.Trim().ToUpper(), item.FName.Trim().ToUpper(), playerTitle);
 
                    msgBody += string.Format("{0}{1}, {2}",playerName, sex, hcp);
                    msgBody += srule != "" ? ", "+srule : "";
                    if ((haveGuestEvent) && (guestID !=0))
                    {
                        msgBody += string.Format("   --   Guest: {0}{1}, {2}", gFirstLast, gsex, ghcp);
                    }
                    msgBody += Environment.NewLine;
                }

        sourceBodyText.Text += msgBody+Environment.NewLine + Environment.NewLine;
        sourceBodyText.Text += "(Rep) is MISGA Representative; (AR) is Assistant Representative" + Environment.NewLine+Environment.NewLine;
        sourceBodyText.Text += literalRep.Text + ", "+literalOrg.Text+" Rep" +  Environment.NewLine;
        sourceBodyText.Text += literalRepEmail.Text + Environment.NewLine;
    }

    protected void LoadButton_Click(object sender, EventArgs e)
    {
        string eid = DropDownList1.SelectedValue;
        ShowEvent(eid);

//        sourceTo.Text = "Tom.Rowlett@gmail.com";
//        sourceFrom.Text = "playerlist@mrmisga.org";
        sourceSubject.Text = literalOrg.Text+" Player's List for MISGA Mixer on " + evDate;


        this.sl = SignupList.LoadFromPlayersListDB(eid);   // load Signup entries from PlayersLists for the selected Event ID
        this.PlayersListRepeater.DataSource = new SignupList[] { this.sl };
        this.PlayersListRepeater.DataBind();

        playerCount = sl.Entries.Count;
        lblCount.Text = "Player Limit = " + evPlayerLimit.ToString("##0") + ":  ";
        if (sl.Entries.Count == 1)
        {
            lblCount.Text += "There is 1 player for this event.";
        }
        else
        {
            if (sl.Entries.Count == 0)
            {
                lblCount.Text += "There are no players for this event.";
            }
            else
            {
                lblCount.Text += string.Format("There are {0} players for this event.", sl.Entries.Count.ToString());
            }
        }
        LoadEmailBody(eid);

        ShowControls(true);


    }

    protected bool loadDDL()
    {
        string closed = "";
        string keyLastDate = "LastDate";
        string keyShowDays = "DaysToShow";
        int daysToShow;
        MrTimeZone etz = new MrTimeZone();
        DateTime date1 = new DateTime(2012, 1, 8, 0, 0, 0);
        DateTime date2 = new DateTime(2012, 1, 1, 0, 0, 0);
        TimeSpan oneWeek = date1.Subtract(date2);
        DateTime nowDate = etz.eastTimeNow();

        DateTime startDate = nowDate;       // .Subtract(oneWeek);
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        MRParams dts = db.MRParams.FirstOrDefault(p => p.Key == keyShowDays);
        daysToShow = 365;
        if (ConfigurationManager.AppSettings[keyShowDays] != null) 
            daysToShow = Convert.ToInt32(ConfigurationManager.AppSettings[keyShowDays]);
        if (dts != null) daysToShow = Convert.ToInt32((string)dts.Value);
        DateTime endDate = nowDate.AddDays(daysToShow);
        DateTime lastDate = endDate;
        MRParams entry = db.MRParams.FirstOrDefault(p => p.Key == keyLastDate);
        if (entry != null)
        {
            lastDate = Convert.ToDateTime((string)entry.Value);
        }
        if (lastDate < endDate)
        {
            endDate = lastDate;
        }

        var query =
            (from ev in db.Events
             //                 join pl in db.PlayersList on ev.EventID equals pl.EventID
             //            (from pl in db.PlayersList
             //            join ev in db.Events on pl.EventID equals ev.EventID
             //            where (pl.Marked == 0)
             where ((ev.Date > startDate) && (ev.Date < endDate))
             orderby ev.EventID
             select new { ev.EventID, ev.Date, ev.Type, ev.Title, ev.Deadline }).Distinct();
        foreach (var item in query)
        {   
            closed = "";
            if (item.Type.Trim() != "MISGA")
            {
                if (item.Deadline <= nowDate) closed = " | CLOSED ";
            string tx = item.EventID.Trim() + " | " + item.Date.ToShortDateString() + " | " + item.Type.Trim() + " | " + item.Title.Trim() + closed;
            DropDownList1.Items.Add(new ListItem(tx, item.EventID));
            }
        }
        return (DropDownList1.Items.Count > 0);
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        ShowControls(false);

    }


}