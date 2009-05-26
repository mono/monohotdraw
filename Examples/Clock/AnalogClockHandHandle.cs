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
using System;

namespace MonoHotDraw.Samples {

	public class AnalogClockHandHandle : LocatorHandle {
		
		public AnalogClockHandHandle (IFigure owner, ILocator locator) : base (owner, locator) {
			_default  = new Cairo.Color (1, 0, 0, 0.8);
			_selected = new Cairo.Color (1, 0, 1, 0.8);
			//We kept the pointer to use it later when requesting cursor 
			//and value to update
			_hand     = (AnalogClockHandLocator) locator;

			FillColor = _default;
			LineColor = new Cairo.Color (0.0, 0.0, 0.0, 1.0);
		}

		public override Gdk.Cursor CreateCursor () {
			return _hand.GetCursor ();
		}

		public override void InvokeEnd (double x, double y, IDrawingView view) {
			FillColor = _default;
		}

		public override void InvokeStart (double x, double y, IDrawingView view) {
			FillColor = _selected;
		}

		public override void InvokeStep (double x, double y, IDrawingView view) {
			AnalogClockFigure clock = (AnalogClockFigure) Owner;
			double      newX  = 0;
			double      newY  = 0;
			double      value = 0;

			newX = Math.Asin ((x - clock.CenterX - clock.DisplayBox.X) / clock.Radius) / (6 * Math.PI / 180);
			newY = Math.Acos ((y - clock.CenterY - clock.DisplayBox.Y) / clock.Radius) / (6 * Math.PI / 180);

			if (Double.IsNaN (newX) || Double.IsNaN (newY))
				return;

			if (y < clock.DisplayBox.Y + clock.Radius) {
				if (newX < 0) {//top left 
					value = 60 - Math.Abs (newX);
				} else { //top right
					value = newX;
				}
			} else {
				if (newX < 0) { //bottom left
					value = Math.Abs (newX) + 30;
				} else {//bottom right
					value = 30 - newX;
				}
			}

			_hand.UpdateClockTime (clock, (int) value);
		}
		
		public override void Draw (Context context) {
			RectangleD rect = DisplayBox;

			context.LineWidth = LineWidth;
			context.Rectangle (GdkCairoHelper.CairoRectangle (rect));
			context.Color = FillColor;
			context.FillPreserve ();
			context.Color = LineColor;
			context.Stroke ();
		}

		private Cairo.Color      _default;
		private AnalogClockHandLocator _hand;
		private Cairo.Color      _selected;
	}
}
