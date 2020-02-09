using System.Windows;
namespace AngModel {
	class ABC {
		public double A, B, C;//коэффициенты уравнения прямой вида: Ax+By+C=0
	} // *********************************************************************************
	class TwoSegments {
		//построение уравнения прямой
		public ABC LineEquation(Point p1, Point p2) {
			ABC abc = new ABC();
			abc.A = p2.Y - p1.Y;
			abc.B = p1.X - p2.X;
			abc.C = -p1.X * (p2.Y - p1.Y) + p1.Y * (p2.X - p1.X);
			return abc;
		} // ///////////////////////////////////////////////////////////////////////////////
		  //поиск точки пересечения
		public Point CrossingPoint(double a1, double b1, double c1, double a2, double b2, double c2) {
			Point pt=new Point();
			double d=a1*b2-b1*a2;
			double dx=-c1*b2+b1*c2;
			double dy=-a1*c2+c1*a2;
			pt.X = dx / d;
			pt.Y = dy / d;
			return pt;
		} // //////////////////////////////////////////////////////////////////////////
		public Point CrossingPoint(ABC abc1, ABC abc2) {
			Point pt=new Point();
			double d = abc1.A * abc2.B - abc1.B * abc2.A;
			double dx =-abc1.C * abc2.B + abc1.B * abc2.C;
			double dy =-abc1.A * abc2.C + abc1.C * abc2.A;
			pt.X = dx / d;
			pt.Y = dy / d;
			return pt;
		} // //////////////////////////////////////////////////////////////////////////
		public bool areCrossing(Point p1, Point p2, Point p3, Point p4) {   //проверка пересечения
			double v1 = vector_mult(p4.X - p3.X, p4.Y - p3.Y, p1.X - p3.X, p1.Y - p3.Y);
			double v2 = vector_mult(p4.X - p3.X, p4.Y - p3.Y, p2.X - p3.X, p2.Y - p3.Y);
			double v3 = vector_mult(p2.X - p1.X, p2.Y - p1.Y, p3.X - p1.X, p3.Y - p1.Y);
			double v4 = vector_mult(p2.X - p1.X, p2.Y - p1.Y, p4.X - p1.X, p4.Y - p1.Y);
			if((v1 * v2) < 0 && (v3 * v4) < 0)
				return true;
			return false;
		} // //////////////////////////////////////////////////////////////////////////////
		private double vector_mult(double ax, double ay, double bx, double by) {    //векторное произведение
			return ax * by - bx * ay;
		} // /////////////////////////////////////////////////////////////////////////
	} // **********************************************************************************
}
