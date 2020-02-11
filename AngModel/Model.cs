using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AngModel {
	class Model {
		// All in milimeter
		public double BallDiameter;     // = 68.0;
		public double TableWidth;       // = 3550.0;
		public double TableHeigh;       // = 1775.0;
		public double LoseCornerWidth;  // = 72.0;
		public double LoseCenterWidth;  // = 82.0;

		public Ball ballCue;
		public Ball ballTarget;
		public Ball ballAim;
		public Point pointAim = new Point(-1,-1);
		public Loses loses;
		public Lose activeLose { get; private set; }
		public Vect CT1 { get; private set; }
		public Vect CT { get; private set; }
		public Vect CT2 { get; private set; }
		public Model() {
			TableWidth = Convert.ToDouble(Properties.Resources.TableWidth);
			TableHeigh = Convert.ToDouble(Properties.Resources.TableHeigh);
			LoseCornerWidth = Convert.ToDouble(Properties.Resources.LoseCornerWidth);
			LoseCenterWidth = Convert.ToDouble(Properties.Resources.LoseCenterWidth);
			loses = new Loses(LoseCornerWidth, LoseCenterWidth, TableWidth, TableHeigh);
			setActiveLose(0);

			BallDiameter = Convert.ToDouble(Properties.Resources.BallDiameter);
			ballCue = new Ball(BallDiameter);
			ballTarget = new Ball(BallDiameter);
			ballAim = new Ball(BallDiameter);
			setBallTarget(2 * BallDiameter, 2 * BallDiameter);
			setBallCue(TableWidth / 4, TableHeigh / 2);
			//for(double j = -0.1; j <= 0.1; j += 0.01)
				//Console.WriteLine("{0} :{1}", j, Shoot(j));
		} // //////////////////////////////////////////////////////////////////////
		public Lose setActiveLose(int index) { return activeLose = loses[index]; }
		public void setBallCue(double X, double Y) { ballCue.set(X, Y); ballCue.visible = true; }
		public void setBallTarget(double X, double Y) { ballTarget.set(X, Y); ballTarget.visible = true; }
		public bool Shoot(double k) {  //k=-1...+1
			double lenAT = ballTarget.R * k;
			double lenTO = Vect.len2(ballTarget.point, ballCue.point);
			double lenAO = Math.Sqrt(lenTO * lenTO - lenAT * lenAT);
			double alfa = Math.Asin((ballCue.x - ballTarget.x)/lenTO);
			double betta = Math.Asin(lenAT / lenTO);
			double gamma = alfa - betta;
			pointAim.X = ballCue.x - lenAO * Math.Sin(gamma);
			pointAim.Y = ballCue.y - lenAO * Math.Cos(gamma);
			double tmp = ballTarget.D * ballTarget.D - lenAT * lenAT;
			ballAim.visible = (tmp > 0);
			if(!ballAim.visible) {
				return false;
			}
			double lenAC = Math.Sqrt( tmp);
			Vect AO = new Vect(pointAim, ballCue.point);
			ballAim.point = AO.pointByLen(lenAC);
			CT = new Vect(ballAim.point, ballTarget.point);
			CT1 = CT.getShiftSide(-ballAim.R);
			CT2 = CT.getShiftSide(+ballAim.R);
			//Console.WriteLine($"CT1: {CT1.a}\t {CT1.b}\nCT : {CT.a}\t {CT.b}\nCT2: {CT2.a}\t{CT2.b}");
			Point result = activeLose.vect.getPointCrossLine(CT);
			Point result1 = activeLose.vect.getPointCrossLine(CT1);
			Point result2 = activeLose.vect.getPointCrossLine(CT2);
			CT.a = CT.b;
			CT1.a = CT1.b;
			CT2.a = CT2.b;
			CT.b = result;
			CT1.b = result1;
			CT2.b = result2;
			bool bres1 = activeLose.vect.isPossess(result1);
			bool bres2 = activeLose.vect.isPossess(result2);
			// check direction
			Vect CL = new Vect(ballAim.point, result);
			bool bres = CL.isPossess(ballTarget.point);
			return (bres1 && bres2) && bres;
		} // ///////////////////////////////////////////////////////////////////////
	} // ***************************************************************************
}
