:MainLoop
pushd %1
compact /C /S /EXE:LZX /I /F
shift
if not "%~1"=="" goto MainLoop
pause