using System;
using System.Windows;

namespace AngModel {
	class DegRad {
		public double rad { get; set; } = 0;
		public double degr {
			get { return 180 * rad / Math.PI; }
			set { rad = Math.PI * value / 180; }
		} // /////////////////////////////////////////////////////////////////
		public double set(double tangent) {
			return rad = Math.Atan(tangent);
		} // //////////////////////////////////////////////////////////////////
		public double set(Point point1) {
			return set(new Point(0, 0), point1);
		} // //////////////////////////////////////////////////////////////////
		public double set(Point point1, Point point2) {
			return rad = Math.Atan2(point2.X - point1.X, point2.Y - point1.Y);
		} // //////////////////////////////////////////////////////////////////
		public double set(double x, double y) {
			return set(new Point(x, y));
		} // /////////////////////////////////////////////////////////////////////////////
		public double set(double x1, double y1, double x2, double y2) {
			return set(new Point(x1, y1), new Point(x2, y2));
		} // /////////////////////////////////////////////////////////////////////////////
	} // *****************************************************************************
}
