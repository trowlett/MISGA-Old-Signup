using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    public MembersList ml { get; set; } 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            ShowMembers();
          }
       }

    public void ShowMembers()
    {
        this.ml = MembersList.LoadMembers();
        this.MembersListMainRepeater.DataSource = new MembersList[] { this.ml };
        this.MembersListMainRepeater.DataBind();

    }
    protected void Member_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        Label2.Text = "The " + ((Button)e.CommandSource).Text + " button has just been clicked; <br />";
        int playerID = Convert.ToInt32(((Button)e.CommandSource).Text.Trim());
        int deleteCode = MembersList.DeleteMember(playerID);
        if (deleteCode == 0)
        {
            Label3.Text = MembersList.MembersName + " has been deleted.";
        }
        else
        {
            Label3.Text = MembersList.MembersName + " has ACTIVE Signups and will stay in the members database.";
        }
        ShowMembers();

    }
}