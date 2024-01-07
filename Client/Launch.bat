@echo off

if "%1"=="start" (
    cd "..\Server"
    call ".\Server.exe"
)

if "%1"=="stop" (
    call "cmd.exe" /c taskkill /F /IM "Server.exe" /T
)
