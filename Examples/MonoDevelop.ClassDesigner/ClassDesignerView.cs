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

using System.Collections.Generic;
using Gtk;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Projects.Dom.Parser;
using MonoDevelop.ClassDesigner.Figures;
using MonoHotDraw;
using MonoHotDraw.Tools;

namespace MonoDevelop.ClassDesigner {
	
	public class ClassDesignerView: AbstractViewContent {
		
		public override string StockIconId {
			get {
				return Stock.Convert;
			}
		}
		
		public override void Load (string fileName)
		{
		}
		
		public override bool IsFile {
			get {
				return false;
			}
		}
		
		public override Widget Control {
			get {
				return mhdEditor;
			}
		}
		
		public ClassDesignerView()
		{
			ContentName = GettextCatalog.GetString ("Class Diagram");
			IsViewOnly = true;
			
			mhdEditor = new SteticComponent();
			mhdEditor.ShowAll();
			
			// CODE DOM TEST:
			// THIS IS TEMPORAL
			
			Project project = IdeApp.ProjectOperations.CurrentSelectedProject;
			ProjectDom dom = ProjectDomService.GetProjectDom(project);
			List<ClassFigure> figures = new List<ClassFigure>();
			foreach (IType type in dom.Types) {
				if (type.ClassType == ClassType.Class) {
					figures.Add(new ClassFigure(type));
				}
			}
			
			double x = 50.0;
			double y = 50.0;
			
			foreach (ClassFigure figure in figures)  {
				mhdEditor.View.Drawing.Add(figure);
				figure.MoveTo(x, y);
				x += figure.DisplayBox.Width + 50.0;
				if (x > 1000.0) {
					x = 50.0;
					y += figure.DisplayBox.Height + 100.0;
				}
			}
		}
		
		private SteticComponent mhdEditor;
	}
}
