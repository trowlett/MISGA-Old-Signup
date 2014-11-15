using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

    public partial class Administration_ChangeDate : System.Web.UI.Page
    {
        private const string keyLastDate = "LastDate";
        private const string keyFirstDate = "FirstDate";
        private DateTime FirstDate;
        private DateTime LastDate;
        private DateTime changeDate;
        private String newFirstDate;
        private string newLastDate;
        private MrTimeZone etz;

        protected void loadLastDate()
        {
            string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
            MRMISGADB db = new MRMISGADB(MRMISGADBConn);
            MRParams entry = db.MRParams.FirstOrDefault(p => p.Key.Trim() == keyLastDate);
            if (entry != null)
            {
                LastDate = Convert.ToDateTime((string)entry.Value);
                changeDate = (DateTime)entry.ChangeDate;
            }
            else
            {
                int curyear = etz.eastTimeNow().Year;
                LastDate = new DateTime(curyear, 3, 1, 23, 59,59, System.Globalization.CultureInfo.InvariantCulture.Calendar);
                changeDate = etz.eastTimeNow();
                MRParams newParm = new MRParams();
                newParm.Key = keyLastDate;
                newParm.Value = LastDate.ToString();
                newParm.ChangeDate = changeDate;
                db.MRParams.InsertOnSubmit(newParm);
                db.SubmitChanges();
            }
            lblDateLastChanged.Text = "Date Last Changed: " + changeDate.ToString();
        }
        protected void LoadFirstDate()
        {
            string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
            MRMISGADB db = new MRMISGADB(MRMISGADBConn);
            MRParams entry = db.MRParams.FirstOrDefault(p => p.Key.Trim() == keyFirstDate);
            if (entry != null)
            {
                FirstDate = Convert.ToDateTime((string)entry.Value);
                changeDate = (DateTime)entry.ChangeDate;
            }
            else
            {
                int curyear = etz.eastTimeNow().Year;
                FirstDate = new DateTime(curyear, 2, 15, 0, 0, 1, System.Globalization.CultureInfo.InvariantCulture.Calendar);
                changeDate = etz.eastTimeNow();
                MRParams newParm = new MRParams();
                newParm.Key = keyFirstDate;
                newParm.Value = FirstDate.ToString();
                newParm.ChangeDate = changeDate;
                db.MRParams.InsertOnSubmit(newParm);
                db.SubmitChanges();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            etz = new MrTimeZone();
            if (!IsPostBack)
            {
                LoadFirstDate();
                CalFirstDate.SelectedDates.Clear();
                CalFirstDate.SelectedDates.Add(FirstDate);
                CalFirstDate.VisibleDate = FirstDate;
                tbFirstDate.Text = FirstDate.ToShortDateString();
                lblDateChanged.Text = "";
                loadLastDate();
                CalLastDate.SelectedDates.Clear();
                CalLastDate.SelectedDates.Add(LastDate);
                CalLastDate.VisibleDate = LastDate;
                tbLastDate.Text = LastDate.ToShortDateString();
                lblDateChanged.Text = "";
            }
        }

        protected void updateParams()
        {
            DateTime lDate = Convert.ToDateTime(newLastDate);
            LastDate = new DateTime(lDate.Year, lDate.Month, lDate.Day, 23, 59, 59);
//            MrTimeZone tmz = new MrTimeZone();
            DateTime fDate = Convert.ToDateTime(newFirstDate);
            FirstDate = new DateTime(fDate.Year, fDate.Month, fDate.Day, 0, 0, 1);
            
            changeDate = etz.eastTimeNow();

            string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
            MRMISGADB db = new MRMISGADB(MRMISGADBConn);
            MRParams param = db.MRParams.FirstOrDefault(p => p.Key == keyLastDate);
            if (param != null)
            {
                param.Value = (string)LastDate.ToString();
                param.ChangeDate = changeDate;
            }
            else
            {
                MRParams newParam = new MRParams();
                newParam.Key = keyLastDate;
                newParam.Value = (string)LastDate.ToString();
                newParam.ChangeDate = changeDate;
                db.MRParams.InsertOnSubmit(newParam);
            }
            MRParams xparam = db.MRParams.FirstOrDefault(p => p.Key.Trim() == keyFirstDate);
            if (xparam != null)
            {
                xparam.Value = (string)FirstDate.ToString();
                xparam.ChangeDate = changeDate;
            }
            else
            {
                MRParams newParam = new MRParams();
                newParam.Key = keyFirstDate;
                newParam.Value = (string)FirstDate.ToString();
                newParam.ChangeDate = changeDate;
                db.MRParams.InsertOnSubmit(newParam);
            }
            db.SubmitChanges();
            lblDateChanged.Text = "Last Date Changed Successfully: " + changeDate.ToString();
        }

        protected void btnChangeDate_Click(object sender, EventArgs e)
        {
//            newLastDate = tbNewLastDate.Text;
            newLastDate = CalLastDate.SelectedDate.ToShortDateString();
            newFirstDate = CalFirstDate.SelectedDate.ToShortDateString();
            updateParams();
            tbFirstDate.Text = tbNewFirstDate.Text;
            tbLastDate.Text = tbNewLastDate.Text;
            CalFirstDate.VisibleDate = FirstDate;
            CalLastDate.VisibleDate = LastDate;
            CalFirstDate.SelectedDates.Clear();
            CalLastDate.SelectedDates.Clear();
            CalFirstDate.SelectedDates.Add(FirstDate);
            CalLastDate.SelectedDates.Add(LastDate);
            tbNewFirstDate.Text = "";
            tbNewLastDate.Text = "";
        }


        protected void CalLastDate_SelectionChanged(object sender, EventArgs e)
        {
            tbNewLastDate.Text = CalLastDate.SelectedDate.ToShortDateString();
            CalLastDate.VisibleDate = CalLastDate.SelectedDate;
        }
        protected void CalFirstDate_SelectionChanged(object sender, EventArgs e)
        {
            tbNewFirstDate.Text = CalFirstDate.SelectedDate.ToShortDateString();
            CalFirstDate.VisibleDate = CalFirstDate.SelectedDate;
        }
}
