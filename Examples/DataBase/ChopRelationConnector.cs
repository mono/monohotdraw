//
// MonoHotDraw. Diagramming library
//
// Authors:
//	Mario Carrión <mario@monouml.org>
//
// Copyright (C) 2006, 2007, 2008 MonoUML Team (http://www.monouml.org)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
using Cairo;
using MonoHotDraw;
using MonoHotDraw.Connectors;
using MonoHotDraw.Figures;
using MonoHotDraw.Util;
using System;
using System.Runtime.Serialization;

namespace MonoHotDraw.Database {

	[Serializable]
	public class ChopRelationConnector : ChopBoxConnector {
	
		public ChopRelationConnector (IFigure figure): base (figure) {
		}
		
		protected ChopRelationConnector (SerializationInfo info, StreamingContext context) : base (info, context) {
		}
		
		protected override PointD Chop (IFigure target, PointD point) {
			if (target.ContainsPoint (point.X, point.Y)) {
				return target.DisplayBox.Center;
			}

			double angle = Geometry.AngleFromPoint (DisplayBox, point);
			double sin = Math.Sin (angle);
			double cos = Math.Cos (angle);
			double x = 0;
			double e = 0.0001;
			double y = 0;
			RectangleD r = DisplayBox;

			if (Math.Abs (sin) > e) {
				x = (1.0 + cos / Math.Abs (sin)) / 2.0 * r.Width;
				x = Geometry.Range (0.0, r.Width, x);
			} else if (cos >= 0.0) {
				x = r.Width;
			}

			if (Math.Abs (cos) > e) {
				y = (1.0 + sin / Math.Abs (cos)) / 2.0 * r.Height;
				y = Geometry.Range (0.0, r.Height, y);
			} else if (sin >= 0.0) {
				y = r.Height;
			}

			x += r.X;// - (x + r.X > r.X + r.Width/2 ? (r.Width/2) * -1 : (r.Width/2));
			if (x > r.X + r.Width / 2) {
				//x = r.X + r.Width / 2;
				Console.WriteLine ("Decrementar la mitad ancho");
			} else {
//				x -= r.Width / 2;
				Console.WriteLine ("Aumentar la mitad ancho");				
			}
			y += r.Y;// - (y > (r.Y + r.Height) / 2 ? (r.Y + r.Height) / 2 : 0);
			if (y > r.Y + r.Height / 2) {
				Console.WriteLine ("Aumentar la mitad altura");
			//	y += r.Height / 2;
			} else {
				//y = r.Y + r.Height / 2;
				Console.WriteLine ("Decrementar la mitad altura");				
			//	y -= r.Height / 2;
			}

			return new PointD (x, y);
		}
	}
}
