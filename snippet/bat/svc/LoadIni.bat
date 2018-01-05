@echo off
call :解析 %1
goto :eof
 
 
:解析 [参数#1=ini文件路径]
set "op="
for /f " usebackq tokens=1* delims==" %%a in ("%~1") do (
    if "%%b"=="" (
        set "op=%%a"
    ) else (
        set "%%a=%%b"
    )
)
goto :eof