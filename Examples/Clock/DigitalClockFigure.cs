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
using System;
using System.Collections.Generic;

namespace MonoHotDraw.Samples {

	public class DigitalClockFigure : BaseBoxFigure {
		
		public DigitalClockFigure () {
			_now = DateTime.Now;
		
			_hour            = new DigitalClockTextFigure (DigitalClockValueType.Hour, "XX");
			_hourSeparator   = new DigitalClockTextFigure (DigitalClockValueType.Separator, ":");
			_hourSeparator.TextEditable = false;
			_minute          = new DigitalClockTextFigure (DigitalClockValueType.Minute, "XX");
			_minuteSeparator = new DigitalClockTextFigure (DigitalClockValueType.Separator, ":");
			_minuteSeparator.TextEditable = false;
			_second          = new DigitalClockTextFigure (DigitalClockValueType.Second, "XX");

			_list = new List <SimpleTextFigure> ();
			_list.Add (_hour);
			_list.Add (_hourSeparator);
			_list.Add (_minute);
			_list.Add (_minuteSeparator);
			_list.Add (_second);
			
			foreach (DigitalClockTextFigure figure in _list) {
				figure.TextChanged += delegate (object sender, EventArgs args) {
					MoveTextFigures ();
					UpdateDateTime ();
				};
				figure.FigureChanged += delegate (object sender, FigureEventArgs args) {
					
					UpdateDisplayBox ();
				};
			}

			GLib.Timeout.Add (500, new GLib.TimeoutHandler (UpdateClock));
		}
		
		public int Hour {
			get { return _now.Hour; }
			set {
				if (value >= 0)
					_now = new DateTime (_now.Year, _now.Month, _now.Day, value, Minute, Second);
			}
		}
		
		public int Minute {
			get { return _now.Minute; }
			set {
				if (value >= 0) {
					_now = new DateTime (_now.Year, _now.Month, _now.Day, Hour, value, Second);
				}
			}
		}
		
		public int Second {
			get { return _now.Second; }
			set {
				if (value >= 0) {
					_now = new DateTime (_now.Year, _now.Month, _now.Day, Hour, Minute, value);
				}
			}
		}
		
		public class DigitalClockFigureTool : CompositeFigureTool {
		
			public DigitalClockFigureTool  (IDrawingEditor editor, IFigure fig, ITool dt)
				: base (editor, fig, dt) {
			}

			public override void MouseDown (MouseEvent ev) {
				IDrawingView view = ev.View;
				DigitalClockTextFigure figure = ((DigitalClockFigure) Figure).FindTextFigure (ev.X, ev.Y);
				
				if (figure != null && view.IsFigureSelected (Figure)) {
					DelegateTool = new SimpleTextTool (Editor, figure, DefaultTool);
				} else {
					DelegateTool = DefaultTool;
				}

				DelegateTool.MouseDown (ev);
			}
		}
		
		public DigitalClockTextFigure FindTextFigure (double x ,double y) {
			foreach (DigitalClockTextFigure figure in _list) {
				if (figure.ContainsPoint (x, y))
					return figure;
			}
			
			return null;
		}
		
		public override ITool CreateFigureTool (IDrawingEditor editor, ITool dt) {
			return new DigitalClockFigureTool (editor, this, dt);
		}

		public override void BasicDraw (Context context) {
			_hour.Text   = String.Format ("{0:00}", _now.Hour);
			_minute.Text = String.Format ("{0:00}", _now.Minute);
			_second.Text = String.Format ("{0:00}", _now.Second);
			foreach (DigitalClockTextFigure figure in _list) {
				figure.BasicDraw (context);
			}
		}
		
		public override void BasicDrawSelected (Context context) {
			RectangleD displayBox = BasicDisplayBox; 
			displayBox.OffsetDot5 ();

			context.LineWidth = LineWidth + 0.5;
			context.Save ();
			context.Rectangle (new PointD (displayBox.X, displayBox.Y), 
			                  displayBox.Width, displayBox.Height);
			context.Save ();
			context.Color = new Color (0.6, 0.6, 1.0, 1.0);
			context.Stroke ();
		}

		public override IEnumerable<IHandle> HandlesEnumerator {
			get {
				//TODO: Return selected handle
				foreach (IFigure figure in _list) {
					foreach (IHandle handle in figure.HandlesEnumerator)
						yield return handle;
				}
			}
		}

		protected override void OnFigureChanged (FigureEventArgs e) {
			base.OnFigureChanged (e);
			MoveTextFigures ();
		}

		private void MoveTextFigures () {
			RectangleD current = DisplayBox;

			_hour.MoveTo (current.X, current.Y);

			current.X += _hour.DisplayBox.Width;
			_hourSeparator.MoveTo (current.X, current.Y);
			
			current.X += _hourSeparator.DisplayBox.Width;
			_minute.MoveTo (current.X, current.Y);
			
			current.X += _minute.DisplayBox.Width;
			_minuteSeparator.MoveTo (current.X, current.Y);
			
			current.X += _minuteSeparator.DisplayBox.Width;
			_second.MoveTo (current.X, current.Y);
		}

		private void UpdateDisplayBox () {
			RectangleD displayBox = DisplayBox;

			displayBox.Height = _hour.DisplayBox.Height;
			displayBox.Width  =_second.DisplayBox.X + _second.DisplayBox.Width - DisplayBox.X;
			//Add the space to set the handles to change
			displayBox.Width += 30;
			DisplayBox        = displayBox;
		}

		private bool UpdateClock () {
			_now = _now.AddSeconds (1);
			Invalidate ();
			return true;
		}
		
		private void UpdateDateTime () {
			int hour   = 0;
			int minute = 0;
			int second = 0;
			
			int.TryParse (_hour.Text, out hour);
			int.TryParse (_minute.Text, out minute);
			int.TryParse (_second.Text, out second);
			
			_now = new DateTime (_now.Year, _now.Month, _now.Day, hour, minute, second);
		}

		private SimpleTextFigure       _hour;
		private SimpleTextFigure       _hourSeparator;
		private List<SimpleTextFigure> _list;
		private SimpleTextFigure       _minute;
		private SimpleTextFigure       _minuteSeparator;
		private DateTime               _now;
		private SimpleTextFigure       _second;
	}
}
