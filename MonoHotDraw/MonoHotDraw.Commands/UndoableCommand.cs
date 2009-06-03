// TODO: Review comments

// MonoHotDraw. Diagramming Framework
//
// Authors:
//	Mario Carrión <mario@monouml.org>
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

namespace MonoHotDraw.Commands {

	public class UndoableCommand : ICommand {
		
		public UndoableCommand (ICommand wrappedCommand) {
			_wrappedCommand = wrappedCommand;
		}

		public IDrawingEditor DrawingEditor {
			get { return WrappedCommand.DrawingEditor; }
		}

		public IDrawingView DrawingView {
			get { return WrappedCommand.DrawingView; }
		}
		
		public bool IsExecutable {
			get { return WrappedCommand.IsExecutable;  }
		}
		
		public string Name {
			get { return WrappedCommand.Name; }
		}
		
		public ICommand WrappedCommand {
			get { return _wrappedCommand; }
		}
		
		public IUndoable UndoActivity {
			get { return new UndoableAdapter (DrawingView); }
			set {  }
		}
		
		public void Execute () {
//			hasSelectionChanged = false;
			// listen for selection change events during executing the wrapped command
//			view().addFigureSelectionListener(this);

			WrappedCommand.Execute ();

			IUndoable undoableCommand = WrappedCommand.UndoActivity;
			if (undoableCommand != null && undoableCommand.Undoable) {
				DrawingEditor.UndoManager.PushUndo (undoableCommand);
				DrawingEditor.UndoManager.ClearRedos ();
			}

			// initiate manual update of undo/redo menu states if it has not
			// been done automatically during executing the wrapped command
//			if (!hasSelectionChanged || (getDrawingEditor().getUndoManager().getUndoSize() == 1)) {
//				getDrawingEditor().figureSelectionChanged(view());
//			}

			// remove because not all commands are listeners that have to be notified
			// all the time (bug-id 595461)
//			view().removeFigureSelectionListener(this);
		}
		
		private ICommand _wrappedCommand;
	}
}
