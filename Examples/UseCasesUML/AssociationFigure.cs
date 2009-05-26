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
using MonoHotDraw;

namespace MonoHotDraw.Samples {

	public class AssociationFigure: LineConnection {
	
		public AssociationFigure(): base() {
			_name = new AssociationCentreLabel("", 0.5, -20.0);
			_roleA = new AssociationSideLabel("", -15.0, 35.0);
			_multiplicityA = new AssociationSideLabel("", 15.0, 35.0);
			_roleB = new AssociationSideLabel("", 15.0, 35.0);
			_multiplicityB = new AssociationSideLabel("", -15.0, 35.0);
			
			AddLabel(_name);
			AddLabel(_roleA);
			AddLabel(_roleB);
			AddLabel(_multiplicityA);
			AddLabel(_multiplicityB);
		}
		
		public override bool CanConnectEnd (IFigure figure) {
			
			if (figure is ActorFigure || figure is UseCaseFigure) {
				if (!figure.Includes(StartFigure)) {					
					return true;
				}
			}
			return false;
		}
		
		public override bool CanConnectStart (IFigure figure) {
			if (figure is ActorFigure || figure is UseCaseFigure) {
				if (!figure.Includes(EndFigure)) {
					return true;
				}
			}
			return false;
		}
		
		public override RectangleD InvalidateDisplayBox {
			get {
				RectangleD rect = DisplayBox;
								
				foreach (LabelFigure label in _labels) {
					rect.Add(label.DisplayBox);
				}
				
				rect.Inflate (5.0, 5.0);
				
				return rect;
			}
		}
		
		public override void BasicDraw(Context context) {
			base.BasicDraw(context);
			foreach (LabelFigure label in _labels) {
				label.BasicDraw(context);
			}
		}
		
		public override void BasicDrawSelected(Context context) {
			_roleA.BasicDrawSelected(context, StartPoint);
			_multiplicityA.BasicDrawSelected(context, StartPoint);
			_roleB.BasicDrawSelected(context, EndPoint);
			_multiplicityB.BasicDrawSelected(context, EndPoint);
			_name.BasicDrawSelected(context, _name.Anchor);
		}
		
		public override bool ContainsPoint (double x, double y) {
			foreach (LabelFigure label in _labels) {
				if (label.ContainsPoint (x, y) ) {
					return true;
				}
			}
			return base.ContainsPoint(x, y);
		}
		
		public LabelFigure FindLabel(double x, double y) {
			foreach (LabelFigure label in _labels) {
				if (label.ContainsPoint (x, y) ) {
					return label;
				}
			}
			return null;
		}
		
		public override ITool CreateFigureTool (IDrawingEditor editor, ITool dt) {
			return new AssociationFigureTool (editor, this, dt);
		}
		
		public override bool Includes (IFigure figure) {
			foreach (LabelFigure label in _labels) {
				if (label.Includes (figure)) {
					return true;
				}
			}
			return base.Includes(figure);
		}
		
		public override IEnumerable<IHandle> HandlesEnumerator {
			get {
				foreach (IHandle handle in base.HandlesEnumerator) {
					yield return handle;
				}
					
				foreach (LabelFigure label in _labels) {
					foreach (IHandle handle in label.HandlesEnumerator) {
						yield return handle;
					}
				}
			}
		}
		
		public void Update() {
			WillChange();
			UpdateAnchors();
			Changed();
		}
		
		protected void AddLabel(LabelFigure label) {
			_labels.Add(label);
			label.FigureInvalidated += delegate(object sender, FigureEventArgs args) {
				Invalidate();
			};
			
			label.TextChanged += delegate(object sender, EventArgs args) {
				UpdateConnection();
			};
		}
		
		public override void UpdateConnection() {
			base.UpdateConnection();
			UpdateAnchors();
		}

		
		private void UpdateAnchors() {
		 	// TODO: Refactor repetitive things like this in this class
			if (_name != null) {
				_name.UpdatePosition(Points);
			}
			if (_roleA != null) {
				_roleA.UpdatePosition(StartPoint, PointAt(1));
			}
			if (_multiplicityA != null) {
				_multiplicityA.UpdatePosition(StartPoint, PointAt(1));
			}
			if (_roleB != null) {
				_roleB.UpdatePosition(EndPoint, PointAt(PointCount-2));
			}
			if (_multiplicityB != null) {
				_multiplicityB.UpdatePosition(EndPoint, PointAt(PointCount-2));
			}
		}
		
		private List<LabelFigure> _labels = new List<LabelFigure>();
		private AssociationSideLabel _roleA;
		private AssociationSideLabel _roleB;
		private AssociationSideLabel _multiplicityA;
		private AssociationSideLabel _multiplicityB;
		private AssociationCentreLabel _name;
	}
}
