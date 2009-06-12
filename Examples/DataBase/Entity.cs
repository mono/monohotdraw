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
using MonoHotDraw;
using MonoHotDraw.Figures;
using MonoHotDraw.Handles;
using MonoHotDraw.Locators;
using MonoHotDraw.Tools;
using MonoHotDraw.Util;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MonoHotDraw.Database {
	
	[Serializable]
	public class Entity : SimpleTextFigure, IPopupMenuFigure {
		
		public Entity () : this ("Entity") {
		}

		public Entity (string name) : base (name) {
			Padding   = 10;
			FillColor = new Color (0.8, 0.8, 1, 0.7);
			LineColor = new Color (0, 0, 0, 1);
		}

		protected Entity (SerializationInfo info, StreamingContext context) : base (info, context) {
			WeakEntity = ((bool) info.GetValue ("WeakEntity", typeof (bool)));
			Console.WriteLine ("Caling entity SerializationInfo info, StreamingContext context");
		}

		public override IEnumerable<IHandle> HandlesEnumerator {
			get {
				if (_handles == null) {
					InitializeHandles ();
				}
				foreach (IHandle handle in _handles)
					yield return handle;
			}
		}
		
		public override RectangleD InvalidateDisplayBox {
			get {
				RectangleD rect = DisplayBox;
				//FIXME: Change these hardcoded values
				rect.Inflate (18, 18);
				return rect;
			}
		}
		
		public IEnumerable <Gtk.MenuItem> MenuItemsEnumerator { 
			get {
				Gtk.CheckMenuItem item = new Gtk.CheckMenuItem ("Weak entity");
				item.Active = this.WeakEntity;
				item.Activated += delegate { WeakEntity = item.Active; };
				yield return item;
			}
		}
		
		public bool WeakEntity {
			get { return _weakEntity; }
			set { _weakEntity = value; }
		}

		public override void BasicDraw (Context context) {
			SetupLayout (context);
			DrawEntity (context, false);
			DrawText (context);
		}
		
		public override void BasicDrawSelected (Context context) {
			DrawEntity (context, true);
		}
		
		public override ITool CreateFigureTool (IDrawingEditor editor, ITool defaultTool) {
			return new PopupMenuTool (editor, this, defaultTool, base.CreateFigureTool (editor, defaultTool)); 
		}
		
		private void DrawEntity (Context context, bool selected) {
			RectangleD displayBox = BasicDisplayBox; 
			displayBox.OffsetDot5 ();

			context.LineWidth = LineWidth + (selected == true ? 2 : 0);
			context.Save ();
			context.Rectangle (new PointD (displayBox.X, displayBox.Y), 
			                  displayBox.Width, displayBox.Height);
			if (WeakEntity) {
				context.Rectangle (new PointD (displayBox.X - 2, displayBox.Y - 2), 
			    	              displayBox.Width + 4, displayBox.Height + 4);
			}
			context.Save ();
			if (selected == true) {
				context.Color = new Color (0, 0, 0, 1);
			} else {
				context.Color = FillColor;
				context.FillPreserve ();
				context.Color = LineColor;
			}
			context.Stroke ();
		}
		
		public override void GetObjectData (SerializationInfo info, StreamingContext context) {
			info.AddValue ("WeakEntity", WeakEntity);

			base.GetObjectData (info, context);
		}

		private void InitializeHandles () {
			_handles = new List <IHandle> ();
			_handles.Add (new EntityAttributeHandle (this, new QuickActionLocator (7.5, 0.5, QuickActionPosition.Right)));
			_handles.Add (new EntityRelationHandle (this, new QuickActionLocator (7.5, 0.5, QuickActionPosition.Left)));
		}
		
		private bool _weakEntity;
		private List<IHandle> _handles;
	}
}

