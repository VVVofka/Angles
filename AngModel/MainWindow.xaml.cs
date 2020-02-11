using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AngModel {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		bool needredraw = false;
		Model model;
		Line[] borders;// = new Border[]  {Border0, Border0,Border0,Border0,Border0,Border0};
		Point leftTop, rightBottom;
		public MainWindow() {
			InitializeComponent();
			model = new Model();
			borders = new Line[] { Border0, Border1, Border2, Border3, Border4, Border5 };
		} // //////////////////////////////////////////////////////////////////////////////////
		private void Window_Loaded(object sender, RoutedEventArgs e) {
			model.Shoot(0);
			ReDraw();
		} // ////////////////////////////////////////////////////////////////////////
		private void ReDraw() {
			setArea();
			DrawBorders();
			DrawBalls();
			DrawVectors();
		} // ///////////////////////////////////////////////////////////////////////////////
		private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
			ReDraw();
		} // ////////////////////////////////////////////////////////////////////////////////
		private double x(double value) {
			return value * (rightBottom.X - leftTop.X) / model.TableWidth;
		} // /////////////////////////////////////////////////////////////////////////////////
		private double y(double value) {
			return value * (rightBottom.Y - leftTop.Y) / model.TableHeigh;
		} // /////////////////////////////////////////////////////////////////////////////////
		private void grid.ActualHeight() {
			double ktable = model.TableWidth / model.TableHeigh;
			double k = grid.ActualWidth / grid.ActualHeight;
			double strip = 0;
			if(k > ktable) {    // стало более широко
				leftTop.Y = strip;
				leftTop.X = strip;
				rightBottom.Y = grid.ActualHeight - strip;
				rightBottom.X = ktable * (rightBottom.Y - leftTop.Y);
			} else {    // стало более высоко
				leftTop.X = strip;
				leftTop.Y = strip;
				rightBottom.X = grid.ActualWidth - strip;
				rightBottom.Y = (rightBottom.X - leftTop.X) / ktable;
			}
		} // //////////////////////////////////////////////////////////////////
		private void setArea() {
			double xszmin = Math.Min(model.activeLose.point1.X, model.activeLose.point2.X);
			double xszmax = Math.Max(model.activeLose.point1.X, model.activeLose.point2.X);
			xszmin = Math.Min(xszmin, model.ballCue.point.X - model.ballCue.R);
			xszmax = Math.Max(xszmax, model.ballCue.point.X + model.ballCue.R);

			double yszmin = Math.Min(model.activeLose.point1.Y, model.activeLose.point2.Y);
			double yszmax = Math.Max(model.activeLose.point1.Y, model.activeLose.point2.Y);
			yszmin = Math.Min(yszmin, model.ballCue.point.Y - model.ballCue.R);
			yszmax = Math.Max(yszmax, model.ballCue.point.Y + model.ballCue.R);

			double ktable = (xszmax - xszmin)/ (yszmax-yszmin);
			double k = grid.ActualWidth / grid.ActualHeight;
			double strip = 0;
			if(k > ktable) {    // стало более широко
				leftTop.Y = strip;
				leftTop.X = strip;
				rightBottom.Y = grid.ActualHeight - strip;
				rightBottom.X = ktable * (rightBottom.Y - leftTop.Y);
			} else {    // стало более высоко
				leftTop.X = strip;
				leftTop.Y = strip;
				rightBottom.X = grid.ActualWidth - strip;
				rightBottom.Y = (rightBottom.X - leftTop.X) / ktable;
			}
		} // //////////////////////////////////////////////////////////////////
		private void DrawBorders() {
			for(int i = 0; i < borders.Length; i++) {
				borders[i].X1 = x(model.loses.borders[i].x1);
				borders[i].X2 = x(model.loses.borders[i].x2);
				borders[i].Y1 = y(model.loses.borders[i].y1);
				borders[i].Y2 = y(model.loses.borders[i].y2);
			}
			PlayGround.Margin = new Thickness(0);
			PlayGround.Width = borders[5].X2;
			PlayGround.Height = borders[2].Y2;
			PlayGround.RadiusX = x(model.BallDiameter);
			PlayGround.RadiusY = y(model.BallDiameter);
		} // //////////////////////////////////////////////////////////////////
		private void DrawBalls() {
			DrawBall(gCueBall, model.ballCue);
			DrawBall(gTargetBall, model.ballTarget);
			DrawBall(gAimBall, model.ballAim);
		} // //////////////////////////////////////////////////////////////////
		private void DrawBall(Ellipse ellipse, Ball ball) {
			if(ball == null)
				return;
			if(ball.visible) {
				ellipse.Width = x(model.BallDiameter);
				ellipse.Height = y(model.BallDiameter);
				Thickness myThickness = new Thickness();
				myThickness.Left = y(ball.x) - ellipse.ActualWidth / 2;
				myThickness.Top = y(ball.y) - ellipse.ActualHeight / 2;
				ellipse.Margin = myThickness;
				ellipse.Visibility = Visibility.Visible;
			} else
				ellipse.Visibility = Visibility.Hidden;
		} // //////////////////////////////////////////////////////////////////
		private void DrawVectors() {
			DrawVector(gLineTargeCentr, model.CT);
			DrawVector(gLineTargeLeft, model.CT1);
			DrawVector(gLineTargeRight, model.CT2);
		} // /////////////////////////////////////////////////////////////////////
		private void txtBox_TextChanged(object sender, TextChangedEventArgs e) {
			var x = e.Source as TextBox;
			string value = x.Text;
			try {
				double result = Convert.ToDouble(value);
				//Console.WriteLine("Converted '{0}' to {1}.", value, result);
				model.Shoot(result);
				ReDraw();
			} catch(FormatException) {
				Console.WriteLine("Unable to convert '{0}' to a Double.", value);
			} catch(OverflowException) {
				Console.WriteLine("'{0}' is outside the range of a Double.", value);
			}
		} // //////////////////////////////////////////////////////////////////////////////
		private void Window_StateChanged(object sender, EventArgs e) {
			needredraw = true;
		} // //////////////////////////////////////////////////////////////////////////////
		private void Window_MouseMove(object sender, MouseEventArgs e) {
			if(needredraw) {
				needredraw = false;
				ReDraw();
			}
		} // ///////////////////////////////////////////////////////////////////////////////
		private void DrawVector(Line line, Vect vect) {
			if(vect == null)
				return;
			Ball ball = model.ballAim;
			if(ball == null)
				return;
			if(ball.visible) {
				line.X1 = x(vect.a.X);
				line.Y1 = y(vect.a.Y);
				line.X2 = x(vect.b.X);
				line.Y2 = y(vect.b.Y);
				line.Visibility = Visibility.Visible;
			} else
				line.Visibility = Visibility.Hidden;
		} // //////////////////////////////////////////////////////////////////
	} // ************************************************************************************
}
