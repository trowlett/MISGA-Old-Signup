using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for Access Control Security
/// </summary>
public class AccessControl
{
    private const string controlCode = "4972";

    public static bool IsValidMember(string LastName, string FirstName, string AccessCode)
    {
        return AccessCode == controlCode ? true : false;    
    }
	public AccessControl()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}