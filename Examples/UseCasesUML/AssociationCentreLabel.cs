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

// The label has a position inside the polyline 
// relativePos is the absolute position relative 
// to the total length of the PolyLine
//
// relativePos = absoultePos / length
//
// distance is the distance from the point inside the
// polyline to the centre of the label.
//
//
//
//                      label
//                        |
//                        |----distance
//               .________.____________.
//               /         \
//              /  /------| \
//             /  /          (middle)
// ._________./  /\
//              /  \
// |-----------/    \
//                Absoulte position
// 
	public class AssociationCentreLabel: LabelFigure {
	
		public AssociationCentreLabel(string text, double pos, double dis): base(text) {
			_relativePos = pos;
			_distance = dis;
		}		
		
		// This metod obtains the label position based on relativPos and distance.
		public void UpdatePosition(List<PointD> points) {
			_anchorPoints = points;
			
			double length = Geometry.PolyLineSize(points);
			
			if (length == 0) {
				return;
			}
			
			// first we convert the relativePos to an absolutePos
			double position = length * _relativePos;
			
			// second, we look for the point inside the PolyLine.
			for (int i=0; i<points.Count-1; i++) {
				double len = Geometry.LineSize(points[i], points[i+1]);
				// when position is less than the lenght of the current line segment
				// that's the right line segment (we use Math.Round to avoid floating 
				// point comparison problems)
				if (len != 0 && Math.Round(position, 2) <= Math.Round(len, 2)) {
					double w = points[i+1].X - points[i].X;
					double h = points[i+1].Y - points[i].Y;
					
					double x = position * w / len;
					double y = position * h / len;
					
					// then we calculate the point inside the poly line.
					PointD middle = new PointD(points[i].X + x, points[i].Y + y);
					_anchor = middle;
					
					// we look for the normal point in order to locate the label.
					PointD normal = new PointD(0.0, 0.0);
					normal.X = -h / len;
					normal.Y = w / len;
					
					// then the label is located.
					RectangleD r = DisplayBox;
					r.X = middle.X + normal.X * _distance - r.Width/2;
					r.Y = middle.Y + normal.Y * _distance - r.Height/2;
					BasicDisplayBox = r;
					
					return; 
				}
				position -= len;
			}
		}
		
		// This method obtains relativePos and distance based on a label position
		public override void SetPosition(PointD pos) {
			double partialLen = 0.0;
			double absolutePos = 0.0;
			
			_distance = Double.MaxValue;
			
			// looks in the PolyLine to find which is the line segment closest to de point
			for (int i = 0; i < _anchorPoints.Count-1; i++) {
				PointD a = _anchorPoints[i];
				PointD b = _anchorPoints[i+1];
				double length = Geometry.LineSize(a, b);
								
				// Calculate the distence between the line segment and the point
				double lineDistance = ((a.X - pos.X) * (b.Y - a.Y) - (a.Y - pos.Y) * (b.X - a.X)) / length;
				
				// Calculate the relative distance between the point and the
				// first point of the line segment. 
				double relPointDistance = ((pos.X - a.X) * (b.X - a.X) + (pos.Y - a.Y) * (b.Y - a.Y)) / (length*length);
				
				// If relPointDistance is <0 or >1, it means that the point is not perpendicular
				// to the line segment. So the boundaries are limited to a or b.
				if (relPointDistance < 0.0) {
					relPointDistance = 0.0;
					lineDistance = Geometry.LineSize(a, pos) * Math.Sign(lineDistance);
				}
				if (relPointDistance > 1.0) {
					relPointDistance = 1.0;
					lineDistance = Geometry.LineSize(b, pos) * Math.Sign(lineDistance);
				}					
				
				// if the obtained distance for the current line segment is shorter than
				// the current distance, the new current distance is the shortest.
				if (Math.Abs(lineDistance) < Math.Abs(_distance)) {
					_distance = lineDistance;
					absolutePos = partialLen + relPointDistance * length;
					double x = a.X + (b.X - a.X) * relPointDistance;
					double y = a.Y + (b.Y - a.Y) * relPointDistance;
					_anchor = new PointD(x, y);
				}
				partialLen += length;
			}
			
			// at the end we calculate the relative position and use UpdatePosition in order 
			// to prevent any weird point.
			_relativePos = absolutePos / partialLen;
			UpdatePosition(_anchorPoints);
		}
		
		public PointD Anchor {
			get { return _anchor; }
		}
		
		public override PointD HandlePosition() {
			PointD p = new PointD(0.0, 0.0);
			
			double angle =  Geometry.AngleFromPoint (DisplayBox, _anchor);
			if (angle > 0.0 && angle < Math.PI) {
				p.Y = DisplayBox.Bottom;
			}
			else {
				p.Y = DisplayBox.Top;
			}
			
			p.X = DisplayBox.Center.X;
			return p;
		}
		
		private double _relativePos;
		private double _distance;
		private PointD _anchor;
		private List<PointD> _anchorPoints;
	}
}
