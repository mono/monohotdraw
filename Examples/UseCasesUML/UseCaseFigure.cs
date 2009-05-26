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

	public class UseCaseFigure: BaseBoxFigure {
	
		public UseCaseFigure (): base () {
			FillColor = new Cairo.Color(0.8, 0.8, 1.0, 0.8);
			
			_text = new MultiLineTextFigure("Use Case");
			
			_text.TextChanged += delegate(object sender, EventArgs args) {
				Resize();
			};
			
			_text.FontAlignment = Pango.Alignment.Center;
			_text.Padding = 10.0;
		}

		public override void BasicDraw (Context context) {
			double midwidth  = DisplayBox.Width / 2.0;
			double midheight = DisplayBox.Height / 2.0;

			context.LineWidth = LineWidth;
			context.Save ();
			context.Translate (DisplayBox.X + midwidth, DisplayBox.Y + midheight);
			context.Scale (midwidth - 1.0, midheight - 1.0);
			context.Arc (0.0, 0.0, 1.0, 0.0, 2.0 * Math.PI);
			context.Restore ();
			context.Color = FillColor;
			context.FillPreserve ();
			context.Color = LineColor;
			context.Stroke ();
			
			_text.BasicDraw(context);
		}
		
		public override RectangleD BasicDisplayBox  {
			get { return base.BasicDisplayBox; }
			set {
				RectangleD r = value;
				
				r.Width = Math.Max(_text.DisplayBox.Width + _margin*2, r.Width);
				r.Height = Math.Max(_text.DisplayBox.Height + _margin*2, r.Height);
				
				base.BasicDisplayBox = r;
			}
		}

		public override ITool CreateFigureTool (IDrawingEditor editor, ITool dt) {
			return new MultiLineTextTool (editor, _text, dt);
		}
		
		public override IConnector ConnectorAt (double x, double y) {
			return new ChopEllipseConnector (this);
		}
		
		protected override void OnFigureChanged (FigureEventArgs e) {
			base.OnFigureChanged(e);
			
			if (_text != null) {
				double x = DisplayBox.X + DisplayBox.Width/2 - _text.DisplayBox.Width/2;
				double y = DisplayBox.Y + DisplayBox.Height/2 - _text.DisplayBox.Height/2;
				_text.MoveTo(x, y);
			}
		}
		
		private void Resize() {
			RectangleD r  = _text.DisplayBox;
			r.Inflate(_margin, _margin);
			
			RectangleD r2 = DisplayBox;
			
			if (r.Width > r2.Width) {
				r2.X = r.X;
				r2.Width = r.Width;
			}
			
			if (r.Height > r2.Height) {
				r2.Y = r.Y;
				r2.Height = r.Height;
			}
			
			DisplayBox = r2;
			
			OnFigureChanged(new FigureEventArgs(this, DisplayBox));
		}
		
		private MultiLineTextFigure _text; 
		private static readonly double _margin = 20.0;
	}
}
