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
using Gdk;
using MonoHotDraw.Figures;
using MonoHotDraw.Handles;
using MonoHotDraw.Locators;
using MonoHotDraw.Util;

namespace MonoDevelop.ClassDesigner.Figures {
	
	public class TypeMemberGroupFigure: VStackFigure {
		
		public TypeMemberGroupFigure(string name): base() {
			Spacing = 5;
			Alignment = VStackAlignment.Left;
			
			groupName = new SimpleTextFigure(name);
			groupName.Padding = 0;
			groupName.FontSize = 10;
			groupName.FontColor = new Cairo.Color(0.3, 0.0, 0.0);
			
			Add(groupName);
			
			membersStack = new VStackFigure();
			membersStack.Spacing = 2;
			
			expandHandle = new ToggleButtonHandle(this, new AbsoluteLocator(-10, 7.5));
			expandHandle.Toggled += delegate(object sender, ToggleEventArgs e) {
				if (e.Active) {
					Add(membersStack);
				}
				else {
					Remove(membersStack);
				}
			};
			expandHandle.Active = true;
		}
		
		public void AddMember(Pixbuf icon, string retValue, string name) {
			TypeMemberFigure member = new TypeMemberFigure(icon, retValue, name);
			membersStack.Add(member);
		}
		
		public override IEnumerable<IHandle> HandlesEnumerator {
			get {
				yield return expandHandle;
			}
		}
		
		public override RectangleD InvalidateDisplayBox {
			get {
				RectangleD rect = base.InvalidateDisplayBox;
				rect.Inflate(15, 0);
				return rect;
			}
		}

		
		private SimpleTextFigure groupName;
		private VStackFigure membersStack;
		private ToggleButtonHandle expandHandle;
	}
}
