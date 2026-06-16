@echo off
title Lumi Beauty Baslatiliyor

echo Lumi Beauty backend baslatiliyor...
start "LumiBeauty Backend" cmd /k "cd /d %~dp0backend\LumiBeauty.Api && dotnet run"

timeout /t 4 /nobreak >nul

echo Lumi Beauty frontend baslatiliyor...
start "LumiBeauty Frontend" cmd /k "cd /d %~dp0frontend && npm run dev"

timeout /t 4 /nobreak >nul

echo Tarayici aciliyor...
start http://localhost:5174

exit
