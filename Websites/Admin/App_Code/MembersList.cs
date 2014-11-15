using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for MemberList
/// </summary>
public class MembersList
{

	private Collection<MrMember> members = new Collection<MrMember>();

	public Collection<MrMember> Members
	{
		get
		{
			return this.members;
		}
	}

	public string FileName { get; private set; }

	public DateTime CreateTime { get; private set; }

    public static int Count {get; set;}
    private static int _count {get; set;}

	private static string _MembersName = "";
	public static string MembersName = "";
	public static MembersList LoadMembers()
	{
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);

		MembersList target = new MembersList();
		var memb =
			from p in db.Players
			orderby p.Name
			select p;
        _count = 0;
		foreach (var item in memb)
		{
			MrMember newMember = new MrMember()
			{
				pID = item.PlayerID,
				name = item.Name,
				fname = item.FName,
				lname = item.LName,
				gender = item.Sex,
				hcp = item.Hcp,
				memberNumber = item.MemberID,
				title = item.Title,
				hdate = item.HDate,
                del = item.Delete
			};
			newMember.active = SignupList.CountPlayersActiveSignupEntries(item.PlayerID);
			target.Members.Add(newMember);
            _count++;
		}
        Count = _count;
		return target;
	}

	public static int DeleteMember(int playerID)
	{
		int err = 1;                                                // set result to not deleted
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);
		var pl = db.Players.Single(p => p.PlayerID == playerID);
		_MembersName = pl.Name;
		if (SignupList.CountPlayersActiveSignupEntries(playerID) == 0)    // delete is active records is zero
		{
			db.Players.DeleteOnSubmit(pl);
			var deleteSignupEntries = 
				from SignupEntries in db.PlayersList
				where SignupEntries.PlayerID == playerID
				select SignupEntries;
			if (deleteSignupEntries != null)
			{
				foreach (var SignupEntry in deleteSignupEntries)
				{
					// delete all signup records for this member
					db.PlayersList.DeleteOnSubmit(SignupEntry);                  
				}
			db.SubmitChanges();
			err = 0;                                                // set result to deleted
			}

		}
		return err;
	}


	public MembersList()
	{
        _count = 0;
		//
		// TODO: Add constructor logic here
		//
	}
}