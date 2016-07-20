:Set your game folders here--the defaults are left as the example I use on my system. 
:You must use the folder the *games* are installed to, not the steam/origin/whatever folder.
set HDDFolder=H:\
set SteamCommonFolder=G:\Steam\steamapps\common
set OriginGamesFolder=G:\Origin Games
set UPlayGamesFolder=G:\Uplay Games
set BattleNetGamesFolder=G:\Blizzard Games
set OtherGamesFolder=G:\Other Games

:MainLoop
@ECHO OFF
set GameIsOnSSD=0
if exist "%HDDFolder%\%~n1\" (set GameIsOnSSD=1)
if exist "%HDDFolder%\%~n1\" (goto MovetoHDDLoop)
robocopy /MT:2 /s /ndl %1 "%HDDFolder%\%~n1"
rmdir /s /q %1
mklink /j %1 "%HDDFolder%\%~n1"
:compact /C /S:"%HDDFolder%\%~n1" /EXE:xpress8k


shift
if not "%~1"=="" goto MainLoop
pause
exit

:MovetoHDDLoop
set steam=0
set origin=0
set uplay=0
set blizz=0
set Other=0

:Search game folders to find game location
if exist "%SteamCommonFolder%\%~n1\" (set steam=1)
if exist "%OriginGamesFolder%\%~n1\" (set origin=1)
if exist "%UplayGamesFolder%\%~n1\" (set uplay=1)
if exist "%OtherGamesFolder%\%~n1\" (set Other=1)
if exist "%BattleNetGamesFolder%\%~n1\" (set blizz=1)

:remove junction from HDD location
if %steam%==1 rmdir /q "%SteamCommonFolder%\%~n1\"
if %origin%==1 rmdir /q "%OriginGamesFolder%\%~n1\"
if %uplay%==1 rmdir /q "%UplayGamesFolder%\%~n1\"
if %blizz%==1 rmdir /q "%BattleNetGamesFolder%\%~n1\"
if %Other%==1 rmdir /q "%OtherGamesFolder%\%~n1\"

:copy folder back to HDD location
if %GameIsOnSSD%==1 goto MovetoHDD2  
if %steam%==1 robocopy /MT:2 /s /ndl %1 "%SteamCommonFolder%\%~n1"
:if %steam%==1 compact /C /S:"%SteamCommonFolder%\%~n1" /EXE:xpress8k
if %origin%==1 robocopy /MT:2 /s /ndl %1 "%OriginGamesFolder%\%~n1"
:if %origin%==1 compact /C /S:"%OriginGamesFolder%\%~n1" /EXE:xpress8k
if %uplay%==1 robocopy /MT:2 /s /ndl %1 "%UplayGamesFolder%\%~n1"
:if %uplay%==1 compact /C /S:"%UPlayGamesFolder%\%~n1" /EXE:xpress8k
if %blizz%==1 robocopy /MT:2 /s /ndl %1 "%BattleNetGamesFolder%\%~n1"
:if %blizz%==1 compact /C /S:"%BattleNetGamesFolder%\%~n1" /EXE:xpress8k
if %Other%==1 robocopy /MT:2 /s /ndl %1 "%OtherGamesFolder%\%~n1"
:if %Other%==1 compact /C /S:"%OtherGamesFolder%\%~n1" /EXE:xpress8k

:deletefolder
rmdir /s /q %1 

:MovetoHDD2
if %steam%==1 robocopy /MT:2 /s /ndl "%HDDFolder%\%~n1" "%SteamCommonFolder%\%~n1"
:if %steam%==1 compact /C /S:"%SteamCommonFolder%\%~n1" /EXE:xpress8k
if %origin%==1 robocopy /MT:2 /s /ndl "%HDDFolder%\%~n1" "%OriginGamesFolder%\%~n1"
:if %origin%==1 compact /C /S:"%OriginGamesFolder%\%~n1" /EXE:xpress8k
if %uplay%==1 robocopy /MT:2 /s /ndl "%HDDFolder%\%~n1" "%UplayGamesFolder%\%~n1"
:if %uplay%==1 compact /C /S:"%UPlayGamesFolder%\%~n1" /EXE:xpress8k
if %blizz%==1 robocopy /MT:2 /s /ndl "%HDDFolder%\%~n1" "%BattleNetGamesFolder%\%~n1"
:if %blizz%==1 compact /C /S:"%BattleNetGamesFolder%\%~n1" /EXE:xpress8k
if %Other%==1 robocopy /MT:2 /s /ndl "%HDDFolder%\%~n1" "%OtherGamesFolder%\%~n1"
:if %Other%==1 compact /C /S:"%OtherGamesFolder%\%~n1" /EXE:xpress8k
rmdir /s /q "%HDDFolder%\%~n1" 

shift
if not "%~1"=="" goto MainLoop
pause