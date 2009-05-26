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
using System;

namespace MonoHotDraw {

	public abstract class AbstractTool: ITool {
	
		protected AbstractTool (IDrawingEditor editor) {
			Editor = editor;
		}

		public IDrawingEditor Editor {
			set { _editor = value; }
			get { return  _editor; }
		}

		public IDrawingView View {
			set { _view = value; }
			get { return  _view; }
		}
		
		public bool Activated {
			get { return _activated; }
		}
		
		public virtual void Activate () {
			_activated = true;
		}
		
		public virtual void Deactivate () {
			_activated = false;
		}
			
		public virtual void KeyDown (KeyEvent ev) {
		}
			
		public virtual void KeyUp (KeyEvent ev) {
		}

		public virtual void MouseDown (MouseEvent ev) {
			SetAnchorCoords (ev.X, ev.Y);
			View = ev.View;
		}

		public virtual void MouseDrag (MouseEvent ev) {
		}
			
		public virtual void MouseMove (MouseEvent ev){
		}
			
		public virtual void MouseUp (MouseEvent ev) {
		}

		protected double AnchorX {
			get { return  _anchorX; }
			set { _anchorX = value; }
		}

		protected double AnchorY {
			get { return  _anchorY; }
			set { _anchorY = value; }
		}

		protected void SetAnchorCoords (double x, double y) {
			AnchorX = x;
			AnchorY = y;
		}

		private IDrawingEditor _editor;
		private IDrawingView _view;
		private double _anchorX;
		private double _anchorY;
		private bool _activated = false;
	}
}
