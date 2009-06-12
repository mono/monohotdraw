//
// MonoHotDraw. Diagramming library
//
// Authors:
//	Mario Carrión <mario@monouml.org>
//
// Copyright (C) 2006, 2007, 2008 MonoUML Team (http://www.monouml.org)
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
//
using System;
using Gtk;
using MonoHotDraw;
using MonoHotDraw.Commands;
using MonoHotDraw.Figures;
using MonoHotDraw.Tools;
using MonoHotDraw.Database;
using MonoHotDraw.Util;

public partial class MainWindow: Gtk.Window, IDrawingEditor {
	
	public MainWindow (): base (Gtk.WindowType.Toplevel) {
		Build ();
		_undoManager = new UndoManager ();
		View = new StandardDrawingView (this);
		_scrolledwindow.Add ((Widget) View);
		Tool = new SelectionTool (this);
		UndoManager.StackChanged += delegate {
			UpdateUndoRedoSensitiveness ();
		};
		ShowAll ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a) {
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnEntityActivated (object sender, System.EventArgs e) {
		Tool = new DragCreationTool (this, new Entity ());
	}

	protected virtual void OnRelationActivated (object sender, System.EventArgs e) {
		Tool = new DragCreationTool (this, new Relation ());
	}

	protected virtual void OnAttributeActivated (object sender, System.EventArgs e) {
		Tool = new DragCreationTool (this, new MonoHotDraw.Database.Attribute ());
	}

	protected virtual void OnQuitActivated (object sender, System.EventArgs e)
	{
		Application.Quit ();
	}
	
	public IDrawingView View {
		get { return _view; }
		set { _view = value; }
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
	
	public UndoManager UndoManager { 
		get { return _undoManager; } 
	}
	
	private void BuildCommandsMenu () {
		Gtk.AccelGroup group = new AccelGroup ();
		Gtk.MenuItem commands = new MenuItem ("Commands");
		Gtk.Menu commandsMenu = new Menu ();
		
		commands.Submenu = commandsMenu;
		menubar1.Append (commands);
		
		//Select all
		Gtk.MenuItem selectAll = new ImageMenuItem (Stock.SelectAll, group);
		selectAll.Activated += delegate {
			UndoableCommand command = new UndoableCommand (new SelectAllCommand ("Select all", this));
			command.Execute ();
		};
		commandsMenu.Append (selectAll);
		
		//Send to back command
		Gtk.MenuItem sendToBack = new ImageMenuItem ("Send to _back");
		sendToBack.Activated += delegate {
		};
		commandsMenu.Append (sendToBack);
		
		//Bring to front command
		Gtk.MenuItem bringToFront = new ImageMenuItem ("Bring to _front");
		bringToFront.Activated += delegate {

		};
		commandsMenu.Append (bringToFront);
		
		//Change attribute
		Gtk.MenuItem changeFontAttribute = new ImageMenuItem ("Change font to Terminus");
		changeFontAttribute.Activated += delegate {

			Console.WriteLine ("Executng");
		};
		commandsMenu.Append (changeFontAttribute);
	}

	protected virtual void OnUndoActivated (object sender, System.EventArgs e) {
		UndoCommand command = new UndoCommand ("Undo", this);
		command.Execute ();
	}

	protected virtual void OnRedoActivated (object sender, System.EventArgs e) {
		RedoCommand command = new RedoCommand ("Redo", this);
		command.Execute ();
	}
	
	protected virtual void OnCommandSendToBack (object sender, System.EventArgs e) {
		UndoableCommand command = new UndoableCommand (new SendToBackCommand ("Send to back", this));
		command.Execute ();
	}

	protected virtual void OnCommandBringToFront (object sender, System.EventArgs e) {
		UndoableCommand command = new UndoableCommand (new BringToFrontCommand ("Bring to front", this));
		command.Execute ();
	}

	protected virtual void OnCommandSelectAll (object sender, System.EventArgs e) {
		UndoableCommand command = new UndoableCommand (new SelectAllCommand ("Select all", this));
		command.Execute ();
	}

	protected virtual void OnCommandFontFamily (object sender, System.EventArgs e) {
		EntryDialog dialog = new EntryDialog ();

		if (dialog.Run () == (int) Gtk.ResponseType.Ok && dialog.EntryValue != string.Empty) {
			UndoableCommand size = new UndoableCommand (new ChangeAttributeCommand ("Font family", 
				FigureAttribute.FontFamily, dialog.EntryValue, this));
			size.Execute ();
		}
		dialog.Destroy ();
	}

	protected virtual void OnCommandFontColor (object sender, System.EventArgs e) {
		ColorDialog dialog = new ColorDialog ();
		if (dialog.Run () == (int) Gtk.ResponseType.Ok) {
			UndoableCommand fontColor = new UndoableCommand (new ChangeAttributeCommand ("Font color",
				FigureAttribute.FontColor, GdkCairoHelper.CairoColor (dialog.Color), this));
			fontColor.Execute ();
		}
		dialog.Destroy ();
	}

	protected virtual void OnCommandFontSize (object sender, System.EventArgs e) {
		EntryDialog dialog = new EntryDialog ();
		int         value = 0;
		
		if (dialog.Run () == (int) Gtk.ResponseType.Ok && dialog.EntryValue != string.Empty) {
			if (int.TryParse (dialog.EntryValue, out value) == true) {
				UndoableCommand size = new UndoableCommand (new ChangeAttributeCommand ("Font size", 
					FigureAttribute.FontSize, value, this));
				size.Execute ();
			}
		}
		dialog.Destroy ();
	}

	protected virtual void OnColorLine (object sender, System.EventArgs e) {
		ColorDialog dialog = new ColorDialog ();
		if (dialog.Run () == (int) Gtk.ResponseType.Ok) {
			UndoableCommand colorLine = new UndoableCommand (new ChangeAttributeCommand ("Line color",
				FigureAttribute.LineColor, GdkCairoHelper.CairoColor (dialog.Color), this));
			colorLine.Execute ();
		}
		dialog.Destroy ();
	}

	protected virtual void OnColorFill (object sender, System.EventArgs e) {
		ColorDialog dialog = new ColorDialog ();
		if (dialog.Run () == (int) Gtk.ResponseType.Ok) {
			UndoableCommand fillColor = new UndoableCommand (new ChangeAttributeCommand ("Fill color",
				FigureAttribute.FillColor, GdkCairoHelper.CairoColor (dialog.Color), this));
			fillColor.Execute ();
		}
		dialog.Destroy ();
	}
	
	private void UpdateUndoRedoSensitiveness () {
		undo.Sensitive = UndoManager.Undoable;
		redo.Sensitive = UndoManager.Redoable;
	}

	protected virtual void OnCutActivated (object sender, System.EventArgs e) {
		ICommand command = new CutCommand ("Cut command", this);
		if (command.IsExecutable == true) {
			command.Execute ();
		} else {
			MessageDialog dialog = new MessageDialog (this, DialogFlags.DestroyWithParent, 
				MessageType.Error, ButtonsType.Close, "Select figures to cut.");
			dialog.Run ();
			dialog.Destroy ();
		}
	}

	protected virtual void OnCopyActivated (object sender, System.EventArgs e) {
		ICommand command = new CopyCommand ("Copy command", this);
		if (command.IsExecutable == true) {
			command.Execute ();
		} else {
			MessageDialog dialog = new MessageDialog (this, DialogFlags.DestroyWithParent, 
				MessageType.Error, ButtonsType.Close, "Select figures to copy.");
			dialog.Run ();
			dialog.Destroy ();
		}
	}

	protected virtual void OnPasteActivated (object sender, System.EventArgs e) {
		ICommand command = new PasteCommand ("Paste command", this);
		if (command.IsExecutable == true) {
			command.Execute ();
		} else {
			MessageDialog dialog = new MessageDialog (this, DialogFlags.DestroyWithParent, 
				MessageType.Error, ButtonsType.Close, "Unable to paste.");
			dialog.Run ();
			dialog.Destroy ();
		}
	}

	protected virtual void OnDeleteActivated (object sender, System.EventArgs e) {
		ICommand command = new DeleteCommand ("Delete command", this);
		if (command.IsExecutable == true) {
			command.Execute ();
		} else {
			MessageDialog dialog = new MessageDialog (this, DialogFlags.DestroyWithParent, 
				MessageType.Error, ButtonsType.Close, "Select figures to delete.");
			dialog.Run ();
			dialog.Destroy ();
		}
	}

	protected virtual void OnDuplicateActivated (object sender, System.EventArgs e) {
		ICommand command = new DuplicateCommand ("Duplicate command", this);
		if (command.IsExecutable == true) {
			command.Execute ();
		} else {
			MessageDialog dialog = new MessageDialog (this, DialogFlags.DestroyWithParent, 
				MessageType.Error, ButtonsType.Close, "Select figures to delete.");
			dialog.Run ();
			dialog.Destroy ();
		}
	}
		
	private IDrawingView _view;
	private ITool _tool;
	private UndoManager _undoManager;
}
