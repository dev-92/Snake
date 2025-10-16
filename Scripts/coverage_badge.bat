@echo off
setlocal EnableDelayedExpansion

REM === Basisverzeichnis automatisch bestimmen ===
cd /d "%~dp0\.."
echo 📁 Working directory: %cd%

REM === Neueste coverage.cobertura.xml im TestResults-Ordner suchen ===
for /f "delims=" %%F in ('dir /b /s /a:-d /o-d "SnakeCoreTests\TestResults\*.cobertura.xml"') do (
    set "COVERAGE_FILE=%%F"
    goto :found
)

:found
if not defined COVERAGE_FILE (
    echo ❌ Keine coverage.cobertura.xml gefunden!
    pause
    exit /b 1
)

echo ✅ Gefundene Datei: %COVERAGE_FILE%

REM === Coverage-Wert aus XML extrahieren (line-rate) ===
for /f "tokens=2 delims== " %%A in ('findstr /i "line-rate=" "%COVERAGE_FILE%"') do (
    set "rate=%%A"
    goto :next
)

:next
set "rate=!rate:"=!"
set "rate=!rate:/>=!"
REM === line-rate in Prozent umrechnen mit PowerShell ===
for /f %%P in ('powershell -Command "[math]::Round([double]'!rate!' * 100)"') do set "percent=%%P"

REM === Badge-Farbe bestimmen ===
set "color=red"
if !percent! GEQ 70 set "color=yellow"
if !percent! GEQ 85 set "color=green"

set "BADGE_URL=https://img.shields.io/badge/coverage-!percent!%%25-!color!"

echo 📊 Coverage: !percent!%%
echo 🏷️ Badge URL: !BADGE_URL!

REM === README im Projektstamm aktualisieren ===
set "README=README.md"

if not exist "!README!" (
    echo ⚠️ Keine README.md im Projektstamm gefunden!
    pause
    exit /b 0
)

powershell -Command ^
  "(Get-Content '!README!') -replace '!\[Coverage\]\(.*?\)', '![Coverage](!BADGE_URL!)' | Set-Content '!README!'"

echo ✅ README.md aktualisiert mit neuem Coverage-Badge.
pause
endlocal
