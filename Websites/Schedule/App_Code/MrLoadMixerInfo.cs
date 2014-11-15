using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for MrLoadMixerInfo
/// </summary>
public class MrLoadMixerInfo
{
//	private Collection<SysEvent> events = new Collection<SysEvent>();
/*
	public Collection<SysEvent> Events
	{
		get
		{
			return this.events;
		}
	}
 * */

	public string FileName()
	{
		return _FileName;
	}
	public bool PageFileExists
    {
        get{return _PageFileExists;}
    }
    //{
	//	return _PageFileExists;
	//}

	public string[] MixerLines()
	{
		return _lines;
	}

	private static string _FileName;
	private static bool _PageFileExists;
	private static string[] _lines;
    private static string club;
	private static string[] _badID = { "<h2>Web Site Error</h2>\n" , 
										@"<p>Event Page does not exists.  Please send email to <a href=""",
										 "mailto:",
										 "webmaster",
										 "?subject=",
										 "website",
										 ":  Event Page not Found ID='", 
										 "XXXXXXXXX" ,
										 @"'"">",
										 "webmaster",
										 "</a> informing him that you " ,
										 "\nencountered this problem and please include the event and date you tried to sign up for.</p>",
                                         "<h2>",
                                         " ",
                                         "</h2>",
									 };
    private static string[] _message= { "<h2>",
                                          " ",
                                          "</h2>"
                                      };
    private static string[] _description = {"<!-- s/w generated -->\n\r<h2>Event:&nbsp;&nbsp;Mixer at ",
                                               "1 - club",
                                               "</h2>\n\r<h3>Visiting:&nbsp;&nbsp;",
                                               "3 - title",
                                               "</h3><h3>Date:&nbsp;&nbsp;",
                                               "5 - date",
                                               "\n\r</h3>\n\r<div id=\"details\"><table><tr><td><h4>Player Limit:&nbsp;&nbsp;",
                                               "7 - limit",
                                               "</h4>\n\r</td><td><h4>Signup Deadline:<br />&nbsp;&nbsp;",
                                               "9 - deadline",
                                               "</h4>\n\r</td></tr><tr><td><ul><li>Cost is:&nbsp;&nbsp;<b>",
                                               "11 - cost",
                                               "<em>(",
                                               "13 - payment",
                                               ")</em></b></li><li>Tee Time:&nbsp;&nbsp;<b>",
                                               "15 - tee time",
                                               "</b>, with refreshments ",
                                               "17 - refreshments",
                                               @"</li></ul></td><td><ul><li>",
                                               "19 - slope",
                                               "</li><li>Directions are on the <a href=\"",
                                               "http://misga.org/clubs_alpha.htm",
                                               "\">MISGA Web Site</a></li><li>Golf Shop Phone Number:&nbsp;&nbsp;<b>",
                                               "23 - Phone",
                                               "</b></li></ul></td></tr></table>\n\r",
                                               "25 - ",
                                               "26 - ",
                                               "27 - ",
                                               "</div>",
                                               "\n\r<!-- END s/w generated -->\n\r"
                                           };

    private static string[] _awayDesc = {   "<!-- s/w generated -->\n\r<h2>Event:&nbsp;&nbsp;Mixer at ",
                                            "1 - club ",
                                            "</h2>\n\r<h3>Date:&nbsp;&nbsp;",
                                            "3 - date",
                                            "</h3>\n\r<div id=\"details\">\n\r<table><tr><td><h4>Player Limit:&nbsp;&nbsp;",
                                            "5 - limit",
                                            "</h4>\n\r</td><td><h4>Signup Deadline:<br />&nbsp;&nbsp;",
                                            "7 - deadline",
                                            "</h4>\n\r</td></tr><tr><td><ul><li>Cost is:&nbsp;&nbsp;<b>",
                                            "9 - cost",
                                            "<em>(",
                                            "11 - payment",
                                            ")</em></b></li><li>Tee Time:&nbsp;&nbsp;<b>",
                                            "13 - tee time",
                                            "</b>, with refreshments ",
                                            "15 - refreshments",
                                            @"</li></ul></td><td><ul><li>",
                                            "17 - Slope",
                                            "</li><li>Directions are on the <a href=\"",
                                            "http://misga.org/clubs_alpha.htm",
                                            "\">MISGA Web Site</a></li><li>Golf Shop Phone Number:&nbsp;&nbsp;<b>",
                                            "21 - Phone",
                                            "</b></li></ul></td></tr></table>\n\r",
                                            "23 - ",
                                            "24 - ",
                                            "25 - ",
                                            "</div>",
                                            "\n\r<!-- END s/w generated -->\n\r"
                                       };

    private static string _limitMsg = "<p class=\"special\">* Due to cancelations and/or extra room in the field, additional players may be allowed</p>\n\r";

	private static string[] GetMixerInfo(string path, string EventID)
	{
		_PageFileExists = true;
//		_FileName = path + "\\Pages\\" + EventID + ".txt";
        _FileName = path + "\\App_Data\\" + EventID + ".txt"; 
        if (System.IO.File.Exists(_FileName))
		{
			_PageFileExists = true;
			_lines = System.IO.File.ReadAllLines(_FileName);
		}
		else
		{
            if (!clubInDatabase(EventID))
            {
                //			_badID[3] = _webmasteremail;
                _badID[3] = ConfigurationManager.AppSettings["WebMasterEmail"];
                //			_badID[5] = _website;
                _badID[5] = ConfigurationManager.AppSettings["Website"];
                //			_badID[9] = _webmasteremail;
                _badID[9] = ConfigurationManager.AppSettings["WebMaster"];
                _badID[7] = EventID;
                _lines = _badID;

                _PageFileExists = false;
            }
		}

		return _lines;
	}
    private static bool clubInDatabase(string eventID)
    {
        bool found = false;
        club = eventID.Substring(11, 3);
        string ClubsConnect = ConfigurationManager.ConnectionStrings["ClubsConnect"].ToString();
        MISGACLUBS cdb = new MISGACLUBS(ClubsConnect);

        Clubs item = cdb.Clubs.SingleOrDefault(c => c.ClubID.Trim() == club);
        if (item != null)
        {
            found = true;
            string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
            MRMISGADB mdb = new MRMISGADB(MRMISGADBConn);
            Events mixer = mdb.Events.FirstOrDefault(ev => ev.EventID == eventID);
            if (mixer.Type.ToUpper().Trim() == "HOME") _lines = fillHomeDescription(item, mixer);
            if (mixer.Type.ToUpper().Trim() == "AWAY") _lines = fillAwayDescription(item, mixer);
            // found club in database
        }
        else {
            found = false;
            _badID[13] = "Club " + club + " NOT found in Clubs database";
            _message[1] = "Club " + club + " NOT found in Clubs database";
            _lines = _message;
            // club not in database
        }
        return found;
    }
    private static string IsUnlimited(int pl)
    {
        return pl < 50 ? string.Format("{0}*",pl) :"Unlimited";
    }
    private static string IsSpecialRule(Events m, Clubs i)
    {
        string rst = "";
        if (m.SpecialRule.Trim() != "") rst = "<div id=\"rule\">\n\r<p>" + i.SRule + "</p>\n\r</div>";
        return rst;
    }
    private static string SetCost(string cost)
    {
        return cost.Substring(0, 3) + " ";
    }
    private static string[] fillAwayDescription(Clubs item, Events mixer)
    {
        _awayDesc[1] = item.ClubName;
        _awayDesc[3] = mixer.Date.ToLongDateString();
        _awayDesc[5] = IsUnlimited(mixer.PlayerLimit);
        _awayDesc[7] = string.Format("{0:h:mm tt} - {1:D}", mixer.Deadline, mixer.Deadline);
        _awayDesc[9] = SetCost(mixer.Cost);
        _awayDesc[11] = item.PayOpt;
        _awayDesc[13] = mixer.Date.ToShortTimeString();
        _awayDesc[15] = item.Refresh;
        _awayDesc[17] = string.Format("Slope for {0} is {1}", item.ClubName,item.slope);
        _awayDesc[19] = item.MISGAURL;
        _awayDesc[21] = item.ProPhone;
        _awayDesc[23] = mixer.PlayerLimit < 50 ? _limitMsg : " ";
        _awayDesc[24] = PolicyAndMisc(item);
        _awayDesc[25] = IsSpecialRule(mixer, item);
        return _awayDesc;
    }
    protected static string PolicyAndMisc(Clubs c)
    {
        string rst = "<p class=\"special\">" + c.OtherRule + "</p>\n\r";
        rst += "<p class=\"special\">"+c.Misc+"</p>\n\r";
        return rst;
    }

    private static string[] fillHomeDescription(Clubs item, Events mixer)
    {
        _description[1] = item.ClubName;
        _description[3] = mixer.Title;
        _description[5] = mixer.Date.ToLongDateString();
        _description[7] = IsUnlimited(mixer.PlayerLimit);
        _description[9] = string.Format("{0:h:mm tt} - {1:D}", mixer.Deadline, mixer.Deadline); 
        _description[11] = SetCost(mixer.Cost);
        _description[13] = item.PayOpt;
        _description[15] = mixer.Date.ToShortTimeString();
        _description[17] = item.Refresh;
        _description[19] = string.Format("Slope for {0} is {1}", item.ClubName, item.slope);
        _description[21] = item.MISGAURL;
        _description[23] = item.ProPhone;
        _description[25] = " ";
        _description[25] = (mixer.PlayerLimit < 50) ? _limitMsg : "";
        _description[26] = PolicyAndMisc(item);
        _description[27] = IsSpecialRule(mixer, item);
        return _description;
    }
    private string[] GetMixerInfo(string ClubID)
    {
        club = ClubID;
        string ClubsConnect = ConfigurationManager.ConnectionStrings["ClubsConnect"].ToString();
        MISGACLUBS cdb = new MISGACLUBS(ClubsConnect);

        Clubs item = cdb.Clubs.SingleOrDefault(c => c.ClubID.Trim() == club);
        if (item != null)
        {
            MrTimeZone etz = new MrTimeZone();
            Events mixer = new Events()
            {
                Type = "AWAY",
                Cost = "$xx",
                Date = etz.eastTimeNow().AddDays(5),
                Deadline = etz.eastTimeNow().AddDays(1),
                PlayerLimit = 10,
                Title = string.Format("View {0} | {1} display", item.ClubID, item.ClubName),
                SpecialRule = "SR"
            };
            if (mixer.Type.ToUpper().Trim() == "AWAY") _lines = fillAwayDescription(item, mixer);
            // found club in database
        }
        else
        {
            _badID[13] = "Club " + club + " NOT found in Clubs database";
            _message[1] = "Club " + club + " NOT found in Clubs database";
            _lines = _message;
            // club not in database
        }

        return _lines;
    }

	public MrLoadMixerInfo(string path, string EventID)
	{
		//
		// TODO: Add constructor logic here
		//
		_FileName = "";
		_PageFileExists = false;
		_lines = GetMixerInfo(path, EventID);
	}
    public MrLoadMixerInfo(string ClubID)
    {
        _FileName = "";
        _PageFileExists = false;
        _lines = GetMixerInfo(ClubID);
    }
}