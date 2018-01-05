@call "%~dp0LoadIni.bat" "%~dp0bat.cfg"
@echo on
net stop %sname%
"%~dp0%fname%" /uninstall
@pause