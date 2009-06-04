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

using System;
using MonoHotDraw.Commands;

namespace MonoHotDraw.Tools {
	
	public class UndoableTool: ITool	{
		
		public UndoableTool (ITool wrappedTool): base(editor) {
			WrappedTool = wrappedTool;
		}
		
		public ITool WrappedTool { get; private set; }

		public IDrawingEditor Editor {
			get { return WrappedTool.Editor; }
			set { WrappedTool.Editor = value; }
		}
		
		public IDrawingView View {
			get { return WrappedTool.View; }
			set { WrappedTool.View = value; }
		}
		
		public bool Activated {
			get { return WrappedTool.Activated; }
		}
		
		public virtual IUndoActivity UndoActivity {
			get { return WrappedTool.UndoActivity; }
			set { WrappedTool.UndoActivity = value; }
		}
		
		public void Activate () {
			WrappedTool.Activate();
		}
		
		public void Deactivate () {
			WrappedTool.Deactivate();
			
			IUndoActivity activity = WrappedTool.UndoActivity;
			if (activity != null && activity.Undoable) {
				Editor.UndoManager.PushUndo(activity);
				Editor.UndoManager.ClearRedos();
			}
		}
			
		public void KeyDown (KeyEvent ev) {
			WrappedTool.KeyDown(ev);
		}
			
		public void KeyUp (KeyEvent ev) {
			WrappedTool.KeyDown(ev);
		}

		public void MouseDown (MouseEvent ev) {
			WrappedTool.MouseDown(ev);
		}

		public void MouseDrag (MouseEvent ev) {
			WrappedTool.MouseDrag(ev);
		}
			
		public void MouseMove (MouseEvent ev){
			WrappedTool.MouseMove(ev);
		}
			
		public void MouseUp (MouseEvent ev) {
			WrappedTool.MouseUp(ev);
		}
	}
}
