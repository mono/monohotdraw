//
// MonoHotDraw. Diagramming library
//
// Authors:
//	Manuel Cerón <ceronman@gmail.com>
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
using System;
using System.Collections.Generic;
using Cairo;
using MonoHotDraw;

namespace MonoHotDraw.Samples {

	public class AssociationSideLabel: LabelFigure {
	
		public AssociationSideLabel(string text, double linedist, double pointdist): base(text) {
			_lineDistance = linedist;
			_pointDistance = pointdist;
		}
		
		public void UpdatePosition(PointD a, PointD b) {
			_anchorA = a;
			_anchorB = b;
			
			PointD arrowPoint = new PointD(); 
			// some dummy variables to fill GetArrowPoints parameters.
			PointD dummy1 = new PointD(), dummy2 = new PointD();
			Geometry.GetArrowPoints(a, b, _lineDistance, _pointDistance, out arrowPoint, out dummy1, out dummy2);
			
			RectangleD r = DisplayBox;
			r.X = arrowPoint.X  - r.Width/2;
			r.Y = arrowPoint.Y - r.Height/2;
			BasicDisplayBox = r;
		}
		
		public override void SetPosition(PointD pos) {
			Geometry.GetArrowDistances(_anchorA, _anchorB, pos, out _lineDistance, out _pointDistance);	
		}
		
		public override PointD HandlePosition() {
			double angle = Geometry.AngleFromPoint (DisplayBox, _anchorA);
			return Geometry.EdgePointFromAngle (DisplayBox, angle);
		}
			
		private double _lineDistance;
		private double _pointDistance;
		private PointD _anchorA = new PointD(0.0, 0.0);
		private PointD _anchorB = new PointD(0.0, 0.0);
	}
	
	
}
