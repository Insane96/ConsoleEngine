using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleEngine
{
    /// <summary>
    /// Helper Class used to save / load data from file
    /// </summary>
    public static class XML
    {
        /// <summary>
        /// Writes an object into the fileName into the given path
        /// </summary>
        /// <param name="uri">String rappresenting the full path to the file</param>
        public static void Serialize(Type type, object obj, string uri)
        {

            XmlSerializer serializer = new XmlSerializer(type);
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.IndentChars = "\t";
            XmlWriter xmlWriter = XmlWriter.Create(uri, xmlWriterSettings);
            serializer.Serialize(xmlWriter, obj);
            xmlWriter.Close();
        }

        /// <summary>
        /// Reads an object from the fileName into the given path
        /// </summary>
        /// <param name="uri">String rappresenting the full path to the file</param>
        public static object Deserialize(Type type, string uri)
        {
            XmlSerializer deserializer = new XmlSerializer(type);
            var obj = Activator.CreateInstance(type);
            if (File.Exists(uri))
            {
                XmlReader xmlReader = XmlReader.Create(uri);
                obj = deserializer.Deserialize(xmlReader);
                xmlReader.Close();
                return obj;
            }
            else
                return obj;

        }
    }
}
