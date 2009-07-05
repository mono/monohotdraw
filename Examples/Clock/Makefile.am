
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/Clock.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../../MonoHotDraw/bin/Debug/MonoHotDraw.dll
BUILD_DIR = bin/Debug

CLOCK_EXE_MDB_SOURCE=bin/Debug/Clock.exe.mdb
CLOCK_EXE_MDB=$(BUILD_DIR)/Clock.exe.mdb
MONOHOTDRAW_DLL_SOURCE=../../MonoHotDraw/bin/Debug/MonoHotDraw.dll

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/Clock.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../../MonoHotDraw/bin/Release/MonoHotDraw.dll
BUILD_DIR = bin/Release

CLOCK_EXE_MDB=
MONOHOTDRAW_DLL_SOURCE=../../MonoHotDraw/bin/Release/MonoHotDraw.dll

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(CLOCK_EXE_MDB) \
	$(MONOHOTDRAW_DLL)  

BINARIES = \
	$(CLOCK)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

FILES = \
	gtk-gui/generated.cs \
	AnalogClockFigure.cs \
	AnalogClockHandHandle.cs \
	AnalogClockHandLocator.cs \
	AnalogClockHandLocatorHour.cs \
	AnalogClockHandLocatorMinute.cs \
	AnalogClockHandLocatorSecond.cs \
	AnalogClockNorthWestHandle.cs \
	AnalogClockTool.cs \
	ClockFigureMain.cs \
	DigitalClockFigure.cs \
	DigitalClockTextFigure.cs \
	DigitalClockValueType.cs \
	MainWindow.cs \
	gtk-gui/MonoHotDraw.Samples.MainWindow.cs 

DATA_FILES = 

RESOURCES = \
	gtk-gui/gui.stetic,gtk-gui.gui.stetic 

EXTRAS = \
	clock.in 

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

MONOHOTDRAW_DLL = $(BUILD_DIR)/MonoHotDraw.dll
CLOCK = $(BUILD_DIR)/clock

$(eval $(call emit-deploy-target,MONOHOTDRAW_DLL))
$(eval $(call emit-deploy-wrapper,CLOCK,clock,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
