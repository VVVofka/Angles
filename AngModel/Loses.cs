using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngModel {
	class Loses {
		public Loses(double width_corner, double width_center, double width_table, double heigh_table) {
			v = new Lose[6];
			double tmp = width_corner / Math.Sqrt(2);
			double xleft = tmp;
			double xright = width_table - tmp;
			double ytop = tmp;
			double ybottom = heigh_table - tmp;
			v[0] = new Lose(width_corner, 0, ytop, xleft, 0, "Top Left");
			v[1] = new Lose(width_corner, xright, 0, width_table, ytop, "Top Right");
			v[2] = new Lose(width_corner, 0, ybottom, xleft, heigh_table, "Bottom Left");
			v[3] = new Lose(width_corner, xright, heigh_table, width_table, ybottom, "Bottom Right");

			double tmpleft = (width_table - width_center) / 2;
			double tmpright = (width_table + width_center) / 2;
			v[4] = new Lose(width_center, tmpleft, 0, tmpright, 0, "Top Center");
			v[5] = new Lose(width_center, tmpleft, heigh_table, tmpright, heigh_table, "Bottom Center");
		}
		Lose[] v;
		public Lose this[int index] {
			get { return v[index]; }
			private set { v[index] = value; }
		} // /////////////////////////////////////////////////////////////////
	} // **********************************************************************
}
