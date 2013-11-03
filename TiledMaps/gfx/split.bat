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
rmdir /s /q Output
mkdir Output

set /a tileSize=16
rem get width and height of image
for /f %%i in ('identify.exe -format "%%w" %1') do set /a imageWidth = %%i
for /f %%i in ('identify.exe -format "%%h" %1') do set /a imageHeight = %%i

echo TileSize is %tileSize%x%tileSize%
echo Width is %imageWidth%
echo Height is %imageHeight%
set /a tilesWide = %imageWidth%/%tileSize%
set /a loopTilesWide = %tilesWide%-1
echo %tilesWide% - Tiles Wide
set /a tilesHigh = %imageHeight%/%tileSize%
set /a loopTilesHigh = %tilesHigh%-1
echo %tilesHigh% - Tiles High
for /L %%Y in (0,1,%loopTilesHigh%) DO (
	for /L %%A in (0,1,%loopTilesWide%) DO (
		set /a xDisp = %%A*%tileSize%
		set /a yDisp = %%Y*%tileSize%
		set /a imageName = %%A+%%Y*%tilesWide% +1
		echo !imageName!.png - source-!xDisp!x!yDisp!
		convert.exe -extract %tileSize%x%tileSize%+!xDisp!+!yDisp! %1 ./Output/!imageName!.png
	)
)

:END
echo Done!
pause

