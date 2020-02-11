using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
namespace AngModel {
	class Ball : INotifyPropertyChanged {
		public Ball(double diameter) {
			mpoint = new Point(-1, -1);
			R = diameter / 2;
		} // ///////////////////////////////////////////////////////////////////////////
		Point mpoint;
		public Point point {
			get { return mpoint; }
			set {
				mpoint = value;
				OnPropertyChanged("point");
			}
		}
		public double x { get { return mpoint.X; } private set { mpoint.X = value; } }
		public double y { get { return mpoint.Y; } private set { mpoint.Y = value; } }
		double radius;
		public double R {    // Radius
			get { return radius; }
			set {
				radius = value;
				OnPropertyChanged("R");
			}
		} // ////////////////////////////////////////////////////////////////////////////
		public double D {  // Diameter
			get { return radius * 2; } 
			set {
				R = value / 2;
			}
		} // /////////////////////////////////////////////////////////////////////

		public void set(double X, double Y) { x = X; y = Y; }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "") {
			if(PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	} // ******************************************************************************
}
