using System;
using Gtk;
using MonoHotDraw.Figures;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel) {
		Build ();
		
		UpdateUndoRedo();
		mhdcanvas.UndoStackChanged += delegate {
			UpdateUndoRedo();
		};
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnAddEllipseActionActivated (object sender, System.EventArgs e) {
		mhdcanvas.AddWithResizing(new EllipseFigure() );
	}

	protected virtual void OnAddRectangleActionActivated (object sender, System.EventArgs e) {
		mhdcanvas.AddWithResizing(new RectangleFigure());
	}

	protected virtual void OnAddPolyLineActionActivated (object sender, System.EventArgs e) {
		mhdcanvas.AddWithDragging(new PolyLineFigure());
	}

	protected virtual void OnAddSimpleTextActionActivated (object sender, System.EventArgs e) {
		mhdcanvas.AddWithDragging(new SimpleTextFigure("Hello World"));
	}

	protected virtual void OnAddMultiLineTextActionActivated (object sender, System.EventArgs e) {
		mhdcanvas.AddWithDragging(new MultiLineTextFigure("Hello\nWorld"));
	}

	protected virtual void OnAddLineConnectionActionActivated (object sender, System.EventArgs e) {
		LineConnection connection = new LineConnection();
		connection.ConnectionChanged += delegate {
			System.Console.WriteLine("Connection Changed");
		};
		mhdcanvas.AddConnection(connection);
	}

	protected virtual void OnRedoActionActivated (object sender, System.EventArgs e)
	{
		mhdcanvas.Redo();
	}

	protected virtual void OnUndoActionActivated (object sender, System.EventArgs e)
	{
		mhdcanvas.Undo();
	}
	
	protected void UpdateUndoRedo() {
		UndoAction.Sensitive = mhdcanvas.UndoManager.Undoable;
		RedoAction.Sensitive = mhdcanvas.UndoManager.Redoable;
	}
}