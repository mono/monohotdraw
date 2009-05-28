
using System;
using Gtk;

namespace MonoHotDraw
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SteticComponent : Gtk.Bin, IDrawingEditor
	{
		
		public SteticComponent()
		{
			this.Build();
			
			View = new StandardDrawingView (this);
			this.scrolledwindow.Add ((Widget) View);
			Tool = new SelectionTool (this);
		}
		
		public IDrawingView View {
			get { return _view; }
			set { _view = value; }
		}
		
		public UndoManager UndoManager {
			get { return null; }
		}
		
		public ITool Tool {
			get { return _tool; }
			set {
				if (_tool != null && _tool.Activated) {
					_tool.Deactivate();
				}
				
				_tool = value;
				if (value != null) {
					_tool.Activate();
				}
			}
		}
			
		private IDrawingView _view;
		private ITool _tool;
	}
}
