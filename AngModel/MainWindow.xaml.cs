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
		Model model;
		Line[] borders;// = new Border[]  {Border0, Border0,Border0,Border0,Border0,Border0};
		Point leftTop, rightBottom;
		public MainWindow() {
			InitializeComponent();
			model = new Model();
			borders = new Line[] { Border0, Border1, Border2, Border3, Border4, Border5 };
		} // //////////////////////////////////////////////////////////////////////////////////
		private void Window_Loaded(object sender, RoutedEventArgs e) {
			ReDraw();
		} // ////////////////////////////////////////////////////////////////////////
		private void ReDraw() {
			setArea();
			DrawBorders();
			DrawBalls();
		} // ///////////////////////////////////////////////////////////////////////////////
		private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
			ReDraw();
		} // ////////////////////////////////////////////////////////////////////////////////
		private double x(double value) {
			return value * (rightBottom.X - leftTop.X ) / model.TableWidth;
		} // /////////////////////////////////////////////////////////////////////////////////
		private double y(double value) {
			return value * (rightBottom.Y - leftTop.Y ) / model.TableHeigh;
		} // /////////////////////////////////////////////////////////////////////////////////
		private void setArea() {
			double ktable = model.TableWidth / model.TableHeigh;
			double k = grid.ActualWidth / grid.ActualHeight;
			double strip = 1;
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
			Thickness myThickness = new Thickness();
			myThickness.Left = 0;
			myThickness.Right = 0;
			myThickness.Top = 0;
			myThickness.Bottom = 0;
			PlayGround.Margin = myThickness;
			PlayGround.Width = borders[5].X2;
			PlayGround.Height = borders[2].Y2;
			PlayGround.RadiusX =x(model.BallDiameter);
			PlayGround.RadiusY = y(model.BallDiameter);
		} // //////////////////////////////////////////////////////////////////
		private void DrawBalls() {
			DrawBall(gCueBall, model.ballCue);
			DrawBall(gTargetBall, model.ballTarget);
			DrawBall(gAimBall, model.ballAim);
		} // //////////////////////////////////////////////////////////////////
		private void DrawBall(Ellipse ellipse, Ball ball) {
			ellipse.Width = x(model.BallDiameter);
			ellipse.Height = y(model.BallDiameter);
			Thickness myThickness = new Thickness();
			myThickness.Left = y(ball.x) - ellipse.ActualWidth / 2;
			myThickness.Top = y(ball.y) - ellipse.ActualHeight / 2;
			ellipse.Margin = myThickness;
		} // //////////////////////////////////////////////////////////////////
	} // ************************************************************************************
}
