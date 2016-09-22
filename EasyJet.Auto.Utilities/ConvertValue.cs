using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyJet.Auto.Utilities {

	public static class ConvertValue {

		public static double GetDoubleValue( string price ) {
			var value = price.Remove( 0, 1 );
			var returnValue = Double.Parse( value );
			return returnValue;
		}

	}
}
