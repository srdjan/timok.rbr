@echo on
setlocal

REM set INCLUDE
REM set PATH
REM set LIB

REM Get VS .Net to set its stuff the way it likes.
REM call "C:\Program Files\Microsoft Visual Studio .NET 2003\Vc7\bin\vcvars32.bat"
call "C:\Program Files\Microsoft Visual Studio 8\VC\bin\vcvars32.bat"

REM Add ours for IVR
set ROOT=%CD%\Aculab
set SAVEDINCLUDE=%INCLUDE%
set INCLUDE=%ROOT%\example_code\os_specific\WINNT;%ROOT%\include;%ROOT%\TING\include;%ROOT%\TiNG\pubdoc\gen;%ROOT%\TiNG\highapi;%INCLUDE%;

set SAVEDLIB=%LIB%
set LIB=%ROOT%\Lib;%LIB%;

"C:\Program Files\Microsoft Visual Studio 8\Common7\IDE\devenv.exe" /useenv Timok.IVR.sln

set INCLUDE=%SAVEDINCLUDE%
set LIB=%SAVEDLIB%
REM set PATH=%SAVEDPATH%
