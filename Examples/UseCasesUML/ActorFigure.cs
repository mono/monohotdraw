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
using System;
using System.Collections.Generic;
using Cairo;
using Gdk;
using MonoHotDraw;

namespace MonoHotDraw.Samples {

	public class ActorFigure: BaseBoxFigure	{
	
		public ActorFigure(): base() {
		
			DisplayBox = new RectangleD(0.0, 0.0, 60.0, 70.0);
			
			_name = new LabelFigure("actor");
			
			_name.FigureInvalidated += delegate(object sender, FigureEventArgs args) {
				Invalidate();
			};
			
			_name.TextChanged += delegate(object sender, EventArgs args) {
				UpdateNamePosition();
				Changed();
			};
			
			_name.Padding = 4.0;
		}
		
		public override void BasicDraw (Cairo.Context context) {
			if (DisplayBox.Width == 0 || DisplayBox.Height == 0) {
				return;
			}
			context.LineWidth = 1.0;
			context.Save();
			
			context.Translate (DisplayBox.X, DisplayBox.Y);
			context.Scale(DisplayBox.Width, DisplayBox.Height);
			
			context.Arc(0.5, 0.15, 0.15, 0.0, 2.0 * Math.PI); // head
			
			context.MoveTo(0.5, 0.30); // body
			context.LineTo(0.5, 0.75); // body
			
			context.LineTo(0.15, 1.0); // left leg
			
			context.MoveTo(0.5, 0.75); // right leg
			context.LineTo(0.85, 1.0);  // right leg
			
			context.MoveTo(0.0, 0.40); // arms
			context.LineTo(1.0, 0.40); // arms
			 
			context.Restore ();
			context.Color = new Cairo.Color (0.0, 0.0, 0.0, 1.0);
			context.Stroke ();
			
			_name.BasicDraw(context);
		}
		
		public override void BasicDrawSelected(Context context)	{
			_name.BasicDrawSelected(context);
		}
		
		public override RectangleD InvalidateDisplayBox {
			get {
				RectangleD rect = DisplayBox;
				
				if (_name == null) {
					return rect;
				}
				
				rect.Add(_name.DisplayBox);
				
				rect.Inflate (30.0, 30.0);
				
				return rect;
			}
		}
		
		protected override void OnFigureChanged (FigureEventArgs e) {
			base.OnFigureChanged(e);
			
			if (_name != null) {
				UpdateNamePosition();
			}
		}
		
		public override IEnumerable<IHandle> HandlesEnumerator {
			get {
				foreach (IHandle handle in base.HandlesEnumerator) {
					yield return handle;
				}
					
				yield return new NewUseCaseHandle(this, new QuickActionLocator(15.0, 0.5, QuickActionPosition.Right));
				yield return new NewUseCaseHandle(this, new QuickActionLocator(15.0, 0.5, QuickActionPosition.Left));
			}
		}
		
		public override bool ContainsPoint (double x, double y) {
			if (_name.ContainsPoint (x, y) ) {
				return true;
			}
			return base.ContainsPoint(x, y);
		}
		
		public LabelFigure FindName(double x, double y) {
			if (_name.ContainsPoint (x, y) ) {
				return _name;
			}
			return null;
		}
		
		private void UpdateNamePosition() {
			PointD anchor = new PointD(0.0, 0.0);
			
			anchor.X = DisplayBox.Center.X - _name.DisplayBox.Width/2;
			anchor.Y = DisplayBox.Bottom + 15.0 - _name.DisplayBox.Height/2;
			
			_name.MoveTo(anchor.X, anchor.Y);
		}
		
		// NOTE: Should this be inside??
		public class ActorFigureTool: CompositeFigureTool {
		
			public ActorFigureTool (IDrawingEditor editor, IFigure fig, ITool dt): base (editor, fig, dt) {
			}

			public override void MouseDown (MouseEvent ev) {
				IDrawingView view = ev.View;
				LabelFigure name = ((ActorFigure)Figure).FindName(ev.X, ev.Y);
				
				if (name != null && view.IsFigureSelected(Figure) ) {
					DelegateTool = new SimpleTextTool(Editor, name, DefaultTool);
				}
				else {
					DelegateTool = DefaultTool;
				}
					
				DelegateTool.MouseDown (ev);
			}
		}
		
		public override ITool CreateFigureTool (IDrawingEditor editor, ITool dt) {
			return new ActorFigureTool (editor, this, dt);
		}
		
		public class ActorConnector: ChopBoxConnector {
		
			public ActorConnector(IFigure figure): base(figure)	{
			}
			
			protected override PointD Chop (IFigure target, PointD point) {
				RectangleD actorRect = target.DisplayBox;
				RectangleD nameRect = ((ActorFigure)target).Name.DisplayBox;
				
				PointD nameTopLeft = new PointD(nameRect.Left, nameRect.Top);
				PointD nameTopRight = new PointD(nameRect.Right, nameRect.Top);
				
				if (MonoHotDraw.Geometry.LineIntersection(actorRect.Center, point, nameTopLeft, nameTopRight) != null) {
					double angle = MonoHotDraw.Geometry.AngleFromPoint (nameRect, point);
					return MonoHotDraw.Geometry.EdgePointFromAngle (nameRect, angle);
				}
				else {
					double angle = MonoHotDraw.Geometry.AngleFromPoint (actorRect, point);
					return MonoHotDraw.Geometry.EdgePointFromAngle (actorRect, angle);
				}
			}
		}
		
		public override IConnector ConnectorAt (double x, double y) {
			return new ActorConnector (this);
		}
		
		public LabelFigure Name {
			get { return _name; }
		}
		private LabelFigure _name;
	}
}
