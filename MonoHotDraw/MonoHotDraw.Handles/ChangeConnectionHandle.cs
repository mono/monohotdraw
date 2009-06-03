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

using Cairo;
using System;
using MonoHotDraw.Connectors;
using MonoHotDraw.Figures;
using MonoHotDraw.Util;

namespace MonoHotDraw.Handles {

	public abstract class ChangeConnectionHandle: AbstractHandle {
	
		protected ChangeConnectionHandle (IConnectionFigure owner): base (owner) {
			Connection = owner;
			TargetFigure = null;
		}
		
		public override Gdk.Cursor CreateCursor () {
			return CursorFactory.GetCursorFromType (Gdk.CursorType.Hand2);
		}

		public override void InvokeStart (double x, double y, IDrawingView view) {
			_originalTarget = Target;
			Disconnect ();
		}

		public override void InvokeStep (double x, double y, IDrawingView view) {
			PointD p = new PointD (x, y);
			IFigure figure = FindConnectableFigure (x, y, view.Drawing);
			TargetFigure = figure;
			IConnector target = FindConnectionTarget (x, y, view.Drawing);
			if (target != null) {
				p = target.DisplayBox.Center;
			}

			Point = p;
			_connection.UpdateConnection ();
		}

		public override void InvokeEnd (double x, double y, IDrawingView view) {
			IConnector target = FindConnectionTarget (x, y, view.Drawing) ?? _originalTarget;
			Point = new PointD (x, y);
			Connect (target);
			Connection.UpdateConnection ();
		}

		public override void Draw (Context context) {
			context.LineWidth = LineWidth;

			context.MoveTo (DisplayBox.Center.X, DisplayBox.Top);
			context.LineTo (DisplayBox.Right, DisplayBox.Center.Y);
			context.LineTo (DisplayBox.Center.X, DisplayBox.Bottom);
			context.LineTo (DisplayBox.Left, DisplayBox.Center.Y);
			context.LineTo (DisplayBox.Center.X, DisplayBox.Top);

			context.Color = new Cairo.Color (1.0, 0.0, 0.0, 0.8);
			context.FillPreserve ();
			context.Color = new Cairo.Color (0.0, 0.0, 0.0, 1.0);
			context.Stroke ();
		}

		protected abstract PointD Point {set; }

		protected abstract IConnector Target { get; }

		protected abstract void Connect (IConnector connector);

		protected abstract void Disconnect ();

		protected abstract bool IsConnectionPossible (IFigure figure);

		protected IConnectionFigure Connection {
			get { return _connection; }
			set { _connection = value; }
		}

		protected IFigure TargetFigure {
			get { return _targetFigure; }
			set { _targetFigure = value; }
		}

		private IFigure FindConnectableFigure (double x, double y, IDrawing drawing) {
			foreach (IFigure figure in drawing.FiguresEnumeratorReverse) {
				if (figure.ContainsPoint (x, y) && IsConnectionPossible (figure)) {
					return figure;
				}
			}
			return null;
		}

		private IConnector FindConnectionTarget (double x, double y, IDrawing drawing) {
			IFigure target = FindConnectableFigure (x, y, drawing);
			return target != null ? target.ConnectorAt (x, y) : null;
		}

		private IConnectionFigure _connection;
		private IFigure _targetFigure;
		private IConnector _originalTarget;
	}
}
