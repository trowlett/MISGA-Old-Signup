using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for Clubs
/// </summary>
[Table(Name="Clubs")]
public class Clubs
{
	[Column(IsPrimaryKey = true)]
	public string ClubID;
    [Column]
    public string ClubName;
    [Column]
    public string MISGAURL;
    [Column]
    public string ProName;
    [Column]
    public string ProEmail;
    [Column]
    public string ProPhone;
    [Column]
    public string ProFax;
    [Column]
    public string RepName;
    [Column]
    public string RepEmail;
    [Column]
    public string RepPhone;
    [Column]
    public string PayOpt;
    [Column]
    public string Refresh;
    [Column]
    public string SRule;
    [Column]
    public string OtherRule;
    [Column]
    public string Misc;
    [Column]
    public string slope;

	public Clubs()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}