using System;

namespace EasyJet.Auto.Utilities {

	public static class DataTimeHelper {

		public static DateTime AddDaysToCurrentDate( int day ) {
			return GetCurrentDataTime().AddDays( day );
		}
			
		private static DateTime GetCurrentDataTime() {
			return DateTime.Now;
		}

		public static DateTime GetFridayDate() {
			var date = DataTimeHelper.GetCurrentDataTime();

			if( date.DayOfWeek != DayOfWeek.Friday ) {

				for( int i = 1; i < 7; i++ ) {

					date = date.AddDays( 1 );
					if( date.DayOfWeek == DayOfWeek.Friday ) {
						break;
					}
				}
			}
			return date;
		}

	}

}
