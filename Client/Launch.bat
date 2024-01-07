@echo off

if "%1"=="start" (
    start /d "..\Server" "CAPP.Server.exe"
)

if "%1"=="stop" (
    call "cmd.exe" /c taskkill /F /IM "CAPP.Server.exe" /T
)
