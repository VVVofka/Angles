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

		Ball ballCue;
		Ball ballTarget;
		Ball ballAim;
		Point pointAim = new Point(-1,-1);
		Loses loses;
		public Lose activeLose { get; private set; }
		public Point result1 { get; private set; }
		public Point result { get; private set; }
		public Point result2 { get; private set; }

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
			for(double j = -1; j < 1; j += 0.1)
				Console.WriteLine("{0} :{1}", j, Shoot(j));
		} // //////////////////////////////////////////////////////////////////////

		public Lose setActiveLose(int index) { return activeLose = loses[index]; }
		public void setBallCue(double X, double Y) { ballCue.set(X, Y); }
		public void setBallTarget(double X, double Y) { ballTarget.set(X, Y); }
		public bool Shoot(double k) {  //k=-1...+1
			double lenAT = ballTarget.R * k;
			double lenTO = Vect.len2(ballTarget.point, ballCue.point);
			double lenAO = Math.Sqrt(lenTO * lenTO - lenAT * lenAT);
			double alfa = Math.Asin((ballCue.x - ballTarget.x)/lenTO);
			double betta = Math.Asin(lenAT / lenTO);
			double gamma = alfa - betta;
			pointAim.X = ballCue.x - lenAO * Math.Sin(gamma);
			pointAim.Y = ballCue.y - lenAO * Math.Cos(gamma);
			double lenAC = Math.Sqrt( ballTarget.D * ballTarget.D - lenAT * lenAT);
			Vect AO = new Vect(pointAim, ballCue.point);
			ballAim.point = AO.pointByLen(lenAC);
			Vect CT = new Vect(ballAim.point, ballTarget.point);
			Vect CT1 = CT.getShift(0, -ballAim.R);
			Vect CT2 = CT.getShift(0, +ballAim.R);
			result1 = activeLose.vect.getPointCrossLine(CT1);
			result = activeLose.vect.getPointCrossLine(CT);
			result2 = activeLose.vect.getPointCrossLine(CT2);
			bool res1 = activeLose.vect.isPossess(result1);
			bool res2 = activeLose.vect.isPossess(result2);
			// check direction
			Vect CL = new Vect(ballAim.point, result);
			bool res = CL.isPossess(ballTarget.point);
			return (res1 || res2) && res;
		} // ///////////////////////////////////////////////////////////////////////
	} // ***************************************************************************
}
