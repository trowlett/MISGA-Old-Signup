using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Data.SqlTypes;

[Table (Name="Guest")]
public class Guest
{
    [Column (IsPrimaryKey=true)]
    public int GuestID;
    [Column]
    public string GuestName;
    [Column]
    public string gLname;
    [Column]
    public string gFname;
    [Column]
    public string gHcp;
    [Column]
    public int gSex;

}