using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for SignupList
/// </summary>
public class SignupList
{

	private Collection<SignupEntry> entries = new Collection<SignupEntry>();

	public Collection<SignupEntry> Entries
	{
		get
		{
			return this.entries;
		}
	}

	public static int EntriesPurged { get { return _entriesPurged; } set { _entriesPurged = value; } }
	private static int _entriesPurged = 0;

	public static SignupList LoadFromPlayersListDB(string EventID)
	{
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);

		SignupList target = new SignupList();
		var slist =
			from pl in db.PlayersList
			join pn in db.Players on pl.PlayerID equals pn.PlayerID
			where pl.EventID == EventID && pl.Marked == 0
			orderby pl.TransDate
			select new { pl.TransDate, pl.EventID, pl.PlayerID, pn.Name, pn.Sex, pn.Hcp, pl.Action, pl.Carpool, pl.Marked, pl.SpecialRule, pl.GuestID };
		int seqNo = 0;

		foreach (var item in slist)
		{
			seqNo++;
			SignupEntry entry = new SignupEntry(){
				SeqNo = seqNo,
				STDate = item.TransDate,
				SeventId = item.EventID,
                SPlayerID = item.PlayerID,
				Splayer = item.Name,
				Shcp = item.Hcp,
				Saction= item.Action,
				Scarpool = item.Carpool,
				Smarked= item.Marked,
				SspecialRule = item.SpecialRule,
				SGuest = item.GuestID,
                SSelected = false,
                SDelete = false
			};
			entry.Ssex = "";
			if ((int) item.Sex == 2) entry.Ssex = "[F]";
			if (item.GuestID == 0)
			{
				entry.SGuest = 0;
			}
			else
			{
//				MRParams param = db.MRParams.FirstOrDefault(p => p.Key == keyPlayers);
				Guest guest = db.Guest.FirstOrDefault(g => g.GuestID == item.GuestID);
				entry.SGuestName = guest.GuestName;
				entry.SgHcp = guest.gHcp;
				entry.SgSex = "";
				if (guest.gSex == 2) entry.SgSex = "[F]";
			}
			target.entries.Add(entry);
		}
		return target;
	}

    public static SignupList LoadPlayersListDB()
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        SignupList target = new SignupList();
        var slist =
            from pl in db.PlayersList
            orderby pl.TransDate
            select pl;
        int seqNo = 0;
        foreach (var item in slist)
        {
            seqNo++;
            SignupEntry entry = new SignupEntry()
            {
                SeqNo = seqNo,
                STDate = item.TransDate,
                SeventId = item.EventID,
                SPlayerID = item.PlayerID,
                Saction = item.Action,
                Scarpool = item.Carpool,
                Smarked = item.Marked,
                SspecialRule = item.SpecialRule,
                SGuest = item.GuestID,
                SSelected = false,
                SDelete = false
            };
            entry.Splayer = GetPlayerName(item.PlayerID);
            target.entries.Add(entry);
        }
        return target;
    }

    protected static string GetPlayerName(int id)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        var prm = db.Players.FirstOrDefault(p => p.PlayerID == id);
        if (prm == null) return "UNKNOWN";
        return prm.Name;
    }


	public static int CountPlayersActiveSignupEntries(int PlayerID)
	{
		int entryCount = 0;
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);

		var plist = 
			from p in db.PlayersList
			where (p.PlayerID == PlayerID)
			select new {p.PlayerID, p.EventID, p.Marked, p.TransDate};
		if (plist != null)
		{
			foreach (var entry in plist)
			{
				if (entry.Marked == 0) entryCount++;
			}
		}

		return entryCount;
	}

	public static void PurgeMarkedEntries()
	{
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);
		var markedEntries =
			from SignupEntries in db.PlayersList
			where SignupEntries.Marked > 0
			select SignupEntries;
		_entriesPurged = 0;
		if (markedEntries != null)
		{

			foreach (var entry in markedEntries)
			{
				db.PlayersList.DeleteOnSubmit(entry);
				_entriesPurged++;
			}
			db.SubmitChanges();
		}

	}

	public static int MarkedEntryCount()
	{
		int mc = 0;
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);
		var plist =
			from p in db.PlayersList
			where (p.Marked > 0)
			select p;
		if (plist != null)
		{
			foreach (var entry in plist)
			{
				if (entry.Marked > 0) mc++;
			}
		}
		return mc;
	}

	public SignupList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}