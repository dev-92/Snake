@echo off
setlocal EnableDelayedExpansion

REM === Determine base directory automatically ===
cd /d "%~dp0\.."
echo 📁 Working directory: %cd%

REM === Find the latest coverage.cobertura.xml in the TestResults folder ===
for /f "delims=" %%F in ('dir /b /s /a:-d "SnakeCoreTests\TestResults\*.cobertura.xml"') do (
    set "COVERAGE_FILE=%%F"
    goto :found
)

:found
if not defined COVERAGE_FILE (
    echo ❌ No coverage.cobertura.xml found!
    pause
    exit /b 1
)

echo ✅ Found file: %COVERAGE_FILE%

REM === Extract line-rate from XML and convert to percentage ===
for /f %%P in ('powershell -NoProfile -Command "([math]::Round([double](Select-Xml -Path ''%COVERAGE_FILE%'' -XPath ''/coverage/@line-rate'').Node.Value * 100, 2))"') do set "percent=%%P"

echo 📊 Coverage: !percent!%%

REM === Determine badge color ===
set "color=red"
if !percent! GEQ 70 set "color=yellow"
if !percent! GEQ 85 set "color=green"

set "BADGE_URL=https://img.shields.io/badge/coverage-!percent!%%25-!color!"
echo 🏷️ Badge URL: !BADGE_URL!

REM === Update README in project root ===
set "README=README.md"

if not exist "!README!" (
    echo ⚠️ README.md not found in project root!
    pause
    exit /b 0
)

powershell -NoProfile -Command ^
  "(Get-Content '!README!') -replace '!\[Coverage\]\(.*?\)', '![Coverage](!BADGE_URL!)' | Set-Content '!README!'"

echo ✅ README.md updated with new coverage badge.

REM === Optional: automatic commit & push if GITHUB_TOKEN is defined ===
if defined GITHUB_TOKEN (
    git config user.name "github-actions"
    git config user.email "actions@github.com"
    git add README.md
    git diff --quiet || git commit -m "Update coverage badge"
    git push https://x-access-token:%GITHUB_TOKEN%@github.com/%GITHUB_REPOSITORY% HEAD:main
    echo ✅ README.md automatically pushed.
) else (
    echo ⚠️ No GITHUB_TOKEN found, push skipped.
)

endlocal
pause
