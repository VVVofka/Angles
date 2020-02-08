using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angles {
	class FromTo {
		public double from = 0, to = 1.0;
		public FromTo(double From, double To) {
			from = From;
			to = To;
		}
		public double size { get { return to - from; } }
		public double val(Random rnd) { return from + rnd.NextDouble() * size; }
	} // **********************************************************************
	class mdParamsLine {
		public FromTo kLen;
		public FromTo kIntersect;
		public mdParamsLine(double len_from, double len_to,
							double cross_from, double cross_to) {
			kLen = new FromTo(len_from, len_to);
			kIntersect = new FromTo(cross_from, cross_to);
		}
	} // ****************************************************************************
	class mdParamsTwoLine {
		public mdParamsLine ln1;
		public mdParamsLine ln2;
		public mdParamsTwoLine() {
			ln1 = new mdParamsLine(0.95, 1.35, 0.0, 0.15);
			ln2 = new mdParamsLine(0.75, 0.95, 0.0, 0.1);
		} // /////////////////////////////////////////////////////////////////////////
	} // ****************************************************************************
}
