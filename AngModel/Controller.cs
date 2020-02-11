using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AngModel{
	class Controller   : INotifyPropertyChanged {
		Ball cueBall = new Ball(68);
		Model m = new Model();

		public Ball CueBall {
			get { return cueBall; }
			set {
				cueBall = value	;
				OnPropertyChanged("CueBall");
			}
		} // /////////////////////////////////////////////
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "") {
			if(PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	} // ****************************************************************************
}
