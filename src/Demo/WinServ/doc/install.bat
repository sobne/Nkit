%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe E:\private\code\Nkit\src\Demo\WinServ\bin\Debug\WinServ.exe
Net Start ServiceAppDaemon
::sc config ServiceAppDaemon start= auto
@pause