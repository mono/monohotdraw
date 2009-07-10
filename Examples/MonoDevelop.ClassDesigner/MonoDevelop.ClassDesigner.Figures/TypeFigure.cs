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
using MonoHotDraw.Figures;
using MonoHotDraw.Handles;
using MonoHotDraw.Util;
using MonoHotDraw.Locators;

namespace MonoDevelop.ClassDesigner.Figures {

	public abstract class TypeFigure: VStackFigure {
		
		public TypeFigure(): base() {
			Spacing = 10.0;
			Header = new TypeHeaderFigure();
			members = new VStackFigure();
			Add(Header);

			expandHandle = new ToggleButtonHandle(this, new AbsoluteLocator(20, 20));
			expandHandle.Toggled += delegate(object sender, ToggleEventArgs e) {
				if (e.Active) {
					Add(members);
				}
				else {
					Remove(members);
				}
			};
			expandHandle.Active = false;
		}
		
		public override void BasicDrawSelected (Cairo.Context context) {
			RectangleD rect = DisplayBox;
			rect.OffsetDot5();
			context.LineWidth = 2.0;
			context.Rectangle(GdkCairoHelper.CairoRectangle(rect));
			context.Stroke();
		}
		
		public override void BasicDraw (Cairo.Context context) {
			RectangleD rect = DisplayBox;
			rect.OffsetDot5();
			context.LineWidth = 1.0;
			context.Rectangle(GdkCairoHelper.CairoRectangle(rect));
			context.Stroke();
			
			base.BasicDraw(context);
		}
		
		public override RectangleD DisplayBox {
			get {
				RectangleD rect = base.DisplayBox;
				rect.X -= 30;
				rect.Y -= 10;
				rect.Width += 30;
				rect.Height += 10;
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
		
		protected virtual void AddMemberGroup(TypeMemberGroupFigure group) {
			members.Add(group);
		}
		
		protected TypeHeaderFigure Header { get; set; }
		
		private VStackFigure members;
		private ToggleButtonHandle expandHandle;
	}
}
