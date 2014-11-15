using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Player Event Definition for displaying Events a players has signed up for
/// </summary>
public class PrEvent
{
	public string PrEvID { get; set; }          // Event ID
	public DateTime PrDate { get; set; }        // Event Date
	public string PrType { get; set; }          // Event Type
	public string PrTitle { get; set; }         // Event Title
	public string PrCost { get; set; }          // Event Cost
	public string PrTime { get; set; }          // Event Time
	public DateTime PrDeadline { get; set; }    // Signup Deadline
	public string PrHostPhone { get; set; }     // Host Club Phone Number
	public string PrCarpool { get; set; }       // Players carpool desire
	public bool PrCompleted { get; set; }       // true if event completed

	public PrEvent()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}