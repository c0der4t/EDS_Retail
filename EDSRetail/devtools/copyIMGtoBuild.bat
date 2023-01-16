@echo off

#this script is set up and used by c0der4t

if not exist "C:\Users\monte\Dev\EDS_Retail\EDSRetail\Build\Debug\net6.0-windows\img" mkdir "C:\Users\monte\Dev\EDS_Retail\EDSRetail\Build\Debug\net6.0-windows\img"

set SOURCE_DIR="C:\Users\monte\Dev\EDS_Retail\EDSRetail\mainModules\img"
set DESTINATION_DIR="C:\Users\monte\Dev\EDS_Retail\EDSRetail\Build\Debug\net6.0-windows\img"

robocopy %SOURCE_DIR% %DESTINATION_DIR% /E /MT:4 /R:1 /W:1 /NP

if not exist "C:\Users\monte\Dev\EDS_Retail\EDSRetail\maintenancebot\img" mkdir "C:\Users\monte\Dev\EDS_Retail\EDSRetail\maintenancebot\img"

set SOURCE_DIR="C:\Users\monte\Dev\EDS_Retail\EDSRetail\mainModules\img"
set DESTINATION_DIR="C:\Users\monte\Dev\EDS_Retail\EDSRetail\maintenancebot\img"

robocopy %SOURCE_DIR% %DESTINATION_DIR% /E /MT:4 /R:1 /W:1 /NP
