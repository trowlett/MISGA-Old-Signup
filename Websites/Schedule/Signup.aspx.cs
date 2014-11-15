using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

	public partial class Schedule_Signup : System.Web.UI.Page
	{
		public SignupList sl { get; set; }

		private string eventID;
		private string path;
		private const string keyPlayers = "Players";
        private const string keySignups = "Signups";
		private string[] contact = new string[3];
		public string webMaster;
		public string webMasterEmail;
		public string website;
		private bool GuestOption = false;
		public string ruleGuest = "";
		public string rule89 = "";
		public const string Guest = "Guest";
		private DateTime deadline;
		private int playerLimit;
        private DateTime postDate;
		private string sRule = "";
        private string[] sRuleChoices;
        private static readonly char[] delimiterChars = { ',' };
        private string spcRule;
        private int prevSRSelection;
        private string prevSRItem;
        private int errorCode = 0;
		private int maxLimit = 60;
		private const string keySignupLimit = "SignupLimit";
		private string hostClubPhone = "Hamburger I 812";
		private string userLastName = "";
		private string userFirstName = "";
		private string userGender = "";
		private string userLastVisit = "";
		private string ln = "";
		private string fn = "";
		private string carpool = "";
		private string tgender = "";
        private bool enableSignups = false;
        private string offset;
        private int signupOffset;
        private int srCount;
        private bool IsAccessControlOn;

		protected void LoadMixerInfo(string[] mixerLines)
		{
			litInfo.Text = "";
			int i = 0;
			foreach (string line in mixerLines)
			{
				i = i + 1;
				litInfo.Text = litInfo.Text + line;
			}
		}
        protected static bool IsSignupAllowed(MRParams p)
        {
            if (p == null) return true;                     // Allowed if nothing in MRParams file
            if (p.Value.ToUpper() == "YES") return true;    // Allowed if YES
            if ((p.Value.Length > 5) && (p.Value.Substring(0,6).ToUpper() == "ENABLE")) return true;    
            return false;                                   // disallow if anything else
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			eventID = Request.QueryString["ID"];
			litOrg1.Text = ConfigurationManager.AppSettings["Org"];
			litOrg2.Text = ConfigurationManager.AppSettings["Org"];
            offset = ConfigurationManager.AppSettings["SignupOffset"];
            IsAccessControlOn = false;
            if ((ConfigurationManager.AppSettings["AccessControl"] != null) &&
                (ConfigurationManager.AppSettings["AccessControl"].ToLower().Trim() == "on"))
                    IsAccessControlOn = true;
            if (!IsAccessControlOn)
            {
                lblAccessCode.Visible = false;
                tbAccessCode.Visible = false;
            }
            signupOffset = 0;
            if (offset != null)
                if (offset.Trim() != "")
                    signupOffset = Convert.ToInt32(offset);


            MrTimeZone tz = new MrTimeZone();
            deadline = tz.eastTimeNow();
            postDate = tz.eastTimeNow();
            playerLimit = 1;
            hostClubPhone = "";
//            linkbuttonSR.Visible = false;
            

            enableSignups = false;
            string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
            MRMISGADB db = new MRMISGADB(MRMISGADBConn);
            MRParams param = db.MRParams.FirstOrDefault(p => p.Key == keySignups);
            if (IsSignupAllowed(param)) enableSignups = true;
            if (!IsPostBack)
			{
				NormalPanel.Visible = true;
                SignupPanel.Visible = true;
                ClosedPanel.Visible = false;
				GuestListPanel.Visible = false;
				SpecialRulePanel.Visible = false;
				GuestOption = false;
//				cbSpecialRule.Checked = false;
                SRCBLPanel.Visible = false;
                SRCBKL.Visible = false;
                SRCBKL.Items.Clear();   
                cbGuestRule.Checked = false;
				tbGFN.Text = "";
				tbGLN.Text = "";
				tbGHcp.Text = "";
                GetCookieUserInfo();
            }
            var mixer = db.Events.FirstOrDefault(ev => ev.EventID == eventID);

            if (mixer != null)
            {
                if (mixer.Type.Trim() == "Home")
                {
                    Page.Title = string.Format("{0} {1} Sign-up", mixer.Date.ToString("MMM d"), ConfigurationManager.AppSettings["Org"]);
                }
                else
                {
                    Page.Title = string.Format("{0} {1} Sign-up",mixer.Date.ToString("MMM d"), mixer.Title);
                }
                deadline = mixer.Deadline;
                playerLimit = mixer.PlayerLimit;
                hostClubPhone = GetHostPhone(mixer.HostID);
                postDate = mixer.PostDate;
                if (playerLimit == maxLimit)
                {
                    lblPlayerLimit.Text = "(Player Limit: Unlimited)";
                }
                else
                {
                    lblPlayerLimit.Text = "(Player Limit: " + playerLimit.ToString("##0") + "*)";
                }
                if (mixer.SpecialRule != null)
                {
                    sRule = mixer.SpecialRule.Trim();
                    sRuleChoices = sRule.Split(delimiterChars);
                }
                if ((mixer.Guest != null) && (mixer.Guest.Trim()) == Guest)
                {
                    GuestOption = true; ;
                }
            }
            postDate = tz.eastTimeNow().AddDays(-signupOffset);  // temporary for testing
            enableSignups = (tz.eastTimeNow() >= postDate) && enableSignups;

			//
			// load mixer details  from text file
			//
			path = Server.MapPath("");
			MrLoadMixerInfo mixerInfo = new MrLoadMixerInfo(path, eventID);
			LoadMixerInfo(mixerInfo.MixerLines());
            btnSubmit.Enabled = mixerInfo.PageFileExists;
			lblEventID.Text = eventID;
			//
			// Get the mixer from the events db
			//

			MRParams entry = db.MRParams.FirstOrDefault(p => p.Key == keySignupLimit);
			if (entry != null)
			{
				maxLimit = Convert.ToInt32((string)entry.Value);
			}

			getPlayersList(eventID);
			//
			// Check for deadline already passed
			//
            if (MrSignup.IsClosed(deadline))
            {
                // Deadline is in the past			}
                lblClosed.Text = @"<span class=""closed"">Contact ";
                //                lblSignupForm.Text += "the<br />Host Club at<br />";
                //				  lblSignupForm.Text += hostClubPhone;
//                lblClosed.Text += "your <br />MISGA Rep, <br >";
                lblClosed.Text += ConfigurationManager.AppSettings["Contact"] + " at<br />";
                string cp = ConfigurationManager.AppSettings["ContactPhone"];
                if (cp == "")
                    cp = hostClubPhone;

                lblClosed.Text += cp;

                lblClosed.Text += "<br />to see if you can be added to the list.</span>";
/*                rblAction.Items[0].Selected = false;
                rblAction.Items[1].Selected = true;
                rblAction.Items[0].Enabled = false;
                rblAction.Items[1].Enabled = true;
 * */
                SignupPanel.Visible = false; 
                ClosedPanel.Visible = true;
                ClosedMessagePanel.Visible = true;
                tblGenderPool.Visible = false;
                rblAction.Visible = false;
                cbCarpool.Visible = false;
                lblgender.Visible = false;
                rblgender.Visible = false;
                PlayerPanel.Visible = false;
//                lblFN.Visible = false;
//                lblLN.Visible = false;
//                FirstNameTextBox.Visible = false;
//                LastNameTextBox.Visible = false;
//                cbCarpool.Visible = false;
//                LinkButton1.Visible = false;
                SpecialRulePanel.Visible = false;
                SRCBLPanel.Visible = false;
                SRCBKL.Visible = false;
                SRCBKL.Items.Clear();
                linkbuttonSR.Visible = false;
                cbGuestRule.Visible = false;
//                cbSpecialRule.Visible = false;
                GuestPanel.Visible = false;
                if (GuestOption)
                {
                    GuestListPanel.Visible = true;
                    NormalPanel.Visible = false;
                }
                enableSignups = false;
            }
            else
            {
                // enable signup page to accomodate special rule
                if (!IsPostBack)
                {
                    if ((sRuleChoices.Length > 0) && (sRuleChoices[0] != ""))
                    {
                        SRCBKL.Items.Add("Default");
                        foreach (string choice in sRuleChoices)
                        {
                            if (choice != "")
                            {
                                SRCBKL.Items.Add(choice);
                            }
                        }
                        srCount = 0;
                        if (SRCBKL.Items.Count > 0)
                        {
                            SpecialRulePanel.Visible = true;
                            linkbuttonSR.Visible = true;
                            SRCBKL.Visible = true;
                            SRCBLPanel.Visible = true;
                            prevSRSelection = -1;
                            prevSRItem = "";
                        }
                    }
                }
/*                if (sRule != "")
                                {
                                    cbSpecialRule.Text = sRule;
                                    SpecialRulePanel.Visible = true;
                                    cbSpecialRule.Visible = true;
                                    linkbuttonSR.Visible = true;

                                }   */
                if (GuestOption)
                {
                    SpecialRulePanel.Visible = true;
                    cbGuestRule.Text = "Partner";
                    cbGuestRule.Visible = true;

                    GuestPanel.Visible = true;

                    GuestListPanel.Visible = true;
                    NormalPanel.Visible = false;
                }
            }
            btnSubmit.Enabled = (mixerInfo.PageFileExists && enableSignups);
            SubmitPanel.Visible = (mixerInfo.PageFileExists && enableSignups);
//            btnSubmit.Enabled = (mixerInfo.PageFileExists);
		}

		protected void getPlayersList(string eid)
		{
			this.sl = SignupList.LoadFromPlayersListDB(eid);   // load Signup entries from PlayersLists for the selected Event ID
			this.PlayersListRepeater.DataSource = new SignupList[] { this.sl };
			this.PlayersListRepeater.DataBind();
			this.GuestListRepeater.DataSource = new SignupList[] { this.sl };
			this.GuestListRepeater.DataBind();

		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			ln = LastNameTextBox.Text.Trim();
			fn = FirstNameTextBox.Text.Trim();
			string action = rblAction.SelectedValue;       // Action  = Signup or Cancel
			carpool = cbCarpool.Checked?"Yes":"No";        // Carpool = No or Yes
			tgender = rblgender.SelectedValue;

			if (carpool.Trim() == "No")
			{
				carpool = "";
			}

/*            if (invalidMember(ln, fn))
            {
                errorCode = 5;
            }
*/
            if (IsAccessControlOn)
            {
                if (!AccessControl.IsValidMember(ln, fn, tbAccessCode.Text))
                {
                    errorCode = 6;
                }
            }
			SaveUserInfo(fn, ln, tgender, carpool);
			int gender = rblgender.SelectedIndex + 1;

			lblAction.Text = "";
            lblAction.Visible = false;

            linkbuttonSR.Visible = false;
            spcRule = "";
            int n = 0;
            if (SRCBKL.Items.Count > 0)
            {
                for (int i = 1; i < SRCBKL.Items.Count; i++)
                {
                    if (SRCBKL.Items[i].Selected)
                    {
                        spcRule += SRCBKL.Items[i].Value;
                        n++;
                    }
                }
            }
            if (n > 2) errorCode = 4;

			string eid = eventID;
			string gln = "";
			string gfn = "";
			int ggender = 0;
			string ghcp = tbGHcp.Text;
			int guestID = 0;
			int PlayerID = MrSignup.GetPlayerID(ln, fn, gender);

			
			if (cbGuestRule.Checked) 
			{
				gln = tbGLN.Text.Trim();
				gfn = tbGFN.Text.Trim();
				ggender = rblGgender.SelectedIndex + 1;
				guestID = MrSignup.GetGuestID(gln, gfn, ghcp, ggender);
				errorCode = MrSignup.ValidateGuestPlaying(eid, guestID, PlayerID);
			}
			if (errorCode == 0)
			{
				string errmsg = MrSignup.AddToList(eid, PlayerID, action, carpool, gender, spcRule, guestID);
//				lblAction.Text = string.Format("Your {0} for this event was successful.",action);
                lblAction.Text = errmsg;
//				lblAction.ForeColor = System.Drawing.Color.DarkGreen;
                lblAction.ForeColor = System.Drawing.Color.Firebrick;
                lblAction.Font.Bold = true;
                lblAction.Visible = true;
//                LinkButton1.Visible = false;
			}
			if (errorCode == 1)
			{
				lblAction.Text = "Partner already playing.  Signup is unsuccessful.";
				lblAction.ForeColor = System.Drawing.Color.Firebrick;
                lblAction.Visible = true;
//                LinkButton1.Visible = false;
            }
			if (errorCode == 2)
			{
				lblAction.Text = "Partner has played once.  Signup unsuccessful.";
				lblAction.ForeColor = System.Drawing.Color.Firebrick;
                lblAction.Visible = true;
//                LinkButton1.Visible = false;
            }
            if (errorCode == 4)
            {
                lblAction.Text = "Select no more than one Special Rule.  Signup unsuccessful.";
                lblAction.ForeColor = System.Drawing.Color.Firebrick;
                lblAction.Visible = true;

            }
            if (errorCode == 6)
            {
                lblAction.Text = "Invalid Access Code.  Please try again";
                lblAction.ForeColor = System.Drawing.Color.Firebrick;
                lblAction.Visible = true;
            }

			getPlayersList(eventID);

			resetSignupForm();
            if (errorCode == 0)
            {
//                Server.Transfer("Default.aspx");
            }
		}
        protected string GetHostPhone(string HostID)
        {
            string phone = "Unavailable";
            string ClubsConnect = ConfigurationManager.ConnectionStrings["ClubsConnect"].ToString();
            MISGACLUBS cdb = new MISGACLUBS(ClubsConnect);

            Clubs item = cdb.Clubs.SingleOrDefault(c => c.ClubID.Trim() == HostID.Trim());
            if (item != null)
                phone = item.ProPhone;

            return phone;
        }

		protected void resetSignupForm()
		{
//			FirstNameTextBox.Text = userFirstName;
//			LastNameTextBox.Text = userLastName;
			rblAction.SelectedIndex = 0;
//			rblgender.SelectedValue = userGender;
			lblgender.Visible = true;
			cbGuestRule.Checked = false;
//            SRCBKL.Items.Clear();
//            if (sRule != "")

//            SRCBLPanel.Visible = false;
//            cbSpecialRule.Checked = false;
            cbCarpool.Checked = false;
            rbClosedCancel.Checked = false;
			tbGHcp.Text = "";
			tbGFN.Text = "";
			tbGLN.Text = "";
            int j = 0;
            while (j < SRCBKL.Items.Count)
            {
                SRCBKL.Items[j].Selected = false;
                j++;
            }
		}


		protected void rbSpecialRule_CheckedChanged(object sender, EventArgs e)
		{

		}
        protected void rbClosedCancel_CheckedChanged(object sender, EventArgs e)
        {
            ClosedMessagePanel.Visible = false;
            PlayerPanel.Visible = true;
            btnSubmit.Enabled = true;
            rblAction.SelectedItem.Text = "Cancel";
        }
        protected void SRCBKL_SelectedIndexChanged(object sender, EventArgs e)
        {
            srCount++;
            int sndx = SRCBKL.SelectedIndex;
            string sitem = SRCBKL.SelectedValue;
            spcRule = sitem;
            if (SRCBKL.Items.Count > 0)
            {
                for (int i = 0; i < SRCBKL.Items.Count; i++)
                {
                    if (SRCBKL.Items[i].Selected)
                    {
                        if (SRCBKL.Items[i].Value == prevSRItem)
                        {
                            SRCBKL.Items[i].Selected = false;
                            prevSRItem = "";
                            prevSRSelection = -1;
                        }
                    }
                }
            }
            prevSRItem = SRCBKL.SelectedValue;
            prevSRSelection = SRCBKL.SelectedIndex;
        }
        protected void GetCookieUserInfo()
        {
            if (Request.Cookies["userInfo"] != null)
            {
                userLastName =
                    Server.HtmlEncode(Request.Cookies["userInfo"]["userLastName"]);
                userFirstName =
                    Server.HtmlEncode(Request.Cookies["userInfo"]["userFirstName"]);
                userGender =
                    Server.HtmlEncode(Request.Cookies["userInfo"]["userGender"]);

                userLastVisit =
                    Server.HtmlEncode(Request.Cookies["userInfo"]["lastVisit"]);

                if (userGender == "") userGender = "Male";
                if (FirstNameTextBox.Text == "") FirstNameTextBox.Text = userFirstName;
                if (LastNameTextBox.Text == "") LastNameTextBox.Text = userLastName;

                if (rblgender.SelectedValue != userGender) rblgender.SelectedValue = userGender;
            }
        }

        protected void SaveUserInfo(string fn, string ln, string gender, string carpool)
        {
            HttpCookie aCookie = new HttpCookie("userInfo");
            aCookie.Values["userFirstName"] = fn;
            aCookie.Values["userLastName"] = ln;
            aCookie.Values["userGender"] = gender;
            aCookie.Values["lastVisit"] = DateTime.Now.ToString();
            aCookie.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Add(aCookie);
        }
        protected void ResetUserInfo()
        {
            HttpCookie aCookie = new HttpCookie("userInfo");
            aCookie.Values["userFirstName"] = "";
            aCookie.Values["userLastName"] = "";
            aCookie.Values["userGender"] = "";
            aCookie.Values["lastVisit"] = DateTime.Now.ToString();
            aCookie.Expires = DateTime.Now.AddMinutes(120);
            Response.Cookies.Add(aCookie);
        }
}

