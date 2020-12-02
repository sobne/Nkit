echo off
title windows 7 无线热点设置 By:REKFAN
color 1E
:main
cls
echo.
echo.       /-------------------------------
echo.       ‖     windows 7 无线热点设置    ‖
echo.       -------------------------------/
echo.
echo.            1 - 显示无线热点状态
echo.            2 - 打开无线热点
echo.            3 - 关闭无线热点
echo.            4 - 禁用无线热点
echo.            5 - 启用无线热点
echo.            6 - 更改无线热点参数（SSID/KEY）
echo.            0 - 退出
echo.-----------------------------------------------------------
echo.          首次运行请执行6      
echo.          直接回车可查看说明文档
echo.          http://blog.rekfan.com
set input=
set /p input=请选择【输入序号,然后回车】：
if /i '%input%'=='1' goto main1
if /i '%input%'=='2' goto main2
if /i '%input%'=='3' goto main3
if /i '%input%'=='4' goto main4
if /i '%input%'=='5' goto main5
if /i '%input%'=='6' goto main6
if /i '%input%'=='0' goto end
cls
echo.
pause
goto main

:end
set ssid=
set key=
exit

:main1
cls
echo 系统当前无线热点状态
echo.
netsh wlan show hostednetwork
echo.
pause
goto main

:main2
cls
echo 打开无线热点
echo.
netsh wlan start hostednetwork
echo.
pause
goto main1

:main3
cls
echo 关闭无线热点
echo.
netsh wlan stop hostednetwork
echo.
pause
goto main1

:main4
cls
echo 禁用无线热点
echo.
netsh wlan set hostednetwork mode=disallow
echo.
pause
goto main1

:main5
cls
echo 启用无线热点
echo.
netsh wlan set hostednetwork mode=allow
pause
goto main2

:main6
cls
echo 更改无线热点参数（SSID/KEY）
echo.
netsh wlan stop hostednetwork
echo.
set ssid=
set /p ssid=请输入热点名称：
echo.
set key=
set /p key=请输入网络密码(8位以上)：
echo.
netsh wlan set hostednetwork mode=allow ssid=%ssid% key=%key%
pause
goto main2