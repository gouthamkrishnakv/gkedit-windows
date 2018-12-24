using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WPFNotepad.Methods
{
    class MySerializer
    {
        public static String Serialize<T>(T t)
        {
            using (StringWriter sw = new StringWriter())
            using (XmlWriter xw = XmlWriter.Create(sw))
            {
                new XmlSerializer(typeof(T)).Serialize(xw, t);
                return sw.GetStringBuilder().ToString();
            }
        }

        public static T Deserialize<T>(String s_xml)
        {
            using (XmlReader xw = XmlReader.Create(new StringReader(s_xml)))
                return (T)new XmlSerializer(typeof(T)).Deserialize(xw);
        }
    }
}
