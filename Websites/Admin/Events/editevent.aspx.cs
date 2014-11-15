using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events_editevent : System.Web.UI.Page
{
    private string eventID;
    private string element;

    protected void Page_Load(object sender, EventArgs e)
    {
        eventID = Request.QueryString["ID"];
        element = Request.QueryString["element"];
        lblEventID.Text = eventID;
        lblElement.Text = element;

    }
}