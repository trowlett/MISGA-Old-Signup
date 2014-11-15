using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

/// <summary>
/// Summary description for MRMISGADB
/// </summary>
public class MISGACLUBS : DataContext
{
    public Table<Clubs> Clubs;
    public MISGACLUBS(string connection) : base(connection) { }
}