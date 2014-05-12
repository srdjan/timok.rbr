# Microsoft Developer Studio Project File - Name="spread" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Console Application" 0x0103

CFG=spread - Win32 Release
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "spread.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "spread.mak" CFG="spread - Win32 Release"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "spread - Win32 Release" (based on "Win32 (x86) Console Application")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
RSC=rc.exe
# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir "Release"
# PROP BASE Intermediate_Dir "Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 0
# PROP Output_Dir ""
# PROP Intermediate_Dir ""
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_CONSOLE" /D "_MBCS" /YX /FD /c
# ADD CPP /nologo /W3 /GX /Od /D "_CONSOLE" /D "_MBCS" /D "ARCH_PC_WIN95" /D "WIN32" /YX /FD /c
# SUBTRACT CPP /Fr
# ADD BASE RSC /l 0x409 /d "NDEBUG"
# ADD RSC /l 0x409 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:console /machine:I386
# ADD LINK32 wsock32.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:console /machine:I386
# Begin Target

# Name "spread - Win32 Release"
# Begin Source File

SOURCE=..\acm.c
# End Source File
# Begin Source File

SOURCE="..\acp-permit.c"
# End Source File
# Begin Source File

SOURCE=..\Alarm.c
# End Source File
# Begin Source File

SOURCE=..\arch.c
# End Source File
# Begin Source File

SOURCE="..\auth-ip.c"
# End Source File
# Begin Source File

SOURCE="..\auth-null.c"
# End Source File
# Begin Source File

SOURCE=..\Configuration.c
# End Source File
# Begin Source File

SOURCE=..\Data_link.c
# End Source File
# Begin Source File

SOURCE=..\Events.c
# End Source File
# Begin Source File

SOURCE=..\Flow_control.c
# End Source File
# Begin Source File

SOURCE=..\Groups.c
# End Source File
# Begin Source File

SOURCE=..\lex.yy.c
# End Source File
# Begin Source File

SOURCE=..\Log.c
# End Source File
# Begin Source File

SOURCE=..\Membership.c
# End Source File
# Begin Source File

SOURCE=..\memory.c
# End Source File
# Begin Source File

SOURCE=..\message.c
# End Source File
# Begin Source File

SOURCE=..\Network.c
# End Source File
# Begin Source File

SOURCE=..\Protocol.c
# End Source File
# Begin Source File

SOURCE=..\Session.c
# End Source File
# Begin Source File

SOURCE=..\skiplist.c
# End Source File
# Begin Source File

SOURCE=..\Spread.c
# End Source File
# Begin Source File

SOURCE=..\Status.c
# End Source File
# Begin Source File

SOURCE=..\y.tab.c
# End Source File
# End Target
# End Project
