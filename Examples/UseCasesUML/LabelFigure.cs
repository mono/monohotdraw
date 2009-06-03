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
using System.Collections.Generic;
using Cairo;
using MonoHotDraw;
using MonoHotDraw.Figures;
using MonoHotDraw.Handles;
using MonoHotDraw.Util;

namespace MonoHotDraw.Samples {

	public class LabelFigure: SimpleTextFigure {
	
		public LabelFigure(string text): base(text) {
		}
		
		public override void BasicDrawSelected(Context context) {
			if (Text != "") {
				return;
			}
			context.LineWidth = LineWidth;
			Color c = LineColor;
			c.A = 0.5;
			context.Color = c;
			RectangleD r = DisplayBox;
			r.OffsetDot5();
			context.Rectangle(GdkCairoHelper.CairoRectangle(r));
			context.Stroke();
		}
		
		public virtual PointD HandlePosition() {
			return new PointD(DisplayBox.X, DisplayBox.Y);
		}
		
		public virtual void SetPosition(PointD pos) {
		}
		
		public void BasicDrawSelected(Context context, PointD anchor) {
			BasicDrawSelected(context);
			
			context.Save();			
			context.Color = new Color(1.0, 0.2, 0.2);
			context.SetDash(Dash.DotDash, 0.0);
			context.MoveTo(anchor);
			context.LineTo(HandlePosition());
			context.Stroke();
			context.Restore();
		}
		
		public virtual PointD HandlePossition() {
			return new PointD(DisplayBox.X, DisplayBox.Y);
		}
		
		public PointD HandleDeltaPosition() {
			PointD pos = HandlePosition();
			
			double deltaX = pos.X - DisplayBox.X;
			double deltaY = pos.Y - DisplayBox.Y;
			
			return new PointD(deltaX, deltaY);
		}
		
		public override IEnumerable<IHandle> HandlesEnumerator {
			get {
				yield return new LabelHandle(this);
			}
		}
	}
}
