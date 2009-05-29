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
using Cairo;
using Gdk;
using Gtk;
using System;

namespace MonoHotDraw {

	public class ConnectionCreationTool: AbstractTool {
	
		public ConnectionCreationTool (IDrawingEditor editor, IConnectionFigure ction): base (editor) {
			_connection = ction;
			_connection.DisconnectStart ();
			_connection.DisconnectEnd ();
		}
		
		public override void MouseDrag (MouseEvent ev) {
			if (_handle != null) {
				_handle.InvokeStep (ev.X, ev.Y, ev.View);
			}
		}
		
		public override void MouseDown (MouseEvent ev) {
			base.MouseDown (ev);
			IDrawingView view = ev.View;
			IFigure figure = view.Drawing.FindFigure (ev.X, ev.Y);

			if (figure != null) {
				_connection.EndPoint = new PointD (ev.X, ev.Y);
				_connection.StartPoint = new PointD (ev.X, ev.Y);
				_connection.ConnectStart (figure.ConnectorAt (ev.X, ev.Y));
				_connection.UpdateConnection ();
				view.Drawing.Add (_connection);
				view.ClearSelection ();
				view.AddToSelection (_connection);
				_handle = _connection.EndHandle;
			}
			else {
				Editor.Tool = new SelectionTool (Editor);
			}

		}
		
		public override void MouseUp (MouseEvent ev) {
			if (_handle != null) {
				_handle.InvokeEnd (ev.X, ev.Y, ev.View);
			}
						
			if (_connection.EndConnector == null) {
				_connection.DisconnectStart ();
				_connection.DisconnectEnd ();
				ev.View.Drawing.Remove (_connection);
				ev.View.ClearSelection ();

			}
			Editor.Tool = new SelectionTool (Editor);
		}
		
		public override void MouseMove (MouseEvent ev) {
			Widget widget = (Widget) ev.View;
			IFigure figure = ev.View.Drawing.FindFigure (ev.X, ev.Y);
			if (figure != null) {
				widget.GdkWindow.Cursor = CursorFactory.GetCursorFromType (Gdk.CursorType.Cross);
			}
			else {
				widget.GdkWindow.Cursor = CursorFactory.GetCursorFromType (Gdk.CursorType.Crosshair);
			}
		}
		
		private IHandle _handle;
		private IConnectionFigure _connection;
	}
}