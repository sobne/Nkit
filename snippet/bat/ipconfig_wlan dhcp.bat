Echo off 
echo  自动获取IP地址....
netsh interface ip set address name = "WLAN" source = dhcp
echo  自动获取DNS服务器....
netsh interface ip set dns name = "WLAN" source = dhcp
Echo 设置成功!
Pause