
namespace AngModel {
	class Lose {
		readonly double width;
		readonly double x0, x1;  // horiz coordinate
		readonly double y0, y1; // vert coordinate
		string description;
		public Lose(double Width, 
					double X0, double Y0, 
					double X1, double Y1,
					string Description) {
			width = Width;
			x0 = X0;
			y0 = Y0;
			x1 = X1;
			y1 = Y1;
			description = Description;
		} // ////////////////////////////////////////////////////////////////////////
	} // ****************************************************************************
}
