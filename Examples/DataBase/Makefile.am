
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG
ASSEMBLY = bin/Debug/DbApplication.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../../MonoHotDraw/bin/Debug/MonoHotDraw.dll
BUILD_DIR = bin/Debug

DBAPPLICATION_EXE_MDB_SOURCE=bin/Debug/DbApplication.exe.mdb
DBAPPLICATION_EXE_MDB=$(BUILD_DIR)/DbApplication.exe.mdb
MONOHOTDRAW_DLL_SOURCE=../../MonoHotDraw/bin/Debug/MonoHotDraw.dll

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+
ASSEMBLY = bin/Release/DbApplication.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../../MonoHotDraw/bin/Release/MonoHotDraw.dll
BUILD_DIR = bin/Release

DBAPPLICATION_EXE_MDB=
MONOHOTDRAW_DLL_SOURCE=../../MonoHotDraw/bin/Release/MonoHotDraw.dll

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(DBAPPLICATION_EXE_MDB) \
	$(MONOHOTDRAW_DLL)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) 

FILES = \
	MainWindow.cs \
	Main.cs \
	AssemblyInfo.cs \
	Entity.cs \
	NewConnectionHandle.cs \
	EntityAttributeHandle.cs \
	ConnectionIconFactory.cs \
	Attribute.cs \
	EntityRelationHandle.cs \
	Relation.cs \
	RelationEntityHandle.cs \
	RelationAttributeHandle.cs \
	AttributeConnection.cs \
	EntityConnection.cs \
	RelationConnection.cs \
	PopupMenuTool.cs \
	IPopupMenuFigure.cs \
	ChopRelationConnector.cs \
	EntryDialog.cs \
	ColorDialog.cs \
	gtk-gui/generated.cs \
	gtk-gui/MainWindow.cs \
	gtk-gui/MonoHotDraw.Database.EntryDialog.cs \
	gtk-gui/MonoHotDraw.Database.ColorDialog.cs 

DATA_FILES = 

RESOURCES = \
	icons/relation.png \
	icons/entity.png \
	icons/relation_handle.png \
	icons/attribute_handle.png \
	icons/entity_handle.png \
	gtk-gui/gui.stetic 

EXTRAS = \
	icons 

REFERENCES =  \
	System \
	Mono.Cairo \
	$(GTK_SHARP_20_LIBS) \
	$(GLIB_SHARP_20_LIBS) \
	Mono.Posix

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) 

include $(top_srcdir)/Makefile.include

MONOHOTDRAW_DLL = $(BUILD_DIR)/MonoHotDraw.dll

$(eval $(call emit-deploy-target,MONOHOTDRAW_DLL))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
