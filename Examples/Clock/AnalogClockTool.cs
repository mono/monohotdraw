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
using Gdk;
using MonoHotDraw.Tools;

namespace MonoHotDraw.Samples {

	public class AnalogClockTool : FigureTool {
	
		public AnalogClockTool (IDrawingEditor editor, AnalogClockFigure fig, ITool dt) 
			: base (editor, fig, dt) {
			_figure   = fig;
			_selected = AnalogClockHandSelected.Hour;
		}

		public override void KeyDown (KeyEvent ev) {
			//Rotate through each clock hand
			if (ev.Key == Gdk.Key.Right) {
				switch (_selected) {
					case AnalogClockHandSelected.Hour:
						_selected = AnalogClockHandSelected.Minute;
						break;
					case AnalogClockHandSelected.Minute:
						_selected = AnalogClockHandSelected.Second;
						break;
					case AnalogClockHandSelected.Second:
						_selected = AnalogClockHandSelected.Hour;
						break;
				}
			//Update clock value
			} else if (ev.Key == Gdk.Key.Up) {
				switch (_selected) {
					case AnalogClockHandSelected.Hour:
						if (_figure.Hour == 23)
							_figure.Hour = 0;
						else
							_figure.Hour = _figure.Hour + 1;
						break;
					case AnalogClockHandSelected.Minute:
						if (_figure.Minute == 59)
							_figure.Minute = 0;
						else
							_figure.Minute = _figure.Minute + 1;
						break;
					case AnalogClockHandSelected.Second:
						if (_figure.Second == 59)
							_figure.Second = 0;
						else
							_figure.Second = _figure.Second + 1;
						break;
				}
			} else if (ev.Key == Gdk.Key.Down) {
				switch (_selected) {
					case AnalogClockHandSelected.Hour:
						if (_figure.Hour == 0)
							_figure.Hour = 24;
						else
							_figure.Hour = _figure.Hour - 1;
						break;
					case AnalogClockHandSelected.Minute:
						if (_figure.Minute == 0)
							_figure.Minute = 59;
						else
							_figure.Minute = _figure.Minute - 1;
						break;
					case AnalogClockHandSelected.Second:
						if (_figure.Second == 0)
							_figure.Second = 59;
						else
							_figure.Second = _figure.Second - 1;
						break;
				}
			}
			DefaultTool.KeyUp (ev);
		}
		
		private enum AnalogClockHandSelected {
			Hour,
			Minute,
			Second
		}

		AnalogClockFigure       _figure;
		AnalogClockHandSelected _selected;
	}
}
