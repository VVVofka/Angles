
using System.Windows;

namespace AngModel {
	class Lose {
		readonly double width;
		public Point point1 { get { return vect.a; } }
		public Point point2 { get { return vect.b; } }
		readonly Vect vect;
		string description;
		public Lose(double Width, 
					double X0, double Y0, 
					double X1, double Y1,
					string Description) {
			width = Width;
			vect = new Vect(new Point(X0, Y0), new Point(X1, Y1));
			description = Description;
		} // ////////////////////////////////////////////////////////////////////////
	} // ****************************************************************************
}
