using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Angles {
	class mdSimpleCross : mdBase {
		public double angle1 { private set; get; }
		public double angle2 { private set; get; }
		private int znangle2 = 0;
		public mdSimpleCross(Canvas base_canvas, Line line_1, Line line_2) :
			base(base_canvas, line_1, line_2) {
		} // ////////////////////////////////////////////////////////////////////
		private void placeLine1(double Len1) {
			for(int j = 0; j < 99999; j++) {
				double rx = rnd.NextDouble();
				ln1.X1 = canvas.ActualWidth * rx;

				double ry = rnd.NextDouble();
				ln1.Y1 = canvas.ActualHeight * ry;

				double ra = rnd.NextDouble();
				angle1 = 2 * Math.PI * ra;

				ln1.X2 = ln1.X1 + Len1 * Math.Sin(angle1);
				ln1.Y2 = ln1.Y1 + Len1 * Math.Cos(angle1);

				if(ln1.X2 <= 2 || ln1.X2 + 2 >= canvas.ActualWidth ||
					ln1.Y2 <= 2 || ln1.Y2 + 2 >= canvas.ActualHeight)
					continue;

				break;
			}
		} // ///////////////////////////////////////////////////////////////////
		public void placeLine2(double Len1, double Len2, double shift_angle_grade,
								double k_intersec1, double k_intersec2) {
			double shiftangle = 2 * Math.PI * shift_angle_grade / 360;
			for(int j = 0; j < 99999; j++) {
				placeLine1(Len1);
				znangle2 = 2 * rnd.Next(2) - 1;
				angle2 = angle1 + znangle2 * shiftangle;

				if(rnd.Next(2) == 0)
					k_intersec1 = 1 - k_intersec1;
				double xcross = ln1.X1 * k_intersec1 + ln1.X2 * (1 - k_intersec1);
				double ycross = ln1.Y1 * k_intersec1 + ln1.Y2 * (1 - k_intersec1);

				if(rnd.Next(2) == 0)
					k_intersec2 = 1 - k_intersec2;
				ln2.X1 = xcross + Len2 * k_intersec2 * Math.Sin(angle2);
				ln2.Y1 = ycross + Len2 * k_intersec2 * Math.Cos(angle2);
				ln2.X2 = xcross + Len2 * (1 - k_intersec2) * Math.Sin(angle2 + Math.PI);
				ln2.Y2 = ycross + Len2 * (1 - k_intersec2) * Math.Cos(angle2 + Math.PI);
				Console.WriteLine("{0}x{1}   {2}x{3} {4}x{5}   {6}x{7} {8}x{9} {10}",
					canvas.ActualWidth, canvas.ActualHeight,
					(int)ln1.X1, (int)ln1.Y1, (int)ln1.X2, (int)ln1.Y2,
					(int)ln2.X1, (int)ln2.Y1, (int)ln2.X2, (int)ln2.Y2, j
					);
				if(ln2.X1 <= 0 || ln2.X1 >= canvas.ActualWidth ||
					ln2.Y1 <= 0 || ln2.Y1 >= canvas.ActualHeight ||
					ln2.X2 <= 0 || ln2.X2 >= canvas.ActualWidth ||
					ln2.Y2 <= 0 || ln2.Y2 >= canvas.ActualHeight)
					continue;
				break;
			}
		} // ///////////////////////////////////////////////////////////////////
	} // **************************************************************************************************
}
