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
using MonoHotDraw.Connectors;
using MonoHotDraw.Figures;
using MonoHotDraw.Handles;
using MonoHotDraw.Locators;
using MonoHotDraw.Util;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MonoHotDraw.Database {
	
	[Serializable]
	public class Relation : SimpleTextFigure {
		
		public Relation () : this ("Relation") {
		}

		public Relation (string name) : base (name) {
			Padding   = 18;
			FillColor = new Color (0.8, 0.6, 1, 0.7);
			LineColor = new Color (0, 0, 0, 1);
		}
		
		protected Relation (SerializationInfo info, StreamingContext context) : base (info, context) {
		}
		
		public override IEnumerable<IHandle> HandlesEnumerator {
			get {
				if (_handles == null)
					InitializeHandles ();

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

		public override void BasicDraw (Context context) {
			SetupLayout (context);
			DrawAttribute (context, false);
			DrawText (context);
		}
		
		public override void BasicDrawSelected (Context context) {
			DrawAttribute (context, true);
		}
		
		public override IConnector ConnectorAt (double x, double y) {
			System.Console.WriteLine ("Creating attribute connector.");
			return new ChopRelationConnector (this); 
		}
		
		private void DrawAttribute (Context context, bool selected) {
			SetupLayout (context);
			
			RectangleD displayBox = DisplayBox;
			displayBox.OffsetDot5();

			context.LineWidth = LineWidth + (selected == true ? 2 : 0);
			context.Save ();

			context.MoveTo (displayBox.X + displayBox.Width / 2, displayBox.Y);
			context.LineTo (displayBox.X + displayBox.Width, displayBox.Y + displayBox.Height / 2);
			context.LineTo (displayBox.X + displayBox.Width / 2, displayBox.Y + displayBox.Height);
			context.LineTo (displayBox.X, displayBox.Y + displayBox.Height / 2);
			context.LineTo (displayBox.X + displayBox.Width / 2, displayBox.Y);
			context.Restore ();
				
			context.Color = FillColor;
			context.FillPreserve ();
			context.Color = LineColor;
			context.Stroke ();

			DrawText (context);
		}
		
		private void InitializeHandles () {
			_handles = new List <IHandle> ();
			_handles.Add (new RelationEntityHandle (this, new QuickActionLocator (7.5, 0.5, QuickActionPosition.Right)));
			_handles.Add (new RelationAttributeHandle (this, new QuickActionLocator (7.5, 0.5, QuickActionPosition.Left)));		
		}

		private List <IHandle> _handles;
	}
}

