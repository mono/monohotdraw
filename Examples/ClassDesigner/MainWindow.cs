using System;
using Gtk;
using MonoHotDraw;
using MonoHotDraw.Tools;
using ClassDesigner;

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

	protected virtual void OnAddClassActionActivated (object sender, System.EventArgs e)
	{
		this.mhdcanvas.Tool = new DragCreationTool(this.mhdcanvas,
		                                           new ClassFigure());
	}
}