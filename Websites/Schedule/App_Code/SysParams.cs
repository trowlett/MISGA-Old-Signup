using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for SysParams
/// </summary>
[Table(Name = "Params")]
public class SysParams
{
    [Column(IsPrimaryKey = true)]
    public string Key { get; set; }

    [Column]
    public string Value { get; set; }

    [Column]
    public DateTime ChangeDate { get; set; }
}