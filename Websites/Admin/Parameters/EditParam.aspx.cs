using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parameters_EditParam : System.Web.UI.Page
{
    ParamsList pl { get; set; }
    private const string lastDate = "LastDate";
    private const string signupLimit = "SignupLimit";
    private const string showDays = "ShowDays";
    private const string club = "Club";
    private const string org = "Org";
    private const string repName = "Rep";
    private const string repPhone = "RepPhone";
    private const string repEmail = "RepEmail";

    private string pkey = "";

    public DateTime etzNow;

    protected void Page_Load(object sender, EventArgs e)
    {
        etzNow = new MrTimeZone().eastTimeNow();
        lblNow.Text = etzNow.ToString();
        if (!IsPostBack)
        {
            ShowParams();
            Label3.Text = @"Select the Key to change its Value.";
            Label2.Text = "";
            UpdatePanel1.Visible = false;
        }
    }

    public void ShowParams()
    {
        this.pl = ParamsList.LoadParams();
        this.ParamMainRepeater1.DataSource = new ParamsList[] { this.pl };
        this.ParamMainRepeater1.DataBind();

    }

    protected void Param_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        Label2.Text = "The " + ((Button)e.CommandSource).Text + " button has just been clicked; <br />";
        pkey =(string)e.CommandArgument;
        Label3.Text = string.Format(@"Key ""{0}"" has been selected.",pkey.Trim());
        lblKey.Text = pkey.Trim();
        lblChangeDate.Text = etzNow.ToString();
        // Write method to obatin Value for current Key
        // and insert call to it here
        TBValue.Text = GetParamValue(pkey);
        DisplayPanel.Visible = false;
        UpdatePanel1.Visible = true;

        // Need Method to change value for a speciafied Key in the Params Database


        ShowParams();

    }

    protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {

    }

    private string GetParamValue(String key)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var prm = db.MRParams.FirstOrDefault(p => p.Key== key);
        if (prm == null) return "";
        return prm.Value;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string chgkey = lblKey.Text;
        string chgvalue = TBValue.Text.Trim();
        DateTime chgChangeDate = etzNow;
        SaveParamValue(chgkey, chgvalue);
        Label2.Text = string.Format("Change to {0} completed.", chgkey);
        ShowParams();
        DisplayPanel.Visible = true;
        UpdatePanel1.Visible = false;

    }

    protected void SaveParamValue(string Key, string Value)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var chg = db.MRParams.FirstOrDefault(c => c.Key == Key);
        chg.Value = Value;
        DateTime chgChangeDate = new MrTimeZone().eastTimeNow();
        chg.ChangeDate = chgChangeDate;
        db.SubmitChanges();
 
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Label2.Text = string.Format("Change to {0} Cancelled",lblKey.Text);
        UpdatePanel1.Visible = false;
        DisplayPanel.Visible = true;
        ShowParams();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        deleteParam(lblKey.Text.Trim());
        Label2.Text = string.Format("{0} deleted.", lblKey.Text);
        ShowParams();
        DisplayPanel.Visible = true;
        UpdatePanel1.Visible = false;

    }
    protected void deleteParam(string k)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        MRParams rec = db.MRParams.FirstOrDefault(c => c.Key == k);

        if (rec != null)
        {
            db.MRParams.DeleteOnSubmit(rec);
        }
        db.SubmitChanges(); 
    }

}