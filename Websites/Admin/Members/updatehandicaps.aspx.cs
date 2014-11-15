using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Members_updatehandicaps : System.Web.UI.Page
{
    public MembersList ml { get; set; }
//    public HandicapList hc { get; set; }

    public DateTime hcpDate;

    private string path;
    private string filename;
    private int PlayersCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        path = Server.MapPath("");
//        filename = System.IO.Path.Combine(path, "handicaps.txt");
        filename = Server.MapPath("~\\App_Data\\handicaps.csv");


          if (!IsPostBack) {
              ShowMembers();
              lblFileName.Text = string.Format("From File = {0}",filename);

          }
       }

 
    public void ShowMembers()
    {
        this.ml = MembersList.LoadMembers();
        this.MembersListMainRepeater.DataSource = new MembersList[] { this.ml };
        this.MembersListMainRepeater.DataBind();

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblUpdateCount.Text = " ";
        lblUpdateCount.ForeColor = System.Drawing.Color.Green;
        // load handicaps.txt file from host server in handicap list
        int updateCount = 0;
        int countOfPlayers = 0;
        // MrResources mr = new MrResources();
        // path = Server.MapPath(mr.Root);
        try
        {
            hcpDate = Convert.ToDateTime(tbHcpDate.Text);

            List<Handicap> hc = Handicap.LoadHandicaps(filename, hcpDate);

            // update sql Players with handicaps from text file
            string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
            MRMISGADB db = new MRMISGADB(MRMISGADBConn);
            MRParams entry = db.MRParams.FirstOrDefault(p => p.Key == "Players");
            if (entry == null)
            {
                PlayersCount = 0;
            }
            else
            {
                PlayersCount = Convert.ToInt32(entry.Value);
                for (int i = 0; i <= PlayersCount; ++i)
                {
                    var m = db.Players.FirstOrDefault(h => h.PlayerID == i);
                    if (m != null)
                    {
                        countOfPlayers++;
                        foreach (var hcp in hc)
                        {
                            if (m.MemberID.Trim() == hcp.ID.Trim())
                            {
                                m.Hcp = hcp.Index;
                                m.HDate = hcp.Date;
                                db.SubmitChanges();
                                updateCount++;
                            }
                        }
                    }
                }
            }
        ShowMembers();
        lblUpdateCount.Text = string.Format("{0} of {1} Handicaps Updated", updateCount, countOfPlayers);
        }
        catch (FormatException)
        {
            lblUpdateCount.Text = "Invalid Handicap Date";
            lblUpdateCount.ForeColor = System.Drawing.Color.Red;
        }
    }
}