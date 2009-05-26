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
using Gtk;
using Cairo;
using MonoHotDraw;

namespace MonoHotDraw.Samples {
	//FIXME: This is NoteFigure preview, I'm thinking changing this to a decorator.
	//because when editing the drawing is not updated according its resizing.
	public class NoteFigure: MultiLineTextFigure {

		public NoteFigure (string text): base  (text) {
			Padding   = 13;
			FillColor = new Color (1, 1, 0, 0.8);
			LineColor = new Color (0, 0, 0, 1);
		}
		
		public override void Draw (Context context) {
			SetupLayout(context);
			
			RectangleD displayBox = DisplayBox;
			displayBox.OffsetDot5();
			double side = 10.0;

			context.LineWidth = LineWidth;
			context.Save ();
			//Box
			context.MoveTo (displayBox.X, displayBox.Y);
			context.LineTo (displayBox.X, displayBox.Y + displayBox.Height);
			context.LineTo (displayBox.X + displayBox.Width, displayBox.Y + displayBox.Height);
			context.LineTo (displayBox.X + displayBox.Width, displayBox.Y + side);
			context.LineTo (displayBox.X + displayBox.Width - side, displayBox.Y);
			context.LineTo (displayBox.X, displayBox.Y);
			context.Save ();
			//Triangle
			context.MoveTo (displayBox.X + displayBox.Width - side, displayBox.Y);
			context.LineTo (displayBox.X + displayBox.Width - side, displayBox.Y + side);
			context.LineTo (displayBox.X + displayBox.Width, displayBox.Y + side);
			context.LineTo (displayBox.X + displayBox.Width - side, displayBox.Y);
			context.Restore ();
				
			context.Color = FillColor;
			context.FillPreserve ();
			context.Color = LineColor;
			context.Stroke ();

			DrawText (context);
		}
	}
}
