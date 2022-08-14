using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TOURISM.App.Infrastructure.Utils
{
    public static class XmlHelper
    {
        public static string SerializeObject(Type type, object obj)
        {
            var soapserializer = new XmlSerializer(type);
            using (StringWriter writer = new Utf8StringWriter())
            {
                soapserializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public static T DeserializeObject<T>(string xml)
        {
            var document = XDocument.Parse(xml);
            T deserializedObject;
            using (var reader = document.CreateReader(ReaderOptions.None))
            {
                var soapserializer = new XmlSerializer(typeof(T));
                deserializedObject = (T)soapserializer.Deserialize(reader);
            }

            return deserializedObject;
        }
    }
}
