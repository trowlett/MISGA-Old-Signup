using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;

public partial class Signups_EditSignupDB : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

//            cleanPlayersList();


    }
    protected void cleanPlayersList()
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var entry =
            from  p in db.PlayersList
            where p.PlayerID > 0
            select p;

        foreach (var item in entry)
        {
            item.GuestID = 0;
            
        }
        db.SubmitChanges();
    }
    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var entry = 
            from p  in db.PlayersList
            where p.PlayerID > 0
            select p;
        foreach (var item in entry)
        {
            db.PlayersList.DeleteOnSubmit(item);
        }
        db.SubmitChanges();

    }
}