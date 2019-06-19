using System;
using System.Text;
using System.IO;

namespace Nkit
{
    /// <summary>
    /// 文件处理帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="FileName">文件名称(全路径)</param>
        /// <returns></returns>
        public static string Read(string FileName)
        {
            string str = string.Empty;
            if (!ExistsFile(FileName))
                throw new FieldAccessException("文件不存在!");
            try
            {
                StreamReader reader = new StreamReader(FileName, Encoding.Default);
                str = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="FileName">文件名称(全路径)</param>
        /// <param name="content">内容</param>
        public static void Write(string FileName, string content)
        {
            try
            {
                FileStream stream = new FileStream(FileName, FileMode.Append);
                StreamWriter writer = new StreamWriter(stream, Encoding.Default);
                writer.Write(content);
                writer.Close();
                stream.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        private static object _locker = new object();
        public static void Append(string fullFileName,string content)
        {
            try
            {
                lock (_locker)
                {
                    using (FileStream fs = new FileStream(fullFileName, FileMode.OpenOrCreate))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.BaseStream.Seek(0, SeekOrigin.End);
                            sw.WriteLine(content);
                            sw.Flush();
                            sw.Close();
                        }
                        fs.Close();
                    }
                }
            }
            catch
            {
            }
        }
        public static void Append(string fullFileName, string[] contents)
        {
            using (FileStream fs = new FileStream(fullFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                fs.Seek(fs.Length, SeekOrigin.Current);
                string content = String.Join(Environment.NewLine, contents) + Environment.NewLine;
                byte[] data = Encoding.UTF8.GetBytes(content);
                fs.Write(data, 0, data.Length);
                fs.Close();
            }
        }
        /// <summary>
        /// 检查文件否具有读写删除权限
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns>如果具有返回true否则false</returns>
        public static bool CheckFilePopedom(string FilePath)
        {
            bool b_tf = true;
            StreamReader sr = File.OpenText(FilePath);
            string s_FileContent = sr.ReadToEnd();
            sr.Close();
            try
            {
                DelFile(FilePath);
                StreamWriter sw = File.CreateText(FilePath);
                sw.Write(s_FileContent);
                sw.Close();
            }
            catch (Exception e)
            {
                b_tf = false;
            }
            return b_tf;
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sSfile">要复制的文件</param>
        /// <param name="sDfile">目标路径</param>
        public static void CopyFile(string sSfile, string sDfile)
        {
            File.Copy(sSfile, sDfile);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="sPathFile">文件路径(物理路径)+名称</param>
        public static void DelFile(string sPathFile)
        {
            if (ExistsFile(sPathFile))
                File.Delete(sPathFile);
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns>存在否?</returns>
        public static bool ExistsFile(string FileName)
        {
            return System.IO.File.Exists(FileName);
        }
        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="FileForder">文件夹名称</param>
        /// <returns></returns>
        public static bool ExistsFileForder(string FileForder)
        {
            return Directory.Exists(FileForder);
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="sPath">路径(绝对路径)</param>
        /// <param name="FolderName">文件夹名称</param>
        public static void CreateFolder(string sPath, string FolderName)
        {
            if (FolderName.Trim().Length > 0)
            {
                try
                {
                    string CreatePath = sPath+ "/" + FolderName;
                    if (!ExistsFileForder(CreatePath))
                        Directory.CreateDirectory(CreatePath);
                }
                catch
                {
                    throw;
                }
            }
        }
        public static void CreateFloder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// 删除一个文件夹下面的子文件夹和文件 
        /// </summary>
        /// <param name="FolderPathName">文件夹路径(绝对路径)名称</param>
        public static void DeleteChildFolder(string FolderPathName)
        {
            if (FolderPathName.Trim().Length > 0)
            {
                try
                {
                    if (ExistsFileForder(FolderPathName))
                        Directory.Delete(FolderPathName, true);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 删除整个文件夹及其子文件夹和文件 
        /// </summary>
        /// <param name="FolderPathName">文件夹路径(绝对路径)名称</param>
        public static void DeleParentFolder(string FolderPathName)
        {
            try
            {
                DirectoryInfo DelFolder = new DirectoryInfo(FolderPathName);
                if (DelFolder.Exists)
                    DelFolder.Delete();
            }
            catch
            {
            }
        } 
    }
}
