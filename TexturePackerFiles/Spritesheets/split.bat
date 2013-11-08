@echo off
setlocal enabledelayedexpansion
if "%1"=="" (
	echo Drag an image file onto this .bat file to split it - Jif
	GOTO END
)
if not exist "%1" (
	echo FILE "%1" doesn't exist!
	GOTO END
)

if not exist "Output" mkdir Output

SET filename=%1 
for %%x in (%filename:\= %) do (set folderName=%%x)
if exist "Output\%folderName:~0,-4%" rmdir /s /q Output\%folderName:~0,-4%
mkdir Output\%folderName:~0,-4%

rem ----------------------
rem - TILESIZE
rem ----------------------
set /a tileSize=16
rem get width and height of image
for /f %%i in ('identify.exe -format "%%w" %1') do set /a imageWidth = %%i
for /f %%i in ('identify.exe -format "%%h" %1') do set /a imageHeight = %%i
echo ---------------------------
echo - Split.bat
echo - Author: Jiffy
echo -------------------------
echo - Splitting up image '%folderName%'
echo - TileSize set to %tileSize%x%tileSize% (change this by editing split.bat)
echo -------------------------
echo -
echo - Width of image is %imageWidth%
echo - Height of image is %imageHeight%
echo - 
set /a tilesWide = %imageWidth%/%tileSize%
set /a loopTilesWide = %tilesWide%-1
echo - %tilesWide% - Tiles Wide
set /a tilesHigh = %imageHeight%/%tileSize%
set /a loopTilesHigh = %tilesHigh%-1
echo - %tilesHigh% - Tiles High
echo - Starting process...
echo -------------------------
rem Make the first tile both 0 and 1 (this is to make FTilemap work correctly)
convert.exe -extract %tileSize%x%tileSize%+0+0 %1 ./Output/%folderName:~0,-4%/0.png
for /L %%Y in (0,1,%loopTilesHigh%) DO (
	for /L %%A in (0,1,%loopTilesWide%) DO (
		set /a xDisp = %%A*%tileSize%
		set /a yDisp = %%Y*%tileSize%
		set /a imageName = %%A+%%Y*%tilesWide% +1
		echo - !imageName!.png - source-!xDisp!x!yDisp!
		convert.exe -extract %tileSize%x%tileSize%+!xDisp!+!yDisp! %1 ./Output/%folderName:~0,-4%/!imageName!.png
	)
)
echo -------------------------
echo Results can be found in Output directory!
:END
echo Done!
pause

