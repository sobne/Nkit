@echo off mode con cols=56 lines=20 title=IP����
@echo ����IP color 39
@echo waiting...
netsh interface ip set address name="��̫��" source=static addr=192.168.5.138 mask=255.255.255.0 gateway=192.168.5.1
::netsh interface ip set address name ="��̫��" source=static 192.168.5.138 255.255.255.0 192.168.5.1

netsh interface ip set dns name="��̫��" source=static addr=119.6.6.1 register=primary
netsh interface ip add dns name="��̫��" 61.139.2.69

@echo IP�������!
@pause
::@echo off pause>nul exit 