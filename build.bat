set ROOT=%CD%\..\Aculab

set SAVEDINCLUDE=%INCLUDE%
set INCLUDE=%ROOT%\example_code\os_specific\WINNT;%ROOT%\include;%ROOT%\TING\include;%ROOT%\TiNG\pubdoc\gen;%ROOT%\TiNG\highapi;%INCLUDE%;
set SAVEDLIB=%LIB%
set LIB=%ROOT%\Lib;%LIB%;

nant -buildfile:rbr.build -D:softswitch=true -D:version=2.80 -v+ -l:nant.log full

set INCLUDE=%SAVEDINCLUDE%
set LIB=%SAVEDLIB%

cmd