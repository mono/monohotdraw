// MonoDevelop ClassDesigner
//
// Authors:
//	Manuel Cerón <ceronman@gmail.com>
//
// Copyright (C) 2009 Manuel Cerón
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
using Cairo;
using MonoHotDraw.Figures;
using MonoHotDraw.Util;

namespace MonoDevelop.ClassDesigner.Figures {
	
	public class TypeHeaderFigure: BaseBoxFigure {
		
		public TypeHeaderFigure(): base()
		{
			namespaceFigure = new SimpleTextFigure("Namespace");
			namespaceFigure.FontSize = 8;
			
			typeFigure = new SimpleTextFigure("Type");
			typeFigure.FontSize = 8;
			
			nameFigure = new SimpleTextFigure("Name");
			nameFigure.FontSize = 14;
		}
		
		public override void BasicDraw (Cairo.Context context)
		{
			DrawFrame(context);
			
			namespaceFigure.BasicDraw(context);
			typeFigure.BasicDraw(context);
			nameFigure.BasicDraw(context);
		}
		
		public override RectangleD BasicDisplayBox {
			get {
				return base.BasicDisplayBox;
			}
			set {
				RectangleD rect = value;
				RectangleD namespaceRect = namespaceFigure.DisplayBox;
				RectangleD typeRect = typeFigure.DisplayBox;
				RectangleD nameRect = nameFigure.DisplayBox;
				
				double margin = marginLeft + marginRight;
				
				rect.Width = Math.Max(namespaceRect.Width + margin, rect.Width);
				rect.Width = Math.Max(typeRect.Width + margin, rect.Width);
				rect.Width = Math.Max(nameRect.Width + margin, rect.Width);
				
				double minHeight = marginTop + marginBottom;
				minHeight += namespaceRect.Height;
				minHeight += typeRect.Height;
				minHeight += nameRect.Height;
				minHeight += spacing * 2;
				
				rect.Height = Math.Max(minHeight, rect.Height);
				
				base.BasicDisplayBox = rect;
			}
		}
		
		protected override void OnFigureChanged (FigureEventArgs e)
		{
			RectangleD rect = DisplayBox;
			
			double x = rect.X + marginLeft;
			double y = rect.Y + marginTop;
			
			namespaceFigure.MoveTo(x, y);
			
			y += namespaceFigure.DisplayBox.Height + spacing;
			
			typeFigure.MoveTo(x, y);
			
			y += typeFigure.DisplayBox.Height + spacing;
			
			nameFigure.MoveTo(x, y);
		}
		
		private void DrawFrame(Cairo.Context context)
		{
			RectangleD rect = DisplayBox;
			context.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
		}
		
		private SimpleTextFigure namespaceFigure;
		private SimpleTextFigure typeFigure;
		private SimpleTextFigure nameFigure;
		
		private double marginTop = 2.0;
		private double marginLeft = 2.0;
		private double marginRight = 2.0;
		private double marginBottom = 2.0;
		private double spacing = 0.0;
	}
}
