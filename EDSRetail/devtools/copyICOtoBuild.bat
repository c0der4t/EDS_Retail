@echo off

#this script is set up and used by c0der4t

if not exist "C:\Users\monte\Dev\EDS_Retail\EDSRetail\Build\Debug\net6.0-windows\ico" mkdir "C:\Users\monte\Dev\EDS_Retail\EDSRetail\Build\Debug\net6.0-windows\ico"

set SOURCE_DIR="C:\Users\monte\Dev\EDS_Retail\EDSRetail\mainModules\ico"
set DESTINATION_DIR="C:\Users\monte\Dev\EDS_Retail\EDSRetail\Build\Debug\net6.0-windows\ico"

robocopy %SOURCE_DIR% %DESTINATION_DIR% /E /MT:4 /R:1 /W:1 /NP


if not exist "C:\Users\monte\Dev\EDS_Retail\EDSRetail\maintenancebot\ico" mkdir "C:\Users\monte\Dev\EDS_Retail\EDSRetail\maintenancebot\ico"

set SOURCE_DIR="C:\Users\monte\Dev\EDS_Retail\EDSRetail\mainModules\ico"
set DESTINATION_DIR="C:\Users\monte\Dev\EDS_Retail\EDSRetail\maintenancebot\ico"

robocopy %SOURCE_DIR% %DESTINATION_DIR% /E /MT:4 /R:1 /W:1 /NP