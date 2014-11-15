using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ParamsList
/// </summary>
public class ParamsList
{
	private Collection<Param> _Params = new Collection<Param>();
	public Collection<Param> Params
	{
		get { return this._Params; }
	}
	public static ParamsList LoadParams()
	{
		string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
		MRMISGADB db = new MRMISGADB(MRMISGADBConn);

		ParamsList target = new ParamsList();
		var prm =
			from p in db.MRParams
			orderby p.Key
			select new { p.Key, p.Value, p.ChangeDate  };
		foreach (var item in prm)
		{
			Param newParam = new Param()
			{
				Key = item.Key,
				Value = item.Value,
				ChangeDate = item.ChangeDate
			};

			target.Params.Add(newParam);
		}

		return target;
	}
/*
	public static ParamsList GetValue(string key)
	{
	
	}
	*/
	public ParamsList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}