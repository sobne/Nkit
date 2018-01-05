@call "%~dp0LoadIni.bat" "%~dp0bat.cfg"
@echo on
"%~dp0%fname%" /install
net start %sname%
@pause