@echo off mode con cols=56 lines=20 title=IP����
@echo ����IP color 39
@echo waiting...
netsh interface ip set address name="��̫��" source=static addr=192.168.5.138 mask=255.255.255.0 gateway=192.168.5.1
netsh interface ipv4 add address name="��̫��" addr=10.199.1.212 mask=255.255.255.0
::netsh interface ip set address name ="��̫��" source=static 192.168.5.138 255.255.255.0 192.168.5.1

netsh interface ip set dns name="��̫��" source=static addr=119.6.6.1 register=primary
netsh interface ip add dns name="��̫��" 61.139.2.69

@echo IP�������!

ping 10.199.1.50 -n 10>nul
ipconfig /all

@pause
::@echo off pause>nul exit 

::route print
::route delete 0.0.0.0 mask 0.0.0.0 10.86.255.254
::route delete 10.0.0.0 mask 255.0.0.0
::route add -p 10.0.0.0 mask 255.0.0.0 10.86.255.254