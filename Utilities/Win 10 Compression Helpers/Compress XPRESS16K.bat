:MainLoop
pushd %1
compact /C /S /EXE:XPRESS16K /I /F
shift
if not "%~1"=="" goto MainLoop
pause