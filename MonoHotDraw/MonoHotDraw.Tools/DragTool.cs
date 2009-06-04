// TODO: Renamed to DragTool

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

using Gdk;
using System;
using MonoHotDraw.Figures;

namespace MonoHotDraw.Tools {

	public class DragTool: AbstractTool {
	
		public DragTool (IDrawingEditor editor, IFigure anchor): base (editor) {
			AnchorFigure = anchor;
		}

		public IFigure AnchorFigure {
			get { return  _anchorFigure; }
			set { _anchorFigure = value; }
		}

		protected double LastX {
			set { _lastX = value; }
			get { return _lastX; }
		}

		protected double LastY {
			set { _lastY = value; }
			get { return _lastY; }
		}

		protected void SetLastCoords (double x, double y) {
			LastX = x;
			LastY = y;
		}

		public bool HasMoved {
			get { return _hasMoved; }
			protected set {_hasMoved = value; }
		}

		public override void MouseDown (MouseEvent ev) {
			base.MouseDown (ev);
			IDrawingView view = ev.View;
			
			SetLastCoords (ev.X, ev.Y);
			
			Gdk.ModifierType state = (ev.GdkEvent as EventButton).State;

			bool shift_pressed = (state & ModifierType.ShiftMask) != 0;

			if (shift_pressed) {
				view.ToggleSelection (AnchorFigure);
			}
				
			else if (!view.IsFigureSelected (AnchorFigure)) {
				view.ClearSelection ();
				view.AddToSelection (AnchorFigure);
			}

		}

		public override void MouseDrag (MouseEvent ev) {
			HasMoved = (Math.Abs (ev.X - AnchorX) > 4 || Math.Abs (ev.Y - AnchorX) > 4);

			if (HasMoved) {
				foreach (IFigure figure in ev.View.SelectionEnumerator) {
					figure.MoveBy (ev.X - LastX, ev.Y - LastY);
				}
			}
			SetLastCoords (ev.X, ev.Y);
		}
		
		public override void MouseUp (MouseEvent ev) {
//			view.Drawing.RecalculateDisplayBox ();
		}

		private double _lastX;
		private double _lastY;
		private bool _hasMoved;
		private IFigure _anchorFigure;
	}
}
