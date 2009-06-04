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

using System;
using System.Collections.Generic;

namespace MonoHotDraw.Commands {

	public class UndoManager {
	
		public  static readonly int DefaultBufferSize = 30;

		public UndoManager () : this (UndoManager.DefaultBufferSize) {
		}
		
		public UndoManager (int bufferSize) {
			_bufferSize = bufferSize;
			_redoList   = new List<IUndoActivity> ();
			_undoList   = new List<IUndoActivity> ();
		}
		
		public bool Redoable {
			get { 
				if (_redoList.Count > 0) {
					return GetLastElement (_redoList).Redoable;
				}
				return false;	
			}
		}
		
		public bool Undoable {
			get { 
				if (_undoList.Count > 0) {
					return GetLastElement (_undoList).Undoable;
				}
				return false;	
			}
		}
		
		public void ClearRedos () {
			_redoList.Clear ();
		}
		
		public void ClearUndos () {
			_undoList.Clear ();
		}

		public IUndoActivity PopUndo () {
			IUndoActivity lastUndoable = PeekUndo ();
			_undoList.RemoveAt (_undoList.Count - 1);

			return lastUndoable;
		}

		public IUndoActivity PopRedo () {
			IUndoActivity lastUndoable = PeekRedo ();
			_redoList.RemoveAt (_redoList.Count - 1);

			return lastUndoable;
		}
		
		public void PushUndo (IUndoActivity undoActivity) {
			if (undoActivity.Undoable) {
				RemoveFirstElementInFullList (_undoList);
				_undoList.Add (undoActivity);
			} else {
				// a not undoable activity clears the stack because
				// the last activity does not correspond with the
				// last undo activity
				_undoList = new List<IUndoActivity> ();
			}
		}
		
		public void PushRedo (IUndoActivity redoActivity) {
			if (redoActivity.Redoable) {
				RemoveFirstElementInFullList (_redoList);
				// add redo activity only if it is not already the last
				// one in the buffer
				if (_redoList.Count == 0 || (PeekRedo () != redoActivity)) {
					_redoList.Add (redoActivity);
				}
			} else {
				// a not undoable activity clears the tack because
				// the last activity does not correspond with the
				// last undo activity
				_redoList = new List <IUndoActivity> ();
			}
		}

		private IUndoActivity GetLastElement (List<IUndoActivity> list) {
			return list.Count > 0 ? list [list.Count - 1] : null;
		}
		
		private void RemoveFirstElementInFullList (List<IUndoActivity> list) {
			if (list.Count >= _bufferSize) {
				IUndoActivity removedActivity = list [0];
				list.RemoveAt (0);
				removedActivity.Release ();
			}
		}
		
		private IUndoActivity PeekRedo () {
			return _redoList.Count > 0 ? GetLastElement (_redoList) : null;
		}
		
		private IUndoActivity PeekUndo () {
			return _undoList.Count > 0 ? GetLastElement (_undoList) : null;
		}

		private int             _bufferSize;
		private List<IUndoActivity> _redoList;
		private List<IUndoActivity> _undoList;
	}
}
