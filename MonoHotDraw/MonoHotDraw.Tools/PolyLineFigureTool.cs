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
using Gdk;
using System;

namespace MonoHotDraw {
	// TODO: Should be this inside PolyLineFigure ???
	public class PolyLineFigureTool: FigureTool {
	
		public PolyLineFigureTool (IDrawingEditor editor, IFigure fig, ITool dt): base (editor, fig, dt) {
		}
		
		public override void MouseDown (MouseEvent ev) {
			SetAnchorCoords (ev.X, ev.Y);
			IDrawingView view = ev.View;
			View = view;
			
			Gdk.EventType type = ev.GdkEvent.Type;
			if (type == EventType.TwoButtonPress) {
				PolyLineFigure connection = (PolyLineFigure) Figure;
				connection.SplitSegment (ev.X, ev.Y);
				view.ClearSelection ();
				view.AddToSelection (Figure);
				IHandle handle = view.FindHandle (ev.X, ev.Y);
				((Gtk.Widget) view).GdkWindow.Cursor = handle.CreateCursor ();
				DefaultTool = new HandleTracker (Editor, handle);
			}
			DefaultTool.MouseDown (ev);
		}
	}
}
