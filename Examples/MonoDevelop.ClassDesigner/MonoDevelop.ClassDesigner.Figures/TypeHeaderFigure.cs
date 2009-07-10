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
using System.Collections.Generic;
using Cairo;
using MonoHotDraw.Figures;
using MonoHotDraw.Util;
using MonoHotDraw.Handles;
using MonoHotDraw.Locators;

namespace MonoDevelop.ClassDesigner.Figures {
	
	public class TypeHeaderFigure: VStackFigure {
		
		public TypeHeaderFigure(): base() {
			namespaceFigure = new SimpleTextFigure("Namespace");
			namespaceFigure.Padding = 0;
			namespaceFigure.FontSize = 7;
			
			typeFigure = new SimpleTextFigure("Type");
			typeFigure.Padding = 0;
			typeFigure.FontSize = 7;

			nameFigure = new SimpleTextFigure("Name");
			nameFigure.Padding = 0;
			nameFigure.FontSize = 10;
			
			Spacing = 0.0;
			
			Add(typeFigure);
			Add(namespaceFigure);
			Add(nameFigure);
		}
		
		public string Name {
			get {
				return nameFigure.Text;
			}
			set {
				nameFigure.Text = value;
			}
		}
		
		public string Namespace {
			get {
				return namespaceFigure.Text;
			}
			set {
				namespaceFigure.Text = value;
			}
		}
		
		public string Type {
			get {
				return typeFigure.Text;
			}
			set {
				typeFigure.Text = value;
			}
		}
		
		private SimpleTextFigure namespaceFigure;
		private SimpleTextFigure typeFigure;
		private SimpleTextFigure nameFigure;
	}
}
