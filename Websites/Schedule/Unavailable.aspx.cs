using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Unavailable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MrTimeZone etz = new MrTimeZone();
        DateTime t = etz.eastTimeNow().AddMinutes(30);
        lblCheckbackTime.Text = string.Format("in 30 minutes at {0}", t.ToShortTimeString());

    }
}