﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngModel {
	class Loses {
		public Loses(double width_corner, double width_center, double width_table, double heigh_table) {
			v = new Lose[6];
			borders = new Border[6];
			double tmp = width_corner / Math.Sqrt(2);
			double xleft = tmp;
			double xright = width_table - tmp;
			double ytop = tmp;
			double ybottom = heigh_table - tmp;
			double tmpleft = (width_table - width_center) / 2;
			double tmpright = (width_table + width_center) / 2;
			int i = 0;
			v[i] = new Lose(width_corner, 0, ytop, xleft, 0, "Top Left");
			v[1] = new Lose(width_corner, xright, 0, width_table, ytop, "Top Right");	
			v[2] = new Lose(width_corner, 0, ybottom, xleft, heigh_table, "Bottom Left");
			v[3] = new Lose(width_corner, xright, heigh_table, width_table, ybottom, "Bottom Right");

			v[4] = new Lose(width_center, tmpleft, 0, tmpright, 0, "Top Center");
			v[5] = new Lose(width_center, tmpleft, heigh_table, tmpright, heigh_table, "Bottom Center");

			borders[0] = new Border(v[0].point2, v[4].point1);
			borders[1] = new Border(v[4].point2, v[1].point1);
			borders[2] = new Border(v[2].point2, v[5].point1);
			borders[3] = new Border(v[5].point2, v[3].point1);

			borders[4] = new Border(v[0].point1, v[2].point1);
			borders[5] = new Border(v[1].point2, v[3].point2);
		}
		Lose[] v;
		public Border[] borders;

		public Lose this[int index] {
			get { return v[index]; }
			private set { v[index] = value; }
		} // /////////////////////////////////////////////////////////////////
	} // **********************************************************************
}
