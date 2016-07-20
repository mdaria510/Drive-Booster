:MainLoop
pushd %1
compact /U /S /EXE /I /F
shift
if not "%~1"=="" goto MainLoop
pause