using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parameters_AddParam : System.Web.UI.Page
{
    ParamsList pl { get; set; }

    private string newKey = "";
    private string newValue = "";
    DateTime etzNow;


    protected void Page_Load(object sender, EventArgs e)
    {
        etzNow = new MrTimeZone().eastTimeNow();
        if (!IsPostBack)
        {
            ShowParams();
        }
    }

    public void ShowParams()
    {
        this.pl = ParamsList.LoadParams();
        this.ParamMainRepeater1.DataSource = new ParamsList[] { this.pl };
        this.ParamMainRepeater1.DataBind();

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        newKey = tbKeyToAdd.Text;
        newValue = tbValueToAdd.Text;
        AddParameter(newKey, newValue);
        lblStatus.Text = string.Format("{0} added.", newKey);
        tbKeyToAdd.Text = "";
        tbValueToAdd.Text = "";
        ShowParams();

    }

    protected void AddParameter(string key, string value)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        MRParams prm = new MRParams();
        prm.Key = key;
        prm.Value = value;
        prm.ChangeDate = new MrTimeZone().eastTimeNow();

        db.MRParams.InsertOnSubmit(prm);
        db.SubmitChanges();

    }

}