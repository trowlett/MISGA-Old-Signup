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

/// <summary>
/// Summary description for MrSignup
/// </summary>
public class MrSignup
{
	public const string keyPlayers = "Players";
	private static DateTime minDate = new DateTime(2011,12,31);

	public static string Proper(string s)
	{
		string txt = "";
		for (int i = 0; i < s.Length; i++)
		{
			if (i == 0)
			{
				txt = "" + (char)s[i];
				txt = txt.ToUpper();
			}
			else
			{
				string tmp = "" + (char)s[i];
				txt = txt + tmp.ToLower();
			}
		}
		return txt;
	}


	private static string keyName;
	private const string txtCancel = "Cancel";

	public static int GetPlayerID(string lName, string fName, int sex)
	{
		keyName = Proper(lName.Trim()) + ", " + Proper(fName.Trim());
		MrTimeZone tz = new MrTimeZone();

		
		int pid = 0;
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);
		var query =
			from p in db.Players
			where p.Name == keyName
			select p;

		Players player = query.FirstOrDefault();     // Lookup Player in Players Datbase
		if (player != null)
		{
			pid = player.PlayerID;
		}
		else{
				Players pl = new Players()
				{              
					Name = keyName,
					LName = Proper(lName.Trim()),
					FName = Proper(fName.Trim()),
					Hcp = "NONE",
					MemberID = "",
					Sex = sex,
					Title = "",
					HDate = tz.eastTimeNow(),
                    Delete = 0
					};
				MRParams param = db.MRParams.FirstOrDefault(p => p.Key == keyPlayers);
				if (param != null)
				{
					pid = Convert.ToInt32(param.Value);
					pid += 1;
					pl.PlayerID = pid;
					db.Players.InsertOnSubmit(pl);
					param.Value = pid.ToString();
					param.ChangeDate = tz.eastTimeNow();
				}
				else
				{
					MRParams par = new MRParams();
					par.Key = keyPlayers;
					par.Value = "1";
					par.ChangeDate = tz.eastTimeNow();
					db.MRParams.InsertOnSubmit(par);
					pid = 1;
					pl.PlayerID = pid;
					db.Players.InsertOnSubmit(pl);
			}
		}
		db.SubmitChanges();

		return pid;
	}

	public static string AddToList(string EventID, int pID, string Action, string Carpool, int Sex, string sr, int guestID)
	{
		///<Remarks>
		///
		/// Action settings
		///    Signup
		///    Cancel
		///    
		/// Carpool Settings
		///    No  to Carpool
		///    Yes to Carpool
		///     
		////*  Add Player to Players List for the mixer or event specified in'EventID'
		/// *  First task is to find player in the table of 'Players' and if the player cannot be found,
		/// *  to give the player the next PlayerID available then add the player to the 'Players' Table.
		/// *  After completing that then create a players list entry for this player and event.  Then add that entry to the 'PlayersList'
		/// *  Table.
		/// *  
		/// * Then return control to caller with a success indicator as the result.  
		/// * Result of integer Zero indicates successful completion
		/// *           integer One  indicates an unsuccessful completion
		/// *
		/// * */
		/// 

        string result = "";
		//
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);
		//
		//
		MrTimeZone tz = new MrTimeZone();
        Events evnt = db.Events.FirstOrDefault(e => e.EventID == EventID);
        if (evnt != null)
        {
            Players player = db.Players.FirstOrDefault(x => x.PlayerID == pID);
            if (player != null)
            {
                PlayersList entry = db.PlayersList.FirstOrDefault(p => p.EventID == EventID && p.PlayerID == pID && p.Marked == 0);
                if (entry != null)
                {
                    if (Action == txtCancel)
                    {
                        entry.Marked = 1;       // set marked for Delete
                        if (evnt.Date < tz.eastTimeNow().AddHours(24))
                        {
                            result = "Cannot cancel online within 24 hours of Event.<br />Call the Golf Shop.";
                        }
                        else
                        {
                            if (tz.eastTimeNow() > evnt.Deadline)
                            {
                                result = "Cancel recorded.<br />Please notify the Host Golf Shop that you cancelled.";
                            }
                            else
                            {
                                result = "Cancel successful.";
                            }
                        }
                    }
                    else
                    {                           // modify existing entry in players database
                        entry.Carpool = Carpool;
                        entry.SpecialRule = sr;
                        entry.GuestID = guestID;
                        result = "Modification of entry was sucessful.";
                    }
                }
                else
                {                               // no entry for this player and event
                    PlayersList ple = new PlayersList()
                    {                                   // create new entry in players list database
                        EventID = EventID,
                        TransDate = tz.eastTimeNow(),
                        Marked = 0,                     // set entrymark to zero
                        PlayerID = pID,
                        Action = Action,
                        Carpool = Carpool,
                        SpecialRule = sr,
                        GuestID = guestID
                    };
                    if (Action != txtCancel)
                    {                                       // cannot do a cancel on new entry
                        db.PlayersList.InsertOnSubmit(ple);
                        result = "Sign-up was successful.";
                    }
                    else
                    {
                        ple.Marked = 1;        // set to not display canceled entries
                        result = string.Format("Cancel unsuccessful because {0} {1} not Signed Up.", player.FName, player.LName);
                    }
                }
                db.SubmitChanges();
            }
            else
            {
                result = "Player not in database.";
            }
        }
        else
        {
            result = "Event not in database.";
        }
        return result;
	}

	public static int ValidateGuestPlaying(string EventID, int gID, int pID)
	{
		int errorCode = 0;
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);

		PlayersList entry = db.PlayersList.FirstOrDefault(p => p.EventID == EventID && p.GuestID == gID && p.Marked == 0);
		if (entry != null)
		{
			if (entry.PlayerID != pID)
			{
				errorCode = 1;       // set error that guest is already playing with another member
			}
		}

		Guest gentry = db.Guest.FirstOrDefault(g => g.GuestID == gID);
		if (gentry != null)
		{
			if (gentry.Played > 0)
			{
				errorCode = 2;          // set erro that guest has already played as a guest once this year.
			}
		}

		return errorCode;
	}

	public static int GetGuestID(string ln, string fn, string hcp, int gender)
	{
		int id = 0;
		string keyGuest = Proper(ln.Trim()) + ", " + Proper(fn.Trim());
		
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);
		int gcount = db.Guest.Count();
		Guest ginfo = db.Guest.FirstOrDefault(g => g.GuestName == keyGuest);
		if (ginfo != null)
		{
			id = ginfo.GuestID;
			if (hcp.Trim() != "")
			{
				if (ginfo.gHcp.Trim() != hcp.Trim())
				{
					ginfo.gHcp = hcp.Trim();
					db.SubmitChanges();
				}
			}
		}
		else
		{
			id = gcount + 1;
			Guest gin = new Guest
			{
				GuestID = id,
				GuestName = keyGuest,
				gLname = Proper(ln.Trim()),
				gFname = Proper(fn.Trim()),
				gSex = gender,
				gHcp = hcp,
				Played = 0,
				DatePlayed = minDate
			};
			db.Guest.InsertOnSubmit(gin);
			db.SubmitChanges();
		}
		return id;
	}
    public static bool IsClosed(DateTime deadline)
    {
        MrTimeZone etz = new MrTimeZone();
        return (etz.eastTimeNow() >= deadline);
    }

	public MrSignup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}