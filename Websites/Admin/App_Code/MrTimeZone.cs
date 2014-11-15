using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;

/// <summary>
/// Summary description for MrTimeZone
/// </summary>
public class MrTimeZone
{
	public DateTime eastTimeNow()
	{
		DateTime timeNow = DateTime.Now;
//        string ans = "";
//       try
//        {
			TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
			DateTime easternTimeNow = TimeZoneInfo.ConvertTime(timeNow, TimeZoneInfo.Local,
															easternZone);

			//		ans = String.Format("{0} {1} corresponds to {2} {3}.",
			//						  timeNow,
			//						  TimeZoneInfo.Local.IsDaylightSavingTime(timeNow) ?
			//									TimeZoneInfo.Local.DaylightName :
			//									TimeZoneInfo.Local.StandardName,
			//						  easternTimeNow,
			//						  easternZone.IsDaylightSavingTime(easternTimeNow) ?
			//									  easternZone.DaylightName :
			//									  easternZone.StandardName); 
			return easternTimeNow;
//        }
		// Handle exception
		//
		// As an alternative to simply displaying an error message, an alternate Eastern
		// Standard Time TimeZoneInfo object could be instantiated here either by restoring
		// it from a serialized string or by providing the necessary data to the
		// CreateCustomTimeZone method.
		//		catch (TimeZoneNotFoundException)
		//		{
		//			ans = string.Format("The Eastern Standard Time Zone cannot be found on the local system.");
		//			return timeNow;
		//		}
		//		catch (InvalidTimeZoneException)
		//		{
		//			ans = String.Format("The Eastern Standard Time Zone contains invalid or missing data.");
		//			return timeNow;
		//		}
		//		catch (SecurityException)
		//		{
		//			ans = string.Format("The application lacks permission to read time zone information from the registry.");
		//			return timeNow;
		//		}
		//		catch (OutOfMemoryException)
		//		{
		//			ans = string.Format("Not enough memory is available to load information on the Eastern Standard Time zone.");
		//			return timeNow;
		//		}
		// If we weren't passing FindSystemTimeZoneById a literal string, we also 
		// would handle an ArgumentNullException.
	}
	public MrTimeZone()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}