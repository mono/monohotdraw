
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/MonoHotDraw.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

MONOHOTDRAW_DLL_MDB_SOURCE=bin/Debug/MonoHotDraw.dll.mdb
MONOHOTDRAW_DLL_MDB=$(BUILD_DIR)/MonoHotDraw.dll.mdb

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/MonoHotDraw.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

MONOHOTDRAW_DLL_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MONOHOTDRAW_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(MONOHOTDRAW_PC)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	AssemblyInfo.cs \
	ContainerCanvas.cs \
	DrawingEventArgs.cs \
	Event.cs \
	gtk-gui/generated.cs \
	gtk-gui/MonoHotDraw.SteticComponent.cs \
	IDrawing.cs \
	IDrawingEditor.cs \
	IDrawingView.cs \
	KeyEvent.cs \
	MonoHotDraw.Commands/AbstractCommand.cs \
	MonoHotDraw.Commands/AbstractUndoActivity.cs \
	MonoHotDraw.Commands/BringToFrontCommand.cs \
	MonoHotDraw.Commands/ChangeAttributeCommand.cs \
	MonoHotDraw.Commands/Clipboard.cs \
	MonoHotDraw.Commands/CopyCommand.cs \
	MonoHotDraw.Commands/CutCommand.cs \
	MonoHotDraw.Commands/DeleteCommand.cs \
	MonoHotDraw.Commands/DeleteFromDrawingVisitor.cs \
	MonoHotDraw.Commands/DuplicateCommand.cs \
	MonoHotDraw.Commands/FigureTransferCommand.cs \
	MonoHotDraw.Commands/ICommand.cs \
	MonoHotDraw.Commands/InsertIntoDrawingVisitor.cs \
	MonoHotDraw.Commands/IUndoActivity.cs \
	MonoHotDraw.Commands/NullUndoActivity.cs \
	MonoHotDraw.Commands/PasteCommand.cs \
	MonoHotDraw.Commands/RedoCommand.cs \
	MonoHotDraw.Commands/SelectAllCommand.cs \
	MonoHotDraw.Commands/SendToBackCommand.cs \
	MonoHotDraw.Commands/UndoableCommand.cs \
	MonoHotDraw.Commands/UndoCommand.cs \
	MonoHotDraw.Commands/UndoManager.cs \
	MonoHotDraw.Connectors/AbstractConnector.cs \
	MonoHotDraw.Connectors/ChopBoxConnector.cs \
	MonoHotDraw.Connectors/ChopEllipseConnector.cs \
	MonoHotDraw.Connectors/IConnector.cs \
	MonoHotDraw.Figures/AbstractFigure.cs \
	MonoHotDraw.Figures/AttributeFigure.cs \
	MonoHotDraw.Figures/BaseBoxFigure.cs \
	MonoHotDraw.Figures/CompositeFigure.cs \
	MonoHotDraw.Figures/EllipseFigure.cs \
	MonoHotDraw.Figures/FigureAttribute.cs \
	MonoHotDraw.Figures/FigureCollection.cs \
	MonoHotDraw.Figures/FigureEventArgs.cs \
	MonoHotDraw.Figures/IConnectionFigure.cs \
	MonoHotDraw.Figures/IFigure.cs \
	MonoHotDraw.Figures/IFigureVisitor.cs \
	MonoHotDraw.Figures/LineConnection.cs \
	MonoHotDraw.Figures/LineTerminal.cs \
	MonoHotDraw.Figures/MultiLineTextFigure.cs \
	MonoHotDraw.Figures/PolyLineFigure.cs \
	MonoHotDraw.Figures/RectangleFigure.cs \
	MonoHotDraw.Figures/SimpleTextFigure.cs \
	MonoHotDraw.Figures/TriangleArrowLineTerminal.cs \
	MonoHotDraw.Handles/AbstractHandle.cs \
	MonoHotDraw.Handles/ChangeConnectionEndHandle.cs \
	MonoHotDraw.Handles/ChangeConnectionHandle.cs \
	MonoHotDraw.Handles/ChangeConnectionStartHandle.cs \
	MonoHotDraw.Handles/EastHandle.cs \
	MonoHotDraw.Handles/IHandle.cs \
	MonoHotDraw.Handles/LineConnectionHandle.cs \
	MonoHotDraw.Handles/LocatorHandle.cs \
	MonoHotDraw.Handles/NorthEastHandle.cs \
	MonoHotDraw.Handles/NorthHandle.cs \
	MonoHotDraw.Handles/NorthWestHandle.cs \
	MonoHotDraw.Handles/PolyLineHandle.cs \
	MonoHotDraw.Handles/ResizeHandle.cs \
	MonoHotDraw.Handles/SouthEastHandle.cs \
	MonoHotDraw.Handles/SouthHandle.cs \
	MonoHotDraw.Handles/SouthWestHandle.cs \
	MonoHotDraw.Handles/UndoableHandle.cs \
	MonoHotDraw.Handles/WestHandle.cs \
	MonoHotDraw.Locators/ILocator.cs \
	MonoHotDraw.Locators/PolyLineLocator.cs \
	MonoHotDraw.Locators/QuickActionLocator.cs \
	MonoHotDraw.Locators/QuickActionPosition.cs \
	MonoHotDraw.Locators/RelativeLocator.cs \
	MonoHotDraw.Serialization/ColorSerializationSurrogate.cs \
	MonoHotDraw.Serialization/GenericCloner.cs \
	MonoHotDraw.Serialization/PointDSerializationSurrogate.cs \
	MonoHotDraw.Tools/AbstractTool.cs \
	MonoHotDraw.Tools/CompositeFigureTool.cs \
	MonoHotDraw.Tools/ConnectionCreationTool.cs \
	MonoHotDraw.Tools/CreationTool.cs \
	MonoHotDraw.Tools/DragCreationTool.cs \
	MonoHotDraw.Tools/DragTool.cs \
	MonoHotDraw.Tools/FigureTool.cs \
	MonoHotDraw.Tools/HandleTracker.cs \
	MonoHotDraw.Tools/ITool.cs \
	MonoHotDraw.Tools/MultiLineTextTool.cs \
	MonoHotDraw.Tools/PolyLineFigureTool.cs \
	MonoHotDraw.Tools/ResizeCreationTool.cs \
	MonoHotDraw.Tools/SelectAreaTracker.cs \
	MonoHotDraw.Tools/SelectionTool.cs \
	MonoHotDraw.Tools/SimpleTextTool.cs \
	MonoHotDraw.Tools/TextTool.cs \
	MonoHotDraw.Util/CairoFigures.cs \
	MonoHotDraw.Util/CursorFactory.cs \
	MonoHotDraw.Util/Dash.cs \
	MonoHotDraw.Util/FontFactory.cs \
	MonoHotDraw.Util/GdkCairoHelper.cs \
	MonoHotDraw.Util/Geometry.cs \
	MonoHotDraw.Util/RectangleD.cs \
	MouseEvent.cs \
	StandardDrawing.cs \
	StandardDrawingView.cs \
	SteticComponent.cs 

DATA_FILES = 

RESOURCES = \
	gtk-gui/gui.stetic 

EXTRAS = \
	MonoHotDraw.Serialization \
	MonoHotDraw.Commands \
	MonoHotDraw.Locators \
	MonoHotDraw.Connectors \
	MonoHotDraw.Handles \
	MonoHotDraw.Tools \
	MonoHotDraw.Util \
	MonoHotDraw.Figures \
	monohotdraw.pc.in 

REFERENCES =  \
	System \
	Mono.Cairo \
	$(GTK_SHARP_20_LIBS) \
	$(GLIB_SHARP_20_LIBS) \
	Mono.Posix \
	System.Core

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

include $(top_srcdir)/Makefile.include

MONOHOTDRAW_PC = $(BUILD_DIR)/monohotdraw.pc

$(eval $(call emit-deploy-wrapper,MONOHOTDRAW_PC,monohotdraw.pc))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
