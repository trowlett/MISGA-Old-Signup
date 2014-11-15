using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events_addevent : System.Web.UI.Page
{
    private string clubID;
    private string clubName;
    private string hostClubID;
    private string hostClubName;
    private string visitingClubID;
    private string visitingClubName;
    private DateTime homeDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        clubID = ConfigurationManager.AppSettings["ClubID"];
        clubName = ConfigurationManager.AppSettings["Club"];
        lblClub.Text = clubName;


    }
    protected void ddlPlace_SelectedIndexChanged(object sender, EventArgs e)
    {
        string place = ddlPlace.SelectedItem.Value;
        ddlPlace.Enabled = false;
        if (place == "Away")
        {
            AwayPanel.Visible = true;
            HomePanel.Visible = false;
            MISGAPanel.Visible = false;
        }
        if (place == "Home")
        {
            AwayPanel.Visible = false;
            HomePanel.Visible = true;
            MISGAPanel.Visible = false;
            fillInHomeInfo();
        }
        if (place == "MISGA")
        {
            AwayPanel.Visible = false;
            HomePanel.Visible = false;
            MISGAPanel.Visible = true;
        }
        if (place == "select")
        {
            AwayPanel.Visible = false;
            HomePanel.Visible = false;
            MISGAPanel.Visible = false;
            ddlPlace.Enabled = true;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Server.Transfer("addevent.aspx");
    }
    protected void btnAwaySelect_Click(object sender, EventArgs e)
    {
        hostClubID = ddlAwayHost.SelectedValue.Trim();
        hostClubName = ddlAwayHost.SelectedItem.Text.Trim();

    }
    protected void btnHomeSelect_Click(object sender, EventArgs e)
    {
        visitingClubID = ddlVisit1.SelectedValue.Trim();
        visitingClubName = ddlVisit1.SelectedItem.Text.Trim();
        if (tbHomeTitle.Text == "")
        {
            tbHomeTitle.Text += visitingClubName;
        }
        else
        {
            tbHomeTitle.Text += ", " + visitingClubName;
        }
    }
    protected void fillInHomeInfo()
    {
        if (tbDate.Text == "")
        {
            tbDate.Text = GetHomeDate();
        }
        tbCost.Text = "$45.00";
        tbTeeTime.Text = "9:00 AM";
        homeDate = Convert.ToDateTime(tbDate.Text);
        if ((homeDate.Month > 5) && (homeDate.Month < 9))
        {
            homeDate = Convert.ToDateTime(tbDate.Text + " 8:30");
            tbTeeTime.Text = "8:30 AM";
        }
        else
        {
            homeDate = Convert.ToDateTime(tbDate.Text + " 9");
            tbTeeTime.Text = "9:00 AM";
        }
    }
    protected string GetHomeDate()
    {
        string d = "";
//        d = window.prompt("Welcome?", "Enter your name here.");
//        document.write("Welcome " + theResponse + ".<BR>");
        return d;
    }
}