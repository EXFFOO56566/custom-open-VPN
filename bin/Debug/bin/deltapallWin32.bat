@echo off
cls
TITLE TAP-Win32 driver Un-Install by Rainbow
Color 0c
echo.
echo.
echo     €x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€ 
echo     x                                                               x
echo     €                TAP-Win32 driver Un-Install                    € 
echo     x                                                               x
echo     €-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-€
echo     x                                                               x 
echo     €                  Press Any Key To Delete!                     € 
echo     x                                                               x
echo     €x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€x€ 
echo.
echo WARNING: this script will delete ALL TAP virtual adapters (use the device manager to delete adapters one at a time)
echo.
pause
cd %~dp0
"tapinstallWin32.exe" remove tap0901
pause