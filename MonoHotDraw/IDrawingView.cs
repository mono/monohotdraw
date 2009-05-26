//
// MonoHotDraw. Diagramming library
//
// Authors:
//	Manuel Cerón <ceronman@gmail.com>
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
using System.Collections.Generic;

namespace MonoHotDraw {

	public interface IDrawingView {
	
		void Add (IFigure figure);
		void Remove (IFigure figure);
		//Used by clipboad
		FigureCollection InsertFigures (FigureCollection figures, double dx, double dy, bool check);
		
		void AddToSelection (IFigure figure);
		void AddToSelection (FigureCollection collection);
		void RemoveFromSelection (IFigure figure);
		void ToggleSelection (IFigure figure);
		void ClearSelection ();
		void ScrollToMakeVisible (PointD p);
		void ScrollToMakeVisible (RectangleD r);
		bool IsFigureSelected (IFigure figure);
		PointD DrawingToView (double x, double y);
		PointD ViewToDrawing (double x, double y);
		IHandle FindHandle (double x, double y);
		
		IDrawing Drawing { get; set; }
		IDrawingEditor Editor { get; set; }
		IEnumerable <IFigure> SelectionEnumerator { get; }
		int SelectionCount { get; }
		RectangleD VisibleArea { get; }
		
		void AddWidget (Gtk.Widget w, double x, double y);
		void MoveWidget (Gtk.Widget w, double x, double y);
		void RemoveWidget (Gtk.Widget w);
		void ClearWidgets ();
	
//		bool IsFrozenView { get; } 
//		void FreezeView ();
//		void UnfreezeView ();
	}
}

