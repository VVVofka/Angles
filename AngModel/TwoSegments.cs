using System.Windows;
// http://grafika.me/node/362
namespace AngModel {
	class TwoSegments {
		struct ABC {
			public double A, B, C;//коэффициенты уравнения прямой вида: Ax+By+C=0
		} // *********************************************************************************
		ABC abc1, abc2;
		Point p1, p2, p3, p4;
		public TwoSegments(Point p_1, Point p_2, Point p_3, Point p_4) {
			set(p_1, p_2, p_3, p_4);
		} // ////////////////////////////////////////////////////////////////////////////////////
		public void set(Point p_1, Point p_2, Point p_3, Point p_4) {
			p1 = p_1;
			p2 = p_2;
			p3 = p_3;
			p4 = p_4;
			abc1 = LineEquation(p1, p2);
			abc2 = LineEquation(p3, p4);
		} // ////////////////////////////////////////////////////////////////////////////////////
		public bool areCrossing {
			get {   //проверка пересечения
				double v1 = vector_mult(p4.X - p3.X, p4.Y - p3.Y, p1.X - p3.X, p1.Y - p3.Y);
				double v2 = vector_mult(p4.X - p3.X, p4.Y - p3.Y, p2.X - p3.X, p2.Y - p3.Y);
				double v3 = vector_mult(p2.X - p1.X, p2.Y - p1.Y, p3.X - p1.X, p3.Y - p1.Y);
				double v4 = vector_mult(p2.X - p1.X, p2.Y - p1.Y, p4.X - p1.X, p4.Y - p1.Y);
				if((v1 * v2) < 0 && (v3 * v4) < 0)
					return true;
				return false;
			}
		} // //////////////////////////////////////////////////////////////////////////////
		public Point CrossingPoint {    //поиск точки пересечения
			get {
				Point pt=new Point();
				double d = abc1.A * abc2.B - abc1.B * abc2.A;
				double dx =-abc1.C * abc2.B + abc1.B * abc2.C;
				double dy =-abc1.A * abc2.C + abc1.C * abc2.A;
				pt.X = dx / d;
				pt.Y = dy / d;
				return pt;
			}
		} // //////////////////////////////////////////////////////////////////////////
		public bool areCrossingLine {
			get {   //проверка пересечения c отрезка р1,р2 с линией р3,р4
				Point p = CrossingPoint;
				return (p.X >= p1.X && p.Y >= p1.Y && p.X <= p2.X && p.Y <= p2.Y) ||
					(p.X <= p1.X && p.Y <= p1.Y && p.X >= p2.X && p.Y >= p2.Y);
			}
		} // //////////////////////////////////////////////////////////////////////////////
		  //построение уравнения прямой
		  //коэффициенты уравнения прямой вида: Ax+By+C=0
		private ABC LineEquation(Point p1, Point p2) {
			ABC abc = new ABC();
			abc.A = p2.Y - p1.Y;
			abc.B = p1.X - p2.X;
			abc.C = -p1.X * (p2.Y - p1.Y) + p1.Y * (p2.X - p1.X);
			return abc;
		} // ///////////////////////////////////////////////////////////////////////////////
		private double vector_mult(double ax, double ay, double bx, double by) {    //векторное произведение
			return ax * by - bx * ay;
		} // /////////////////////////////////////////////////////////////////////////
	} // **********************************************************************************
}
