using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected string OrgURL { get { return ConfigurationManager.AppSettings["OrgURL"]; } }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
