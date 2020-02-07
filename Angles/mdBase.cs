using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Angles {
	public class mdBase {
		protected Random rnd = new Random(7112);
		public Canvas canvas;
		public Line ln1, ln2;
		public mdBase(Canvas canvas_base, Line line_1, Line line_2) {
			canvas = canvas_base;
			ln1 = line_1;
			ln2 = line_2;
		} // //////////////////////////////////////////////////////////////
	} // *******************************************************************
}
