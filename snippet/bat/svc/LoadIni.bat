@echo off
call :���� %1
goto :eof
 
 
:���� [����#1=ini�ļ�·��]
set "op="
for /f " usebackq tokens=1* delims==" %%a in ("%~1") do (
    if "%%b"=="" (
        set "op=%%a"
    ) else (
        set "%%a=%%b"
    )
)
goto :eof