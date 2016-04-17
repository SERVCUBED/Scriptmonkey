On Error Resume Next

Dim fso, WshShell
Set fso = CreateObject("Scripting.FileSystemObject")
Set WshShell =  CreateObject("WScript.Shell")
msgbox WScript.ScriptFullName
Path = fso.BuildPath(fso.GetParentFolderName(WScript.ScriptFullName), "Register.NETv4.0.30319.bat")

if Path = "" Then
   msgbox "You need to register the extension manually. Run Register.NETv4.0.30319.bat in the installation folder AS AN ADMINISTRATOR."
End If

WshShell.Run ("cmd.exe /c """ & Path & """")