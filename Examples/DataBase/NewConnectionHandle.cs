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
using Gdk;
using MonoHotDraw;
using MonoHotDraw.Figures;
using MonoHotDraw.Handles;
using MonoHotDraw.Locators;
using MonoHotDraw.Util;
using System;

//TODO: Evaluate: adding this duplicated class to the framework.
//Because this class (with minor modifications) is also being used on UmlApplication
namespace MonoHotDraw.Database {

	[Serializable]
	public abstract class NewConnectionHandle : LocatorHandle {
		
		public NewConnectionHandle (IFigure owner, ILocator locator) : base (owner, locator) {
			_icon = GetConnectionIcon ();
		}

		public override RectangleD DisplayBox {
			get {
				RectangleD rect = new RectangleD (Locate ());
				rect.Inflate (8.0, 8.0);
				return rect;
			}
		}

		public override void Draw (Context context) {
			RectangleD r = DisplayBox;
			_icon.Show (context, Math.Round (r.X), Math.Round (r.Y));
		}
		
		public override void InvokeStart (double x, double y, IDrawingView view) {
			_connection = CreateConnectionFigure ();
			_connection.EndPoint = new PointD (x, y);
			_connection.StartPoint = new PointD (x, y);
			_connection.ConnectStart (Owner.ConnectorAt (x, y));
			_connection.UpdateConnection ();
			view.Drawing.Add (_connection);
			view.ClearSelection ();
			view.AddToSelection (_connection);
			_handle = view.FindHandle (x, y);
		}
		
		public override void InvokeStep (double x, double y, IDrawingView view) {
			if (_handle != null) {
				_handle.InvokeStep (x, y, view);
			}
		}

		public override void InvokeEnd (double x, double y, IDrawingView view) {
			if (_handle != null) {
				_handle.InvokeEnd (x, y, view);
			}
			
			if (_connection.EndConnector == null) {
				IFigure newFigure = CreateEndFigure ();
				newFigure.MoveTo (x, y);
				view.Drawing.Add (newFigure);
				_connection.ConnectEnd (newFigure.ConnectorAt (0.0, 0.0));
				_connection.UpdateConnection ();
				view.ClearSelection ();
				view.AddToSelection (newFigure);
			}
		}
		
		protected abstract IFigure CreateEndFigure ();
		protected abstract IConnectionFigure CreateConnectionFigure ();
		protected abstract ImageSurface GetConnectionIcon ();

		private IConnectionFigure _connection;
		private IHandle           _handle;
		private ImageSurface      _icon;
	}
}
