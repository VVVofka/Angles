using System.Windows;
namespace AngModel {
	class Ball {
		public Point point = new Point(-1,-1);
		public double x { get { return point.X; } set { point.X = value; } }
		public double y { get { return point.Y; } set { point.Y = value; } }
		double radius;
		public Ball(double diameter) { radius = diameter / 2; }
		public double R { get { return radius; } }   // Radius
		public double D { get { return radius * 2; } }   // Diameter

		public void set(double X, double Y) { x = X; y = Y; }
	} // ******************************************************************************
}
