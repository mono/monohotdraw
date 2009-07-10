using System;
using Gdk;
using Gtk;
using MonoDevelop.ClassDesigner.Figures;
using MonoHotDraw.Figures;

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
		Pixbuf pb = RenderIcon("gtk-info", IconSize.Button, "");
		mhdcanvas.AddWithDragging(new TypeMemberFigure(pb, "Hello", "World"));
	}

	protected virtual void OnAddMemberGroupActionActivated (object sender, System.EventArgs e)
	{
		TypeMemberGroupFigure group = new TypeMemberGroupFigure("Methods");
		Pixbuf icon = RenderIcon("gtk-info", IconSize.Menu, "");
		
		for (int i=0; i<5; i++) {
			group.AddMember(icon, "int", string.Format("method{0}", i));
		}
		
		mhdcanvas.AddWithDragging(group);
	}

	protected virtual void OnAddSimpleTextFigureActionActivated (object sender, System.EventArgs e)
	{
		SimpleTextFigure figure = new SimpleTextFigure("Hello World");
		figure.Padding = 0;
		mhdcanvas.AddWithDragging(figure);
	}
}