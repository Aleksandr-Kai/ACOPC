using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using System.Data;
using System.ComponentModel;

namespace ACOPC
{
    class ConfigRW
    {
        private static string m_ConfigPath;

        public static void SetPath(string _Path)
        {
            m_ConfigPath = _Path;
        }

        public static T Read<T>(string _Section, string _Key)
        {
            var parser = new FileIniDataParser();
            try
            {
                IniData data = parser.ReadFile(m_ConfigPath);
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T)converter.ConvertFromString(data[_Section][_Key]);
                }
            }
            catch (Exception) { }
            
            return default(T);
        }

        public static T Read<T>(string _Section, string _Key, T _default)
        {
            var parser = new FileIniDataParser();
            try
            {
                IniData data = parser.ReadFile(m_ConfigPath);
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T)converter.ConvertFromString(data[_Section][_Key]);
                }
            }
            catch (Exception) { }

            return _default;
        }

        public static void Write<T>(string _Section, string _Key, T _Value)
        {
            try
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(m_ConfigPath);
                var converter = TypeDescriptor.GetConverter(typeof(T));
                data[_Section][_Key] = converter.ConvertToString(_Value);
                parser.WriteFile(m_ConfigPath, data);
            }
            catch (Exception) { }
        }
    }
}
