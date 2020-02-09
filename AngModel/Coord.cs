using System;

namespace AngModel {
	class Coord {
		public double x = 0;
		public double y = 0;
		public int ix { get { return Convert.ToInt32(x); } }
		public int iy { get { return Convert.ToInt32(y); } }
	} // *****************************************************************************
}
