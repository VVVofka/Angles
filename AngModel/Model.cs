using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		Loses loses = new Loses(LoseCornerWidth, LoseCenterWidth, TableWidth, TableHeigh);
		public Lose activeLose { get; private set; }

		public Model() {
			setActiveLose(0);
			setBallCue(2 * BallDiameter, 2 * BallDiameter);
			setBallTarget(TableWidth / 4, TableHeigh / 2);
		} // //////////////////////////////////////////////////////////////////////

		public Lose setActiveLose(int index) { return activeLose = loses[index]; }
		public void setBallCue(double X, double Y) { ballCue.set(X, Y); }
		public void setBallTarget(double X, double Y) { ballTarget.set(X, Y); }

	} // ***************************************************************************
}
