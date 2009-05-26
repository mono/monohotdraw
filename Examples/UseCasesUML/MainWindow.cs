using System;
using Gtk;
using MonoHotDraw;
using MonoHotDraw.Samples;

namespace MonoHotDraw.Samples
{
	public partial class MainWindow: Gtk.Window, IDrawingEditor
	{	
		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build ();
			
			View = new StandardDrawingView (this);
			scrolledwindow.Add((Widget) View);
			Tool = new SelectionTool(this);
		}
		
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected virtual void OnAddActorActivated (object sender, System.EventArgs e) {
			Console.WriteLine("Actor Added");
			Tool = new DragCreationTool(this, new ActorFigure() );
		}
	
		protected virtual void OnAddUseCaseActivated (object sender, System.EventArgs e) {
			Tool = new DragCreationTool(this, new UseCaseFigure() );
		}
	
		protected virtual void OnAddConnectionActivated (object sender, System.EventArgs e) {
			AssociationFigure c = new AssociationFigure();
			Tool = new ConnectionCreationTool(this, c);
		}
	
		protected virtual void OnAddGeneralizationActivated (object sender, System.EventArgs e) {
			GeneralizationFigure c = new GeneralizationFigure();
			Tool = new ConnectionCreationTool(this, c);
		}
	
		protected virtual void OnAddCommentActivated (object sender, System.EventArgs e) {
			Tool = new DragCreationTool(this, new NoteFigure("new note") );
		}

		protected virtual void OnQuitActionActivated (object sender, System.EventArgs e)
		{
			Application.Quit();
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
				
				_tool.Activate();
			}
		}
		
		private IDrawingView _view;
		private ITool _tool;
	}
}
