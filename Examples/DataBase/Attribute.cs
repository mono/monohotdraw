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
using MonoHotDraw.Tools;
using MonoHotDraw.Util;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MonoHotDraw.Database {

	[Serializable]
	public class Attribute : SimpleTextFigure, IPopupMenuFigure {
		
		public Attribute () : this ("Attribute") {
		}

		public Attribute (string name) : base (name) {
			Padding   = 10;
			FillColor = new Color (0.8, 1, 0.8, 0.7);
			LineColor = new Color (0, 0, 0, 1);
		}

		public IEnumerable <Gtk.MenuItem> MenuItemsEnumerator { 
			get {
				List<Gtk.CheckMenuItem> items = new List<Gtk.CheckMenuItem> ();
				
				Gtk.CheckMenuItem primary  = new Gtk.CheckMenuItem ("Primary key");
				primary.Active = IsPrimaryKey;
				primary.Activated += delegate { IsPrimaryKey = primary.Active; };
				items.Add (primary);
				
				Gtk.CheckMenuItem multivalued  = new Gtk.CheckMenuItem ("Multivalued");
				multivalued.Active = Multivalued;
				multivalued.Activated += delegate { Multivalued = multivalued.Active; };
				items.Add (multivalued);
				
				foreach (Gtk.MenuItem item in items) {
					yield return item;
				}
			}
		}
		
		public bool IsPrimaryKey  {
			get { return _isPrimaryKey; }
			set { _isPrimaryKey = value; }
		}
		
		public bool Multivalued {
			get { return _multiValued; }
			set { _multiValued = value; }
		}

		public override void BasicDraw (Context context) {
			SetupLayout (context);
			DrawAttribute (context, false);
			DrawText (context);
		}
		
		public override ITool CreateFigureTool (IDrawingEditor editor, ITool defaultTool) {
			return new PopupMenuTool (editor, this, defaultTool, base.CreateFigureTool (editor, defaultTool)); 
		}

		public override void BasicDrawSelected (Context context) {
			DrawAttribute (context, true);
		}
		
		public override IConnector ConnectorAt (double x, double y) {
			return new ChopEllipseConnector (this);
		}
		
		private void DrawAttribute (Context context, bool selected) {
			double midwidth  = 0;
			double midheight = 0;
			
			RectangleD displayBox = BasicDisplayBox; 
			displayBox.OffsetDot5 ();

			midwidth  = displayBox.Width / 2.0;
			midheight = displayBox.Height / 2.0;

			context.LineWidth = LineWidth + (selected == true ? 2 : 0);

			context.Save ();
			context.Translate (displayBox.X + midwidth, displayBox.Y + midheight);
			context.Scale (midwidth - 1.0, midheight - 1.0);
			context.Arc (0.0, 0.0, 1.0, 0.0, 2.0 * Math.PI);
			context.Restore ();
			if (Multivalued) {
				context.SetDash (Dash.MediumDash, 0);
			}
			if (selected) {
				context.Color = new Color (0, 0, 0, 1);
			} else {
				context.Color = FillColor;
				context.FillPreserve ();
				context.Color = LineColor;
			}
			context.Stroke ();
			if (IsPrimaryKey) {
				context.Restore ();
				PointD bottomLeft  = DisplayBox.BottomLeft;
				PointD bottomRight = DisplayBox.BottomRight;

				bottomLeft.X += Padding;
				bottomLeft.Y -= Padding + 0.5;
				
				bottomRight.X -= Padding;
				bottomRight.Y -= Padding + 0.5;

				context.LineWidth = LineWidth;
				context.MoveTo (bottomLeft); 
				context.LineTo (bottomRight);
				context.Stroke ();
			}
		}
	
		private bool _multiValued; 
		private bool _isPrimaryKey;

	}
}

