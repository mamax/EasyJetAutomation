using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyJet.Auto.Utilities {

	public class WaitHelper {

		public static bool Try( Action action ) {
			Exception exception;

			return Try( action, out exception );
		}

		public static bool Try( Action action, out Exception exception ) {
			try {
				action();
				exception = null;

				return true;
			} catch( Exception e ) {
				exception = e;

				return false;
			}
		}
	}
}
