
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/UseCases.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../../MonoHotDraw/bin/Debug/MonoHotDraw.dll
BUILD_DIR = bin/Debug

MONOHOTDRAW_DLL_SOURCE=../../MonoHotDraw/bin/Debug/MonoHotDraw.dll
USECASES_EXE_MDB_SOURCE=bin/Debug/UseCases.exe.mdb
USECASES_EXE_MDB=$(BUILD_DIR)/UseCases.exe.mdb

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/UseCases.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../../MonoHotDraw/bin/Release/MonoHotDraw.dll
BUILD_DIR = bin/Release

MONOHOTDRAW_DLL_SOURCE=../../MonoHotDraw/bin/Release/MonoHotDraw.dll
USECASES_EXE_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MONOHOTDRAW_DLL) \
	$(USECASES_EXE_MDB)  

BINARIES = \
	$(USECASESUML)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

FILES = \
	gtk-gui/generated.cs \
	MainWindow.cs \
	AssemblyInfo.cs \
	ActorFigure.cs \
	AssociationCentreLabel.cs \
	AssociationFigure.cs \
	AssociationFigureTool.cs \
	AssociationSideLabel.cs \
	GeneralizationFigure.cs \
	LabelFigure.cs \
	LabelHandle.cs \
	Main.cs \
	NewConnectionHandle.cs \
	NewUseCaseHandle.cs \
	NoteFigure.cs \
	UseCaseFigure.cs \
	gtk-gui/MonoHotDraw.Samples.MainWindow.cs 

DATA_FILES = 

RESOURCES = \
	gtk-gui/gui.stetic \
	icons/actor.png,icons.actor.png \
	icons/comment.png,icons.comment.png \
	icons/connection.png,icons.connection.png \
	icons/usecase.png,icons.usecase.png \
	icons/new_association_handle.png,icons.new_association_handle.png 

EXTRAS = \
	icons \
	usecasesuml.in 

REFERENCES =  \
	System \
	Mono.Posix \
	$(GTK_SHARP_20_LIBS) \
	$(GLIB_SHARP_20_LIBS) \
	$(GLADE_SHARP_20_LIBS) \
	Mono.Cairo

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

include $(top_srcdir)/Makefile.include

MONOHOTDRAW_DLL = $(BUILD_DIR)/MonoHotDraw.dll
USECASESUML = $(BUILD_DIR)/usecasesuml

$(eval $(call emit-deploy-target,MONOHOTDRAW_DLL))
$(eval $(call emit-deploy-wrapper,USECASESUML,usecasesuml,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
