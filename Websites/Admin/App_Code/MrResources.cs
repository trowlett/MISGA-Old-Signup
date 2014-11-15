using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for MrResources
/// </summary> 
public class MrResources
{
	public string Root { get { return _root; } set { _root = value; } }    
	public string Path { get { return _path; } set { _path = value; } }
	private string _path;
	private string _root;
//	public string Root { get { return _root; } set { _root = value; } }
//	private string _root;

	public MrResources()
	{
		//
		// TODO: Add constructor logic here
		//
		_root = ConfigurationManager.AppSettings["RootDirectory"];

	}
}