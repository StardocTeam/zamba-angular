rem Remove
caspol.exe -rf "D:\WinControl\SystemApi.dll" -pp off
gacutil.exe -u "D:\WinControl\SystemApi.dll" /nologo
    

rem app
caspol.exe -af "D:\WinControl\SystemApi.dll" -pp off
gacutil.exe -i "D:\WinControl\SystemApi.dll" /nologo
