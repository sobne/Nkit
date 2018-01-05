@call "%~dp0LoadIni.bat" "%~dp0bat.cfg"
@echo on
net start %sname%
@pause