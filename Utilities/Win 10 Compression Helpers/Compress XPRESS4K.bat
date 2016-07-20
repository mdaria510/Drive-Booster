:MainLoop
pushd %1
compact /C /S /EXE:XPRESS4K /I /F
shift
if not "%~1"=="" goto MainLoop
pause