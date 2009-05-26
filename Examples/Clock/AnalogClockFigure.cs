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
using Gtk;
using MonoHotDraw;
using System;
using System.Collections.Generic;

namespace MonoHotDraw.Samples {

	public class AnalogClockFigure : EllipseFigure {

		public AnalogClockFigure () : base () {
			DisplayBox = new RectangleD (0.0, 0.0, 100.0, 100.0);
			
			LineWidth  = 3.0;
			FillColor  = new Color (0.7, 0.7, 0.7, 0.8);
			_now       = DateTime.Now;			
			_handles   = new List <IHandle> ();
			//Handle used to keep height and width synchronized when resizing
			_handles.Add (new AnalogClockNorthWestHandle (this));
			
			_handlesHand = new List <IHandle> ();
			//Handles to move hands
			_handlesHand.Add (new AnalogClockHandHandle (this, new AnalogClockHandLocatorHour ()));
			_handlesHand.Add (new AnalogClockHandHandle (this, new AnalogClockHandLocatorMinute ()));
			_handlesHand.Add (new AnalogClockHandHandle (this, new AnalogClockHandLocatorSecond ()));
			
			_hourColor   = new Color (0.337, 0.612, 0.117, 0.8);
			_minuteColor = new Color (0.117, 0.337, 0.619, 0.8);
			_secondColor = new Color (1, 1, 1, 0.8);
			
			_hourHandLength = 0.0;

			//Timer to update time
			GLib.Timeout.Add (500, new GLib.TimeoutHandler (UpdateClock));
		}
			
		public override IEnumerable<IHandle> HandlesEnumerator {
			get {
				foreach (IHandle handle in _handles) {
					yield return handle;
				}

				foreach (IHandle handleHand in _handlesHand) {
					yield return handleHand;
				}
			}
		}
		
		public double CenterX {
			get { return _centerX; }
		}
		
		public double CenterY {
			get { return _centerY; }
		}
		
		public double Radius {
			get { return _radius; }
		}
		
		public int Second {
			get { return _now.Second; }
			set {
				if (value >= 0) {
					_now = new DateTime (_now.Year, _now.Month, _now.Day, Hour, Minute, value);
				}
			}
		}
		
		public Color SecondColor {
			get { return _secondColor; }
			set { _secondColor = value; }
		}
		
		public double SecondHandLength  {
			get { return _secondHandLength; }
		}
		
		public int Minute {
			get { return _now.Minute; }
			set {
				if (value >= 0) {
					_now = new DateTime (_now.Year, _now.Month, _now.Day, Hour, value, Second);
				}
			}
		}
		
		public Color MinuteColor {
			get { return _minuteColor; }
			set { _minuteColor = value; }
		}
		
		public double MinuteHandLength  {
			get { return _minuteHandLength; }
		}
		
		public int Hour {
			get { return _now.Hour; }
			set {
				if (value >= 0)
					_now = new DateTime (_now.Year, _now.Month, _now.Day, value, Minute, Second);
			}
		}
		
		public Color HourColor {
			get { return _hourColor; }
			set { _hourColor = value; }
		}
		
		public double HourHandLength {
			get { return _hourHandLength; }
		}

		public override void BasicDraw (Context context) {
			double hours   = 0;
			double minutes = 0;
			PointD moveToPoint;
			double seconds = 0;

			base.BasicDraw (context);
			
			_radius  = DisplayBox.Height / 2;
			_centerX = DisplayBox.Width  / 2;
			_centerY = DisplayBox.Height / 2;

			hours   = (_now.Hour % 12 + _now.Minute / 60) * 30 * Math.PI / 180;
			minutes = _now.Minute * 6 * Math.PI / 180;
			seconds = _now.Second * 6 * Math.PI / 180;

			context.Translate (DisplayBox.X, DisplayBox.Y);
			context.LineWidth = 0.5;

			_secondHandLength = _radius - (_radius / 6) * 1;
			DrawClockHands (context, seconds, 0.8, SecondHandLength, SecondColor);
			_minuteHandLength = _radius - (_radius / 7) * 1;
			DrawClockHands (context, minutes, 3, MinuteHandLength, MinuteColor);
			_secondHandLength = _radius - (_radius / 7) * 2;
			DrawClockHands (context, hours, 2.5, SecondHandLength, HourColor);

			//Draw ticks
			for (int i = 0; i < 60; i++) {
				if (i % 5 == 0) { //Five minute ticks
					context.Color = new Color (0, 0, 0, 1);
					context.LineWidth = 5;
					moveToPoint = new PointD (_centerX + (_radius * Math.Sin (i * 6 * Math.PI / 180)),
							_centerY - (_radius * Math.Cos (i * 6 * Math.PI / 180)));
					context.MoveTo (moveToPoint);
					context.LineTo (_centerX + (_radius / 1.05 * Math.Sin (i * 6 * Math.PI / 180)), 
						_centerY - (_radius / 1.08 * Math.Cos (i * 6 * Math.PI / 180)));
					context.Stroke ();
				} else { //Other ticks
					context.LineWidth = 1;
					context.Color = new Color (0, 0, 0, 1);
					moveToPoint = new PointD (_centerX + (_radius * Math.Sin (i * 6 * Math.PI / 180)), 
						_centerY - (_radius * Math.Cos (i * 6 * Math.PI / 180)));
					context.MoveTo (moveToPoint);
					context.LineTo (
						_centerX + (_radius / 1.08 * Math.Sin (i * 6 * Math.PI / 180)), 
						_centerY - (_radius / 1.08 * Math.Cos (i * 6 * Math.PI / 180))
					);
				}
				context.Stroke ();
			}
		}

		public override ITool CreateFigureTool (IDrawingEditor editor, ITool dt) {
			return new AnalogClockTool (editor, this, dt);
		}

		private void DrawClockHands (Context context, double radians, double thickness, 
			                             double length, Color color) {
			PointD a;
			PointD b;
			PointD c;

			a = new PointD ((_centerX + thickness * 2 * Math.Sin (radians + Math.PI / 2)), 
				(_centerY - thickness * 2 * Math.Cos (radians + Math.PI / 2)));
			context.MoveTo (a);

			b = new PointD ((_centerX + thickness * 2 * Math.Sin (radians - Math.PI / 2)),
				(_centerY - thickness * 2 * Math.Cos (radians - Math.PI / 2)));
			context.LineTo (b.X, b.Y);

			c = new PointD ((_centerX + length * Math.Sin (radians)), 
				(_centerY - length * Math.Cos (radians)));
			context.LineTo (c.X, c.Y);

			context.Color = color;
			context.FillPreserve ();

			context.Stroke ();
		}

		private bool UpdateClock () {
			_now = _now.AddSeconds (1);
			Invalidate ();
			return true;
		}
	
		private double        _centerX;
		private double        _centerY;
		private List<IHandle> _handles;
		private List<IHandle> _handlesHand;
		private Color         _hourColor;
		private double        _hourHandLength;
		private Color         _minuteColor;
		private double        _minuteHandLength;
		private DateTime      _now;
		private double        _radius;
		private Color         _secondColor;
		private double        _secondHandLength;
	}
}
