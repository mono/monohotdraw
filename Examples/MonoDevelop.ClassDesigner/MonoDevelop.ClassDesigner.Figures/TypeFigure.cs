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
using MonoHotDraw.Handles;
using MonoHotDraw.Util;
using MonoHotDraw.Locators;
using MonoDevelop.Core;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Core.Gui;

namespace MonoDevelop.ClassDesigner.Figures {

	public abstract class TypeFigure: VStackFigure {
		
		public TypeFigure(): base() {
			Spacing = 10.0;
			Header = new TypeHeaderFigure();
			members = new VStackFigure();
			Add(Header);

			expandHandle = new ToggleButtonHandle(this, new AbsoluteLocator(10, 20));
			expandHandle.Toggled += delegate(object sender, ToggleEventArgs e) {
				if (e.Active) {
					Add(members);
				}
				else {
					Remove(members);
				}
			};
			expandHandle.Active = false;
			
			CreateGroups();
		}
		
		public TypeFigure(IType domtype): this() {
			if (domtype == null || domtype.ClassType != this.ClassType) {
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
		
		public override void BasicDrawSelected (Cairo.Context context) {
			RectangleD rect = DisplayBox;
			rect.OffsetDot5();
			context.LineWidth = 3.0;
			context.Rectangle(GdkCairoHelper.CairoRectangle(rect));
			context.Stroke();
		}
		
		public override void BasicDraw (Cairo.Context context) {
			RectangleD rect = DisplayBox;
			rect.OffsetDot5();
			context.LineWidth = 1.0;
			context.Rectangle(GdkCairoHelper.CairoRectangle(rect));
			context.Color = new Cairo.Color(1.0, 1.0, 0.7, 0.8);
			context.FillPreserve();
			context.Color = new Cairo.Color(0.0, 0.0, 0.0, 1.0);
			context.Stroke();
			
			base.BasicDraw(context);
		}
		
		public override bool ContainsPoint (double x, double y) {
			return DisplayBox.Contains(x, y);
		}
		
		public override RectangleD DisplayBox {
			get {
				RectangleD rect = base.DisplayBox;
				rect.X -= 20;
				rect.Y -= 10;
				rect.Width += 30;
				rect.Height += 20;
				return rect;
			}
			set {
				base.DisplayBox = value;
			}
		}

		public override IEnumerable<IHandle> HandlesEnumerator {
			get {
				yield return expandHandle;
				foreach (IHandle handle in base.HandlesEnumerator)
					yield return handle;
			}
		}
		
		// FIXME: Use an IType member instead
		public string Name {
			get { return Header.Name; }
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
		
		protected virtual void AddMemberGroup(VStackFigure group) {
			members.Add(group);
		}
		
		protected TypeHeaderFigure Header { get; set; }
		
		protected virtual void CreateGroups() {
			fields = new TypeMemberGroupFigure(GettextCatalog.GetString("Fields"));
			properties = new TypeMemberGroupFigure(GettextCatalog.GetString("Properties"));
			methods = new TypeMemberGroupFigure(GettextCatalog.GetString("Methods"));
			events = new TypeMemberGroupFigure(GettextCatalog.GetString("Events"));
			
			AddMemberGroup(fields);
			AddMemberGroup(properties);
			AddMemberGroup(methods);
			AddMemberGroup(events);
		}
		
		protected virtual ClassType ClassType {
			get {
				return ClassType.Unknown;
			}
		}
		
		protected TypeMemberGroupFigure fields;
		protected TypeMemberGroupFigure properties;
		protected TypeMemberGroupFigure methods;
		protected TypeMemberGroupFigure events;
		
		private VStackFigure members;
		private ToggleButtonHandle expandHandle;
	}
}
