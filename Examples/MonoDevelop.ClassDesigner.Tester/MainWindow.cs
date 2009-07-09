using System;
using Gtk;
using MonoDevelop.ClassDesigner.Figures;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnAddClassFigureActionActivated (object sender, System.EventArgs e)
	{
		mhdcanvas.AddWithDragging(new TypeHeaderFigure());
	}

	protected virtual void OnAddStackFigureActionActivated (object sender, System.EventArgs e)
	{
		mhdcanvas.AddWithDragging(new TypeMemberFigure("Hello", "World"));
	}
}