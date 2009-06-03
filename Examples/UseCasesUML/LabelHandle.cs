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
using Cairo;
using MonoHotDraw;
using MonoHotDraw.Figures;
using MonoHotDraw.Handles;

namespace MonoHotDraw.Samples {

	public class LabelHandle: AbstractHandle {
	
		public LabelHandle (IFigure owner): base (owner) {
		}
			
		public override void InvokeStart (double x, double y, IDrawingView view) {
		}
		
		public override void InvokeStep (double x, double y, IDrawingView view) {
			LabelFigure label = (LabelFigure) Owner;
			PointD delta = label.HandleDeltaPosition();
			Owner.MoveTo(x - delta.X, y - delta.Y);
			
			double offsetX = Owner.DisplayBox.Width/2 - delta.X;
			double offsetY = Owner.DisplayBox.Height/2 - delta.Y;
			label.SetPosition(new PointD(x + offsetX, y + offsetY));
		}
		
		public override void InvokeEnd (double x, double y, IDrawingView view) {
		}


		public override PointD Locate () {
			LabelFigure label = (LabelFigure) Owner;
			return label.HandlePosition();
		}
			
		public override void Draw (Context context) {
			double middle = DisplayBox.Width / 2.0;

			context.LineWidth = LineWidth;
			context.Save ();
			context.Translate (DisplayBox.X + middle, DisplayBox.Y + middle);
			context.Arc (0.0, 0.0, middle, 0.0, 2.0 * Math.PI);
			context.Restore ();
			context.Color = new Cairo.Color (1.0, 1.0, 0.2, 0.2);
			context.FillPreserve ();
			context.Color = new Cairo.Color (0.0, 0.0, 0.0, 1.0);
			context.Stroke ();
		}
	}
}
