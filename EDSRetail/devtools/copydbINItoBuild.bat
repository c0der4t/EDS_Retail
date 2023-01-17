@echo off

#this script is set up and used by c0der4t

set SOURCE_DIR="D:\EDS_Retail\EDSRetail\mainModules\bin\Debug\net6.0-windows\db.ini"
set DESTINATION_DIR="C:\Users\monte\Dev\EDS_Retail\EDSRetail\Build\Debug\net6.0-windows"

robocopy %SOURCE_DIR% %DESTINATION_DIR% /E /MT:4 /R:1 /W:1 /NP