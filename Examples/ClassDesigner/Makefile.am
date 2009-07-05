
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/ClassDesigner.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../../MonoHotDraw/bin/Debug/MonoHotDraw.dll
BUILD_DIR = bin/Debug

CLASSDESIGNER_EXE_MDB_SOURCE=bin/Debug/ClassDesigner.exe.mdb
CLASSDESIGNER_EXE_MDB=$(BUILD_DIR)/ClassDesigner.exe.mdb
MONOHOTDRAW_DLL_SOURCE=../../MonoHotDraw/bin/Debug/MonoHotDraw.dll

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/ClassDesigner.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../../MonoHotDraw/bin/Release/MonoHotDraw.dll
BUILD_DIR = bin/Release

CLASSDESIGNER_EXE_MDB=
MONOHOTDRAW_DLL_SOURCE=../../MonoHotDraw/bin/Release/MonoHotDraw.dll

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(CLASSDESIGNER_EXE_MDB) \
	$(MONOHOTDRAW_DLL)  

BINARIES = \
	$(CLASSDESIGNER)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

FILES = \
	gtk-gui/generated.cs \
	MainWindow.cs \
	gtk-gui/MainWindow.cs \
	Main.cs \
	AssemblyInfo.cs \
	ClassFigure.cs 

DATA_FILES = 

RESOURCES = \
	gtk-gui/gui.stetic 

EXTRAS = \
	classdesigner.in 

REFERENCES =  \
	System \
	$(GTK_SHARP_20_LIBS) \
	$(GLIB_SHARP_20_LIBS) \
	$(GLADE_SHARP_20_LIBS) \
	Mono.Posix \
	Mono.Cairo

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

include $(top_srcdir)/Makefile.include

CLASSDESIGNER = $(BUILD_DIR)/classdesigner
MONOHOTDRAW_DLL = $(BUILD_DIR)/MonoHotDraw.dll

$(eval $(call emit-deploy-wrapper,CLASSDESIGNER,classdesigner,x))
$(eval $(call emit-deploy-target,MONOHOTDRAW_DLL))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
