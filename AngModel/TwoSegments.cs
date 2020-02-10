using System;
using System.Windows;
// http://grafika.me/node/362
namespace AngModel {
	class Vect {
		struct ABC {
			public double A, B, C;//коэффициенты уравнения прямой вида: Ax+By+C=0
		} // *********************************************************************************
		public Point a,b;
		public Vect(Point A, Point B) {
			a = A;
			b = B;
		} // ////////////////////////////////////////////////////////////////////////
		public Vect(double x1, double y1, double x2, double y2) {
			a = new Point(x1, y1);
			b = new Point(x2, y2);
		} // ////////////////////////////////////////////////////////////////////////
		static public double len2(Point p1, Point p2) { return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y)); }
		public double len { get { return Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y)); } }
		public double radians { get { return Math.Atan2(b.Y - a.Y, b.X - a.X); } }
		public double degree { get { return radians * (180 / Math.PI); } }
		public Point pointByKLen(double k) {
			return new Point(a.X + k * (b.X - a.X), a.Y + k * (b.Y - a.Y));
		} // ///////////////////////////////////////////////////////////////////////////////////
		public Point pointByLen(double lenght) {
			double l = len;
			if(l == 0)
				return a;
			return pointByKLen(lenght / l);
		} // ///////////////////////////////////////////////////////////////////////////////////
		public Point pointByProj(Point point) {
			if(len == 0)
				return a;
			double l2 = len2(b, point);
			if(l2 == 0)
				return b;
			double l1 = len2(a, point);
			return pointByKLen(l1 / l2);
		} // ///////////////////////////////////////////////////////////////////////////////////
		public Vect getShift(double shift_x, double shift_y) {
			double sina = (b.Y - a.Y) / (b.X - a.X);
			double cosa = 1 / sina;
			Point newa = new Point(a.X + shift_x * cosa, a.Y + shift_y * sina);
			Point newb = new Point(b.X + shift_x * cosa, b.Y + shift_y * sina);
			return new Vect(newa, newb);
		} // ////////////////////////////////////////////////////////////////////////////////////
		public Point getPointCrossLine(Vect other_vect) {    //поиск точки пересечения линий
			ABC abc1 =LineEquation(a, b);
			ABC abc2 =LineEquation(other_vect.a, other_vect.b);
			double d = abc1.A * abc2.B - abc1.B * abc2.A;
			double dx =-abc1.C * abc2.B + abc1.B * abc2.C;
			double dy =-abc1.A * abc2.C + abc1.C * abc2.A;
			return new Point(dx / d, dy / d);
		} // //////////////////////////////////////////////////////////////////////////
		public bool isPossess(Point point) {    // точка принадлежит отрезку
			bool bx = (point.X >= a.X && point.X <= b.X) ||
				(point.X >= b.X && point.X <= a.X);
			bool by = (point.Y >= a.Y && point.Y <= b.Y) ||
				(point.Y >= b.Y && point.Y <= a.Y);
			return bx & by;
		} // ///////////////////////////////////////////////////////////////////////////////////
		  //построение уравнения прямой
		  //коэффициенты уравнения прямой вида: Ax+By+C=0
		private ABC LineEquation(Point p1, Point p2) {
			ABC abc = new ABC();
			abc.A = p2.Y - p1.Y;
			abc.B = p1.X - p2.X;
			abc.C = -p1.X * (p2.Y - p1.Y) + p1.Y * (p2.X - p1.X);
			return abc;
		} // ///////////////////////////////////////////////////////////////////////////////
	} // ****************************************************************************
	//class TwoSegments {
	//	struct ABC {
	//		public double A, B, C;//коэффициенты уравнения прямой вида: Ax+By+C=0
	//	} // *********************************************************************************
	//	ABC abc1, abc2;
	//	Point p1, p2, p3, p4;
	//	public Point crossPoint { get; private set; }
	//	public TwoSegments(Point p_1, Point p_2, Point p_3, Point p_4) {
	//		set(p_1, p_2, p_3, p_4);
	//	} // ////////////////////////////////////////////////////////////////////////////////////
	//	public void set(Point p_1, Point p_2, Point p_3, Point p_4) {
	//		p1 = p_1;
	//		p2 = p_2;
	//		p3 = p_3;
	//		p4 = p_4;
	//		abc1 = LineEquation(p1, p2);
	//		abc2 = LineEquation(p3, p4);
	//		crossPoint = CrossingPoint();
	//	} // ////////////////////////////////////////////////////////////////////////////////////
	//	public bool isCrossing {
	//		get {   //проверка пересечения
	//			double v1 = vector_mult(p4.X - p3.X, p4.Y - p3.Y, p1.X - p3.X, p1.Y - p3.Y);
	//			double v2 = vector_mult(p4.X - p3.X, p4.Y - p3.Y, p2.X - p3.X, p2.Y - p3.Y);
	//			double v3 = vector_mult(p2.X - p1.X, p2.Y - p1.Y, p3.X - p1.X, p3.Y - p1.Y);
	//			double v4 = vector_mult(p2.X - p1.X, p2.Y - p1.Y, p4.X - p1.X, p4.Y - p1.Y);
	//			return ((v1 * v2) < 0 && (v3 * v4) < 0);
	//		}
	//	} // //////////////////////////////////////////////////////////////////////////////
	//	public bool isCrossingLine {
	//		get {   //проверка пересечения c отрезка р1,р2 с линией р3,р4
	//			return (crossPoint.X >= p1.X && crossPoint.Y >= p1.Y && crossPoint.X <= p2.X && crossPoint.Y <= p2.Y) ||
	//				(crossPoint.X <= p1.X && crossPoint.Y <= p1.Y && crossPoint.X >= p2.X && crossPoint.Y >= p2.Y);
	//		}
	//	} // //////////////////////////////////////////////////////////////////////////////
	//	  //построение уравнения прямой
	//	  //коэффициенты уравнения прямой вида: Ax+By+C=0
	//	private ABC LineEquation(Point p1, Point p2) {
	//		ABC abc = new ABC();
	//		abc.A = p2.Y - p1.Y;
	//		abc.B = p1.X - p2.X;
	//		abc.C = -p1.X * (p2.Y - p1.Y) + p1.Y * (p2.X - p1.X);
	//		return abc;
	//	} // ///////////////////////////////////////////////////////////////////////////////
	//	private double vector_mult(double ax, double ay, double bx, double by) {    //векторное произведение
	//		return ax * by - bx * ay;
	//	} // /////////////////////////////////////////////////////////////////////////
	//	private Point CrossingPoint() {    //поиск точки пересечения линий
	//		double d = abc1.A * abc2.B - abc1.B * abc2.A;
	//		double dx =-abc1.C * abc2.B + abc1.B * abc2.C;
	//		double dy =-abc1.A * abc2.C + abc1.C * abc2.A;
	//		return new Point(dx / d, dy / d);
	//	} // //////////////////////////////////////////////////////////////////////////
	//} // **********************************************************************************
}
