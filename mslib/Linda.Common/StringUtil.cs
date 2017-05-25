using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

/// <summary>
/// �ַ�������
/// </summary>
public class StringUtil
{
    public StringUtil()
    {
        //
        // TODO: �ڴ˴����ӹ��캯���߼�
        //
    }

    /// <summary>
    /// ����DBNULL��ֵ�ķ�������
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static  string GetDBNULLValue(object obj)
    {
        if (obj == System.DBNull.Value)
            return "";
        else
        {
            if(string.IsNullOrEmpty(obj.ToString()))
                return "";
            else
                return obj.ToString();
        }
    }
    

    /// <summary>
    /// �Ƿ�Ϊnull
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    public static bool IfNull(string strText)
    {
        bool b = false;
        if (strText == null || strText == "")
        {
            b = true;
        }
        else
        {
            b = false;
        }
        return b;
    }

     
    /// <summary>
    /// �ַ�����ת�� ���ڲ�ѯ ��½ʱ ��ֹ����ĵ�ȡ����
    /// </summary>
    /// <param name="strtb"></param>
    /// <returns></returns>
    public static string TBCode(string strtb)
    {
        strtb = strtb.Trim();
        strtb = strtb.Replace("!", "");
        strtb = strtb.Replace("@", "");
        strtb = strtb.Replace("#", "");
        strtb = strtb.Replace("$", "");
        strtb = strtb.Replace("%", "");
        strtb = strtb.Replace("^", "");
        strtb = strtb.Replace("&", "");
        strtb = strtb.Replace("*", "");
        strtb = strtb.Replace("(", "");
        strtb = strtb.Replace(")", "");
        strtb = strtb.Replace("_", "");
        strtb = strtb.Replace("+", "");

        strtb = strtb.Replace("?", "");

        strtb = strtb.Replace(".", "");
        strtb = strtb.Replace(">", "");
        strtb = strtb.Replace("<", "");
        strtb = strtb.Replace("{", "");
        strtb = strtb.Replace("}", "");
        strtb = strtb.Replace("[", "");
        strtb = strtb.Replace("]", "");
        strtb = strtb.Replace("-", "");
        strtb = strtb.Replace("=", "");
        strtb = strtb.Replace(",", "");

        strtb = strtb.Replace("\r", "<br>");
        strtb = strtb.Replace("\r\n", "<br>");
        strtb = strtb.Replace("'", "");
        return strtb;
    }

    /// <summary>
    /// ��ȡ��������
    /// </summary>
    /// <param name="content"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    static public string GetSubstr(string content, int nlength)
    {
        if (content.Length > nlength)
            return content.Substring(0, nlength - 2) + "...";
        else
            return content;

    }

    /// <summary>
    /// �����ı��༭���滻����ַ���
    /// </summary>
    /// <param name="str">Ҫ�滻���ַ���</param>
    /// <returns></returns>
    static public string GetHtmlEditReplace(string str)
    {
        #region
        return str.Replace("'", "''").Replace("&nbsp;", " ").Replace(",", "��").Replace("%", "��").Replace("script", "").Replace(".js", "");
        #endregion
    }
      
    /// <summary>
    /// �÷��������ж��Ƿ�Ϊ����
    /// </summary>
    public static bool IsNumeric(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
    }
    /// <summary>
    /// �÷��������ж��Ƿ�Ϊ����
    /// </summary>
    public static bool IsInt(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*$");
    }
    /// <summary>
    /// �÷��������ж��Ƿ�Ϊ�޷�������
    /// </summary>
    public static bool IsUnsign(string value)
    {
        return Regex.IsMatch(value, @"^\d*[.]?\d*$");
    }

    /// <summary> 
    /// ȥ��HTML���
    /// </summary>
    /// <param name="NoHTML">����HTML��Դ�� </param>
    /// <returns>�Ѿ�ȥ���������</returns>
    public static string NoHTML(string Htmlstring)
    {
        //ɾ���ű�
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //ɾ��HTML
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        Htmlstring.Replace("\r\n", "");
        Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
        return Htmlstring;
    }

    /// <summary>
    /// ��|�Ų���ַ�����һ������
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] ret_Arry(string str)
    {
        string[] Dic = str.Split('|');
        return Dic;
    }
    /// <summary>
    /// ��,�Ų���ַ�����һ������
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] ret_String(string str)
    {
        string[] Dic = str.Split(',');
        return Dic;
    }



    /// <summary>
    /// ����ָ�����ȵ��ַ���,������strLong��str�ַ���
    /// </summary>
    /// <param name="strLong">���ɵĳ���</param>
    /// <param name="str">��str�����ַ���</param>
    /// <returns></returns>
    public static string StringOfChar(int strLong, string str)
    {
        string ReturnStr = "";
        for (int i = 0; i < strLong; i++)
        {
            ReturnStr += str;
        }

        return ReturnStr;
    }

    /// <summary>
    /// �������������  5λ������
    /// </summary>
    /// <returns></returns>
    public static string GetRamCode()
    {
        #region
        return DateTime.Now.ToString("yyyyMMddHHmmssfffff");
        #endregion
    }
}
