using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Nkit.Net
{
    
    public class FtpUtils
    {
        public static int Upload(string localFile, string remoteFile, string serverIp)
        {
            return Upload(localFile, remoteFile, serverIp, "", "");
        }
        public static int Upload(string localFile, string remoteFile, string serverIp, string userId, string Pwd)
        {
            FileInfo f = new FileInfo(localFile);
            string ftpPath = "ftp://" + serverIp + "/";
            SyncDir(ftpPath, remoteFile.Substring(0, remoteFile.LastIndexOf('/')));
            string uri = ftpPath + remoteFile;
            FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(Pwd))
                    req.Credentials = new NetworkCredential(userId, Pwd);
                req.KeepAlive = false;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.UseBinary = true;
                req.ContentLength = f.Length;
                int buffLength = 4096;
                byte[] buff = new byte[buffLength];

                FileStream fs = f.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Stream strm = req.GetRequestStream();
                int contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
                return 0;
            }
            catch (Exception)
            {
                req.Abort();
                return -2;
            }
        }
        public static void SyncDir(string ftpPath, string dirPath)
        {
            string[] dir = dirPath.Split('/');
            for (int i = 0; i < dir.Length; i++)
            {
                if (string.IsNullOrEmpty(dir[i])) continue;
                MakeDir(ftpPath, dir[i]);
                ftpPath += dir[i] + "/";
            }
        }
        public static void MakeDir(string ftpPath, string dirName)
        {
            if (DirExist(ftpPath, dirName)) return;
            try
            {
                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpPath + dirName));
                req.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse res = (FtpWebResponse)req.GetResponse();
                res.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static bool DirExist(string ftpPath, string dirName)
        {
            bool r = false;
            try
            {
                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpPath));
                req.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse res = (FtpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.Default);
                StringBuilder sb = new StringBuilder();
                string line = sr.ReadLine();
                while (line != null)
                {
                    sb.Append(line);
                    sb.Append("|");
                    line = sr.ReadLine();
                }
                string[] datas = sb.ToString().Split('|');
                for (int i = 0; i < datas.Length; i++)
                {
                    if (datas[i].Contains("<DIR>"))
                    {
                        int index = datas[i].IndexOf("<DIR>");
                        string name = datas[i].Substring(index + 5).Trim();
                        if (name == dirName)
                        {
                            r = true;
                            break;
                        }
                    }
                }
                sr.Close();
                sr.Dispose();
                res.Close();
            }
            catch (Exception)
            {

                //throw;
            }
            return r;
        }
    }
}
