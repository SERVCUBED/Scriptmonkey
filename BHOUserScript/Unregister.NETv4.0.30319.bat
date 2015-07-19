@echo off
 if /i "%processor_architecture%"=="AMD64" GOTO AMD64
 if /i "%PROCESSOR_ARCHITEW6432%"=="AMD64" GOTO AMD64
 GOTO ERR
 :AMD64
  "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" /unregister "%~dp0\Scriptmonkey.dll"
 GOTO EXEC
 :EXEC
	"C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe" /unregister "%~dp0\Scriptmonkey.dll"
 GOTO END
 :ERR
 @echo Unsupported architecture "%processor_architecture%"! Defaulting to x86.
 GOTO EXEC
 pause
 :END