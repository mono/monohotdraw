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
using System;
using System.Collections.Generic;

namespace MonoHotDraw {

	public class UndoableAdapter : IUndoable {
	
		public UndoableAdapter (IDrawingView drawingView) {
			_drawingView = drawingView;
		}
		
		public bool Undoable {
			get { return _undoable; }
			set { _undoable = value; }
		}

		public bool Redoable {
			get { return _redoable; }
			set { _redoable = value; }
		}

		public IDrawingView DrawingView {
			get { return _drawingView; }
		}

		public virtual FigureCollection AffectedFigures {
			get { return _affectedFigures; }
			set { _affectedFigures = value; }
		}
		
		// TODO: Move this to FigureCollection
		public virtual FigureCollection AffectedFiguresReversed {
			get {
				FigureCollection collection = new FigureCollection ();
				for (int i = 1; i <= AffectedFigures.Count; i++) {
					collection.Add (AffectedFigures [AffectedFigures.Count - i]);
				}
				return collection;
			}
		}

		public virtual bool Undo () {
			return Undoable; 
		}

		public virtual bool Redo () {
			return Redoable;
		}

		public void Release () {
			_affectedFigures = null;
		}
		
		private FigureCollection _affectedFigures;
		private IDrawingView     _drawingView;
		private bool             _redoable;
		private bool             _undoable;
	}
}
