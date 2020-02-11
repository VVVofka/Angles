
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AngModel {
	class Lose {
		readonly double width;
		public Point point1 { get { return vect.a; } }
		public Point point2 { get { return vect.b; } }
		public readonly Vect vect;
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
	class Border {
		Point a;
		Point b;
		public Border() { }
		public Border(Point A, Point B) {
			a = A; b = B;
		}
		public Border(double x1, double y1, double x2, double y2) {
			a.X = x1;
			a.Y = y1;
			b.X = x2;
			b.Y = y2;
		} // //////////////////////////////////////////////////////////////
		public double x1 { get { return a.X; } }
		public double y1 { get { return a.Y; } }
		public double x2 { get { return b.X; } }
		public double y2 { get { return b.Y; } }
	} // *****************************************************************

}
