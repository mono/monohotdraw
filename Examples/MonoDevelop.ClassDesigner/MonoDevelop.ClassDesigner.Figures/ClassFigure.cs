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
using Gtk;
using Gdk;
using MonoHotDraw.Figures;
using MonoDevelop.Core;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Core.Gui;

namespace MonoDevelop.ClassDesigner.Figures {
	
	public class ClassFigure: TypeFigure {
		
		public ClassFigure(): base() {
			fields = new TypeMemberGroupFigure(GettextCatalog.GetString("Fields"));
			properties = new TypeMemberGroupFigure(GettextCatalog.GetString("Properties"));
			methods = new TypeMemberGroupFigure(GettextCatalog.GetString("Methods"));
			events = new TypeMemberGroupFigure(GettextCatalog.GetString("Events"));
			
			AddMemberGroup(fields);
			AddMemberGroup(properties);
			AddMemberGroup(methods);
			AddMemberGroup(events);
		}
		
		public ClassFigure(IType domtype): this() {
			if (domtype == null || domtype.ClassType != ClassType.Class) {
				throw new ArgumentException();
			}
			
			Header.Name = domtype.Name;
			Header.Namespace = domtype.Namespace;
			Header.Type = domtype.ClassType.ToString();
			
			foreach (IField field in domtype.Fields) {
				Pixbuf icon = Services.Resources.GetIcon(field.StockIcon, IconSize.Menu);
				AddField(icon, field.ReturnType.Name, field.Name);
			}
			
			foreach (IProperty property in domtype.Properties) {
				Pixbuf icon = Services.Resources.GetIcon(property.StockIcon, IconSize.Menu);
				AddProperty(icon, property.ReturnType.Name, property.Name);
			}
			
			foreach (IMethod method in domtype.Methods) {
				Pixbuf icon = Services.Resources.GetIcon(method.StockIcon, IconSize.Menu);
				IReturnType ret = method.ReturnType;
				if (ret != null) {
					AddMethod(icon, ret.Name, method.Name);
				}
			}
			
			foreach (IEvent ev in domtype.Events) {
				Pixbuf icon = Services.Resources.GetIcon(ev.StockIcon, IconSize.Menu);
				AddMethod(icon, ev.ReturnType.Name, ev.Name);
			}
		}
		
		public void AddField(Pixbuf icon, string type, string name) {
			fields.AddMember(icon, type, name);
		}
		
		public void AddMethod(Pixbuf icon, string retvalue, string name) {
			methods.AddMember(icon, retvalue, name);
		}
		
		public void AddProperty(Pixbuf icon, string type, string name) {
			properties.AddMember(icon, type, name);
		}
		
		public void AddEvent(Pixbuf icon, string type, string name) {
			events.AddMember(icon, type, name);
		}
		
		TypeMemberGroupFigure fields;
		TypeMemberGroupFigure properties;
		TypeMemberGroupFigure methods;
		TypeMemberGroupFigure events;
	}
}
