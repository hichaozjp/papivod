using System;
using System.IO;
using System.Web;
using System.Text;
using System.Threading;
 using System.Collections;
using System.Data;
using System.Collections.Generic;
/// <summary>
/// �ļ�����
/// </summary>
public class DoFile
{
    public DoFile()
    {
        //
        // TODO: �ڴ˴����ӹ��캯���߼�
        //
    }

    /// <summary>
    /// ����Ŀ¼
    /// </summary>
    /// <param name="folderpath"></param>
    public static void CreateFolder(string folderpath)
    
    {
        if (!Directory.Exists(folderpath))
            Directory.CreateDirectory(folderpath);

    }
    /// <summary>
    /// ɾ��Ŀ¼���ļ�
    /// </summary>
    /// <param name="folderpath"></param>
    public static void DeleteFolder(string folderpath)
    {
        if (!Directory.Exists(folderpath))
        {
            if(File.Exists("foderpath" + "default.aspx"));
            File.Delete("foderpath" + "default.aspx");
            Directory.Delete(folderpath);
        }
        
    }

    /// <summary>
    /// �����ļ�
    /// </summary>
    /// <param name="sourcefile"></param>
    /// <param name="destfile"></param>
    public static void CopyFile(string sourcefile, string destfile)
    {
        if (File.Exists(sourcefile))
        {
            File.Copy(sourcefile, destfile);
        }
    }

   /// <summary>
    /// ɾ��ָ���ļ�
   /// </summary>
   /// <param name="filename"></param>
   /// <returns></returns>
    public static int DeleteFile(string filename)
    {
        if (filename == string.Empty || filename == "" || filename == null)
            return 0;
        else
        {
            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(filename)))
            {
                File.Delete(System.Web.HttpContext.Current.Server.MapPath(filename));
                return 1;
            }
            return 0;
        }
    }

    /// <summary>
    /// �ж��Ǵ��ڴ��ļ� ���ܵ�������·��
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static bool ExistsFile(string filename)
    {
        return File.Exists(System.Web.HttpContext.Current.Server.MapPath(filename));
    }


    public static bool MoveConfig(int type, string folder)
    {
        string file = "";
        if (type == 0)
        {
            //����Ĭ�ϵĳ���-->�޸�Ϊһ
            file = HttpContext.Current.Server.MapPath("~/" + folder + "/config/html/web.config");


        }
        else
        {
            //�޸�Ϊ������
            file = HttpContext.Current.Server.MapPath("~/" + folder + "/config/aspx/web.config");
        }

        string newfile = HttpContext.Current.Server.MapPath("~/web.config");
        try
        {
            if (File.Exists(file))
            {
                if (File.Exists(newfile))
                {
                    File.Delete(newfile);
                }
                FileInfo fi = new FileInfo(file);

                FileInfo fi1 = fi.CopyTo(newfile);

            }
            return true;
        }
        catch
        {
            return false;
        }

    }



    //���ӷ���

    /// <summary>
    /// �ļ���ַ
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="str">д�������</param>
    public static void WriteFile(string filename, string str)
    {
        if (File.Exists(filename))
        {
             StreamWriter sw = new StreamWriter(filename,false,Encoding.UTF8);
             sw.Write(str);
             sw.Close();
        }
        else
        {
          FileStream fs =  File.Create(filename);
          fs.Flush();
          fs.Close();         
        }
    }



    /// <summary>
    /// ���Ӳ���ļ����ṩ����
    /// </summary>
    /// <param name="_Request">Page.Request����</param>
    /// <param name="_Response">Page.Response����</param>
    /// <param name="_fileName">�����ļ���</param>
    /// <param name="_fullPath">���ļ�������·��</param>
    /// <param name="_speed">ÿ���������ص��ֽ���</param>
    /// <returns>�����Ƿ�ɹ�</returns>
    public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
    {
        try
        {
            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(myFile);
            try
            {
                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;
                long fileLength = myFile.Length;
                long startBytes = 0;



                int pack = 10240; //10K bytes
                //int sleep = 200; //ÿ��5�� ��5*10K bytesÿ��
                int sleep = (int)Math.Floor((decimal)1000 * pack / _speed) + 1;
                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                }
                _Response.AddHeader("Connection", "Keep-Alive");
                _Response.ContentType = "application/octet-stream";
                _Response.Charset = "UTF-8";
                _Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
                _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));



                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int)Math.Floor((decimal)(fileLength - startBytes) / pack) + 1;



                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(pack));
                        Thread.Sleep(sleep);
                    }
                    else
                    {
                        i = maxCount;
                    }
                }
                _Response.End();
            }
            catch (Exception ex)
            {
                _Response.Write(ex.Message);
                return false;
            }
            finally
            {
                br.Close();
                myFile.Close();
            }
        }
        catch (Exception ex)
        {
            _Response.Write(ex.Message);
            return false;
        }
        return true;
    }


    /// <summary>
    /// ��ȡ�ļ�����
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static string ReadFile(string filename)
    {
        StreamReader sr = new StreamReader(filename,Encoding.Default);
        string str = sr.ReadToEnd();

        sr.Close();

        return str;
    }

    /// <summary>
    /// �����ļ���
    /// </summary>
    /// <param name="type">����</param>
    /// <param name="setfname">�ļ���</param>
    /// <param name="content">����</param>
    public static void extendFile(extenFileType type, string setfname, string content)
    {
        HttpResponse response = HttpContext.Current.Response;

        if (type == extenFileType.excel)
        {
            response.ContentType = "application/vnd.ms-excel";
        }
        else
        {
            response.ContentType = "application/ms-word";
        }

        response.AddHeader("content-disposition", "inline;filename="
          + HttpUtility.UrlEncode(setfname, Encoding.UTF8));

        //s sb = new stringbuilder();
        //system.io.stringwriter sw = new system.io.stringwriter(sb);
        //system.web.ui.htmltextwriter hw = new system.web.ui.htmltextwriter(sw);
        //sb.append("<html><body>");
        //dgshow.rendercontrol(hw);
        //sb.append("</body></html>");
        response.Write(content);
        response.End();


    }


    /// <summary>
    /// �������ļ�����
    /// </summary>
    public enum extenFileType
    {
        word,
        excel
    } 

    /// <summary>
    /// ����ָ���ļ�������������ļ��б�
    /// </summary>
    /// <param name="dirpath"></param>
    /// <returns></returns>
    public static IList<string> GetDirFiles(string dirpath)
    {
        if (Directory.Exists(dirpath))
        {
            DirectoryInfo Objdir = new DirectoryInfo(dirpath);

            FileInfo[] fs = Objdir.GetFiles();

            if (fs != null)
            {
                IList<string> ilist = new List<string>();
                foreach (FileInfo fi in fs)
                {
                    ilist.Add(fi.Name);
                }

                return ilist;

            }
        }
        return null;
    }



    /// <summary>
    /// ��������ϴ��ļ������ͺʹ�С��֤  0���ļ����ʹ���. -1���ļ���������ϴ�����.
    /// </summary>
    /// <param name="files">�ļ���</param>
    /// <param name="maxfilesize">�ļ���С</param>
    /// <param name="filetype">�ļ����� �� .jpg,.png,.gif��</param>
    /// <returns>��������ֵ</returns>
    public static  int CheckeFileList(HttpFileCollection files, int maxfilesize, string filetype)
    {
        int intret = 1;
        for (int i = 0; i < files.Count;i++ )
        {
            HttpPostedFile f = files[i];

            if (f.FileName != "" && f.ContentLength > 0)
            {
                //�ж��ļ������ͺʹ�С
                if (checkFileType(filetype, f.FileName))
                {
                    if (checekIsAllowSize(maxfilesize, f))
                    {

                    }
                    else
                        intret = -1;
                }
                else
                    intret = 0;//�ļ����ʹ���
            }

            if (intret != 1)
                break;
        }

        return intret;
    }


    /// <summary>
    ///  �����ļ������Ƿ���ȷ
    /// </summary>
    /// <param name="filetype"></param>
    /// <param name="filename"></param>
    /// <returns>�����Ƿ���ȷ</returns>
    private static  bool checkFileType(string filetype, string filename)
    {    
        //����ļ�������
        string extn = filename.Substring(filename.LastIndexOf("."));

        if (filetype.IndexOf(extn) != -1)
            return true;
        return false; 
    }

    /// <summary>
    /// ����ļ��Ƿ񳬹�����С
    /// </summary>
    /// <param name="maxfilesize">�ļ����ֵ</param>
    /// <param name="file">�ļ�����</param>
    /// <returns>�������</returns>
    private static  bool checekIsAllowSize(int maxfilesize, HttpPostedFile file)
    {
        return (file.ContentLength / 1000 <= maxfilesize); 
    }
     

}
