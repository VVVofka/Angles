using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Angles {
	class mdSimpleCross : mdBase {
		public mdParamsTwoLine Params;
		public double angle1 { private set; get; }
		public double angle2 { private set; get; }
		public double angle3 { private set; get; }
		public double degr1 { get { return (180 * angle1 / Math.PI) % 360; } }
		public double degr2 { get { return (180 * angle2 / Math.PI) % 360; } }
		public double degr3 { get { return (180 * angle3 / Math.PI) % 360; } }
		public double xcross { private set; get; }
		public double ycross { private set; get; }
		public double len1 { private set; get; }
		public double len2 { private set; get; }
		private int znangle2 = 0;
		public mdSimpleCross(Canvas base_canvas, Line line_1, Line line_2, Line line_3) :
			base(base_canvas, line_1, line_2, line_3) {
			Params = new mdParamsTwoLine();
		} // ////////////////////////////////////////////////////////////////////
		private void placeLine1() {
			for(int j = 0; j < 99999; j++) {
				len1 = CanvasMinSize * Params.ln1.kLen.val(rnd);
				double rx = rnd.NextDouble();
				ln1.X1 = canvas.ActualWidth * rx;

				double ry = rnd.NextDouble();
				ln1.Y1 = canvas.ActualHeight * ry;

				double ra = rnd.NextDouble();
				angle1 = 2 * Math.PI * ra;

				ln1.X2 = ln1.X1 + len1 * Math.Cos(angle1);
				ln1.Y2 = ln1.Y1 + len1 * Math.Sin(angle1);

				if(!(ln1.X2 <= 2 || ln1.X2 + 2 >= canvas.ActualWidth ||
					ln1.Y2 <= 2 || ln1.Y2 + 2 >= canvas.ActualHeight))
					break;
			}
		} // ///////////////////////////////////////////////////////////////////
		public void placeLine2(double shift_angle_grade) {
			double shiftangle = 2 * Math.PI * shift_angle_grade / 360;
			for(int j = 0; j < 9999; j++) {
				len2 = CanvasMinSize * Params.ln2.kLen.val(rnd);
				placeLine1();
				znangle2 = 2 * rnd.Next(2) - 1;
				angle2 = (2 * Math.PI + angle1 + znangle2 * shiftangle) % (2 * Math.PI);
				double k_intersec1 = Params.ln1.kIntersect.val(rnd);
				xcross = ln1.X2 * k_intersec1 + ln1.X1 * (1 - k_intersec1);
				ycross = ln1.Y2 * k_intersec1 + ln1.Y1 * (1 - k_intersec1);

				double k_intersec2 = Params.ln2.kIntersect.val(rnd);
				ln2.X1 = xcross - len2 * k_intersec2 * Math.Cos(angle2);
				ln2.Y1 = ycross - len2 * k_intersec2 * Math.Sin(angle2);
				ln2.X2 = xcross + len2 * (1-k_intersec2) * Math.Cos(angle2);
				ln2.Y2 = ycross + len2 * (1-k_intersec2) * Math.Sin(angle2);
				Console.WriteLine("{0}x{1}   {2}x{3} {4}x{5}   {6}x{7} {8}x{9} {10}x{11} {12} {13} {14}",
					canvas.ActualWidth, canvas.ActualHeight,
					(int)ln1.X1, (int)ln1.Y1, (int)ln1.X2, (int)ln1.Y2,
					(int)ln2.X1, (int)ln2.Y1, (int)ln2.X2, (int)ln2.Y2,
					(int)xcross, (int)ycross,
					(int)degr1, (int)degr2, j
					);
				if(ln2.X1 <= 0 || ln2.X1 >= canvas.ActualWidth ||
					ln2.Y1 <= 0 || ln2.Y1 >= canvas.ActualHeight ||
					ln2.X2 <= 0 || ln2.X2 >= canvas.ActualWidth ||
					ln2.Y2 <= 0 || ln2.Y2 >= canvas.ActualHeight)
					continue;
				break;
			}
		} // ///////////////////////////////////////////////////////////////////
		public void placeLine3(double shift_angle_grade) {
			if(shift_angle_grade == 0)
				return;
			double shiftangle = 2 * Math.PI * shift_angle_grade / 360;
			angle3 = angle1 + znangle2 * shiftangle;
			//ln3.X1 = xcross;
			//ln3.Y1 = ycross;
			ln3.X1 = ln1.X1;
			ln3.Y1 = ln1.Y1;
			ln3.X2 = ln1.X2;
			ln3.Y2 = ln1.Y2;
			RotateTransform myRotateTransform3 = new RotateTransform(degr3 - degr1, xcross, ycross);
			ln3.RenderTransform = myRotateTransform3;

			Console.WriteLine("{0}x{1}   {2}x{3} {4}x{5}   {6}x{7} {8}x{9}",
				canvas.ActualWidth, canvas.ActualHeight,
				(int)ln3.X1, (int)ln3.Y1, (int)ln3.X2, (int)ln3.Y2,
				(int)ln2.X1, (int)ln2.Y1, (int)ln2.X2, (int)ln2.Y2);
		} // ////////////////////////////////////////////////////////////////////////
	} // **************************************************************************************************
}
