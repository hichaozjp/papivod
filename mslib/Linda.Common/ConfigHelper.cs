using System;
using System.Configuration;

/// <summary>
/// web.config������
/// </summary>
public sealed class ConfigHelper
{
    /// <summary>
    /// �õ�AppSettings�е������ַ�����Ϣ
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetConfigString(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }

    /// <summary>
    /// �õ�AppSettings�е������ַ�����Ϣ
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetConnectionStrings(string key)
    {
        return ConfigurationManager.ConnectionStrings[key].ConnectionString;
    }

    /// <summary>
    /// �õ�AppSettings�е�����bool��Ϣ
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool GetConfigBool(string key)
    {
        bool result = false;
        string cfgVal = GetConfigString(key);
        if (null != cfgVal && string.Empty != cfgVal)
        {
            try
            {
                result = bool.Parse(cfgVal);
            }
            catch (FormatException)
            {
                // Ignore format exceptions.
            }
        }

        return result;
    }
    /// <summary>
    /// �õ�AppSettings�е�����decimal��Ϣ
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static decimal GetConfigDecimal(string key)
    {
        decimal result = 0;
        string cfgVal = GetConfigString(key);
        if (null != cfgVal && string.Empty != cfgVal)
        {
            try
            {
                result = decimal.Parse(cfgVal);
            }
            catch (FormatException)
            {
                // Ignore format exceptions.
            }
        }

        return result;
    }
    /// <summary>
    /// �õ�AppSettings�е�����int��Ϣ
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static int GetConfigInt(string key)
    {
        int result = 0;
        string cfgVal = GetConfigString(key);
        if (null != cfgVal && string.Empty != cfgVal)
        {
            try
            {
                result = int.Parse(cfgVal);
            }
            catch (FormatException)
            {
                // Ignore format exceptions.
            }
        }

        return result;
    }
}
