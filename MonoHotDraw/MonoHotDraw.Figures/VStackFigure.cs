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
using System.Collections.Generic;
using System.Linq;
using Cairo;
using MonoHotDraw.Util;

namespace MonoHotDraw.Figures {
	
	public abstract class VStackFigure: CompositeFigure {
		
		protected VStackFigure() {
			Position = new PointD(0.0, 0.0);
			Spacing = 5.0;
		}
		
		public override RectangleD BasicDisplayBox {
			get {
				return new RectangleD {
					X = Position.X,
					Y = Position.Y,
					Width = this.Width,
					Height = this.Height,
				};
			}
			set {
				Position = value.TopLeft;
				UpdateFiguresPosition();
			}
		}
		
		public virtual void PackFigure(IFigure figure) {
			Add(figure);
			figure.FigureChanged += delegate {
				CalculateDimensions();
				System.Console.WriteLine("Calculating");
			};
			CalculateDimensions();
		}
		
		public double Spacing {
			get { return _spacing; }
			set { 
				_spacing = value;
				CalculateDimensions();
			}
		}
		
		private double CalculateFigureY(IFigure figure) {
			return Position.Y + (Height - figure.DisplayBox.Height)/2;
		}
		
		private void CalculateDimensions()
		{
			WillChange();
			Width = CalculateWidth();
			Height = CalculateHeight();
			UpdateFiguresPosition();
			Changed();
		}
		
		private double CalculateHeight()
		{
			if (Figures.Count() == 0)
				return 0.0;
			return (from IFigure fig in this.Figures
			        select fig.DisplayBox.Height).Max();
		}
		
		private double CalculateWidth() {
			if (Figures.Count() == 0)
				return 0.0;
			return (from IFigure fig in this.Figures
			        select fig.DisplayBox.Width).Sum();
		}
		
		private void UpdateFiguresPosition() {
			double width = 0.0;
			foreach (IFigure figure in Figures) {
				RectangleD r = figure.DisplayBox;
				r.X = Position.X + width;
				r.Y = CalculateFigureY(figure);
				AbstractFigure af = figure as AbstractFigure;
				af.BasicDisplayBox = r;
				width += r.Width + Spacing;
			}
		}
		
		protected PointD Position { get; set; }
		protected double Width { get; set; } 
		protected double Height { get; set; }
		private double _spacing;
	}
}
