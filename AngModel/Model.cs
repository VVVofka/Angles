using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AngModel {
	class Model {
		// All in milimeter
		public const double BallDiameter = 68.0;
		public const double TableWidth = 3550.0;
		public const double TableHeigh = 1775.0;
		public const double LoseCornerWidth = 72.0;
		public const double LoseCenterWidth = 82.0;

		Ball ballCue = new Ball(BallDiameter);
		Ball ballTarget = new Ball(BallDiameter);
		Ball ballAim = new Ball(BallDiameter);
		Point pointAim = new Point(0,0);
		Loses loses = new Loses(LoseCornerWidth, LoseCenterWidth, TableWidth, TableHeigh);
		public Lose activeLose { get; private set; }
		public Coord result { get; private set; }

		public Model() {
			setActiveLose(0);
			setBallCue(2 * BallDiameter, 2 * BallDiameter);
			setBallTarget(TableWidth / 4, TableHeigh / 2);
		} // //////////////////////////////////////////////////////////////////////

		public Lose setActiveLose(int index) { return activeLose = loses[index]; }
		public void setBallCue(double X, double Y) { ballCue.set(X, Y); }
		public void setBallTarget(double X, double Y) { ballTarget.set(X, Y); }
		public Coord Shoot(double k) {  //k=-1...+1
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

			return result;
		} // ///////////////////////////////////////////////////////////////////////
	} // ***************************************************************************
}
