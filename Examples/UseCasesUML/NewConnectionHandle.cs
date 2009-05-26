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
using System.Reflection;
using Gdk;
using Cairo;
using MonoHotDraw;

namespace MonoHotDraw.Samples {

	public abstract class NewConnectionHandle: LocatorHandle {
	
		public NewConnectionHandle(IFigure owner, ILocator locator): base (owner, locator) {
			if (_icon == null) {
				// TODO: use icon factory.
				Pixbuf pixbuf = Pixbuf.LoadFromResource("icons.new_association_handle.png");
				_icon = GdkCairoHelper.PixbufToImageSurface(pixbuf);
			}
		}
		
		public override RectangleD DisplayBox {
			get {
				RectangleD rect = new RectangleD(Locate());
				rect.Inflate(8.0, 8.0);
				return rect;
			}
		}
		
		public override void InvokeStart(double x, double y, IDrawingView view) {
			_connection = CreateConnection();
			_connection.EndPoint = new PointD (x, y);
			_connection.StartPoint = new PointD (x, y);
			_connection.ConnectStart (Owner.ConnectorAt(x, y));
			_connection.UpdateConnection();
			view.Drawing.Add(_connection);
			view.ClearSelection();
			view.AddToSelection(_connection);
			_handle = view.FindHandle(x, y);
		}
		
		public override void InvokeStep(double x, double y, IDrawingView view) {
			if (_handle != null) {
				_handle.InvokeStep(x, y, view);
			}
		}
		
		public override void InvokeEnd(double x, double y, IDrawingView view) {
			if (_handle != null) {
				_handle.InvokeEnd(x, y, view);
			}
			
			if (_connection.EndConnector == null) {
				IFigure new_figure = CreateEndFigure();
				new_figure.MoveTo(x, y);
				view.Drawing.Add(new_figure);
				_connection.ConnectEnd(new_figure.ConnectorAt(0.0, 0.0));
				_connection.UpdateConnection();
				view.ClearSelection();
				view.AddToSelection(new_figure);
			}
		}
		
		public override void Draw(Context context) {
			RectangleD r = DisplayBox;
			_icon.Show (context, Math.Round (r.X), Math.Round (r.Y));
		}
		
		protected abstract IConnectionFigure CreateConnection();
		protected abstract IFigure CreateEndFigure();
		
		private IConnectionFigure _connection;
		private IHandle _handle;
		private static ImageSurface _icon;
	}
}
