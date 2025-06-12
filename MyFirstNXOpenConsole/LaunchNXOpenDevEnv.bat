@echo off
echo Setting up NX Open environment...

:: === Step 1: Set NX environment variables ===
set "UGII_BASE_DIR=C:\Siemens\NX2406"
set "UGII_ROOT_DIR=%UGII_BASE_DIR%\UGII"
set "UGII_USER_DIR=%USERPROFILE%\nx"
set "PATH=%UGII_BASE_DIR%\NXBIN;%UGII_ROOT_DIR%;%PATH%"

echo NX environment configured.

:: === Step 2: Launch Visual Studio ===
echo Launching Visual Studio...

:: Path to Visual Studio (adjust if needed)
set "VS_PATH=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe"

:: Launch Visual Studio with the solution
start "" "%VS_PATH%" 

echo Done.
