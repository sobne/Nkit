@echo off mode con cols=56 lines=20 title=IP设置
@echo 设置IP color 39
@echo waiting...
netsh interface ip set address name="以太网" source=static addr=192.168.5.138 mask=255.255.255.0 gateway=192.168.5.1
::netsh interface ip set address name ="以太网" source=static 192.168.5.138 255.255.255.0 192.168.5.1

netsh interface ip set dns name="以太网" source=static addr=119.6.6.1 register=primary
netsh interface ip add dns name="以太网" 61.139.2.69

@echo IP设置完毕!
@pause
::@echo off pause>nul exit 