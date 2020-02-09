
namespace AngModel{
	class Ball {
		public double x { get; private set; }
		public double y { get; private set; }
		double radius;
		public Ball(double diameter) { radius = diameter / 2; }
		public double R {   // Radius
			get { return radius; }
		}
		public double D {   // Diameter
			get { return radius * 2; }
		}
		public void set(double X, double Y) { x = X; y = Y; }
	} // ******************************************************************************
}
