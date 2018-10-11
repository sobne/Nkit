%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe E:\private\code\Nkit\src\Demo\WinServ\bin\Debug\WinServ.exe
Net Start ServiceTest
::sc config ServiceTest start= auto
@pause