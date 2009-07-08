// MonoHotDraw. Diagramming Framework
//
// Authors:
//	Manuel Cerón <ceronman@gmail.com>
//
// Copyright (C) 2006, 2007, 2008, 2009 MonoUML Team (http://www.monouml.org)
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

using System;
using Cairo;
using MonoHotDraw.Figures;
using MonoHotDraw.Locators;
using MonoHotDraw.Util;

namespace MonoHotDraw.Handles {
	
	public class ToggleEventArgs: EventArgs {
		
		public ToggleEventArgs(bool active): base() {
			Active = active;
		}
		
		public bool Active { get; private set; }
	}
	
	public class ToggleButtonHandle: LocatorHandle {
		
		public ToggleButtonHandle(IFigure owner, ILocator locator): base(owner, locator) {
			FillColor = new Color(1, 1, 0.0, 0.3);
		}
		
		public event EventHandler<ToggleEventArgs> Toggled;
		
		public override RectangleD DisplayBox {
			get {
				PointD p = Locate ();
				RectangleD rect = new RectangleD (p, p);
				rect.Inflate (width/2, height/2);
				rect.OffsetDot5 ();
				return rect;
			}
		}
		
		public override void Draw (Cairo.Context context)
		{
			base.Draw(context);
			
			if (Active) {
				DrawOn(context);
			}
			else {
				DrawOff(context);
			}
		}
		
		public bool Active {
			get { return active; }
			set {
				active = value;
				OnToggled();
			}
		}
		
		public override void InvokeStart (double x, double y, IDrawingView view)
		{
			base.InvokeStart (x, y, view);
			
			clicked = true;
		}
		
		public override void InvokeEnd (double x, double y, IDrawingView view)
		{
			base.InvokeEnd (x, y, view);
			
			if (clicked) {
				Active = !Active;
			}
			
			clicked = false;
		}

		
		protected virtual void OnToggled()
		{
			if (Toggled != null) {
				Toggled(this, new ToggleEventArgs(Active));
			}
		}
		
		protected virtual void DrawOn(Cairo.Context context)
		{
			RectangleD rect = DisplayBox;
			PointD center = rect.Center;
			
			double margin = 5.0;
			
			context.MoveTo(rect.Left + margin, Dot5(center.Y));
			context.LineTo(rect.Right - margin, Dot5(center.Y));
			context.Stroke();
		}
		
		protected virtual void DrawOff(Cairo.Context context)
		{
			RectangleD rect = DisplayBox;
			PointD center = rect.Center;
			
			double margin = 5.0;
			
			context.MoveTo(rect.Left + margin, Dot5(center.Y));
			context.LineTo(rect.Right - margin, Dot5(center.Y));
			context.Stroke();
			
			context.MoveTo(Dot5(center.X), rect.Top + margin);
			context.LineTo(Dot5(center.X), rect.Bottom - margin);
			context.Stroke();
		}
		
		private double Dot5(double val)
		{
			return Math.Truncate(val) + 0.5;
		}
		
		private double width = 20.0;
		private double height = 20.0;
		private bool active = false;
		private bool clicked = false;
	}
}
