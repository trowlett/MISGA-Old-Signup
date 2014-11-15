using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

/// <summary>
/// Summary description for MRMISGADB
/// </summary>
public class MRMISGADB : DataContext
{
    public Table<MRParams> MRParams;
    public Table<Players> Players;
    public Table<PlayersList> PlayersList;
    public Table<Events> Events;
    public Table<Clubs> Clubs;
    public Table<Guest> Guest;
    public Table<SysParams> SysParams;
    public MRMISGADB(string connection) : base(connection) { }
}