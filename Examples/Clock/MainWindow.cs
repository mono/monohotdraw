
using System;
using MonoHotDraw;
using MonoHotDraw.Commands;
using MonoHotDraw.Tools;
using Gtk;

namespace MonoHotDraw.Samples
{
	public partial class MainWindow : Gtk.Window, IDrawingEditor
	{
		public MainWindow() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			
			View = new StandardDrawingView (this);
			this.scrolledwindow1.Add ((Widget) View);
			Tool = new SelectionTool (this);
		}
		
		protected virtual void OnDeleteWindow (object o, Gtk.DeleteEventArgs args) {
			Application.Quit ();
			args.RetVal = true;
		}

		protected virtual void OnAddAction (object sender, System.EventArgs e) {
			Tool = new DragCreationTool (this, new AnalogClockFigure ());
		}

		protected virtual void OnAddAnalogClockActionActivated (object sender, System.EventArgs e)
		{
			Tool = new DragCreationTool (this, new AnalogClockFigure ());
		}

		protected virtual void OnAddDigitalClockActionActivated (object sender, System.EventArgs e)
		{
			Tool = new DragCreationTool (this, new DigitalClockFigure ());
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
