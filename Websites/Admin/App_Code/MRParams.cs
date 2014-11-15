using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for MRParams
/// </summary>
[Table(Name = "MRParams")]
public class MRParams
{
    [Column(IsPrimaryKey = true)]
    public string Key { get; set; }

    [Column]
    public string Value { get; set; }

    [Column]
    public DateTime ChangeDate { get; set; }
}