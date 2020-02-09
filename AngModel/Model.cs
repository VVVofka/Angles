using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AngModel {
	class Model {
		// All in milimeter
		const double BallDiameter = 68.0;
		const double TableWidth = 3550.0;
		const double TableHeigh = 1775.0;
		const double LoseCornerWidth = 72.0;
		const double LoseCenterWidth = 82.0;

		Ball ballCue = new Ball(BallDiameter);
		Ball ballTarget = new Ball(BallDiameter);
		Loses loses = new Loses(LoseCornerWidth, LoseCenterWidth, TableWidth, TableHeigh);

		public void setBallCue(double X, double Y) { ballCue.set(X, Y); }
		public void setballTarget(double X, double Y) { ballTarget.set(X, Y); }
	} // ***************************************************************************
}
