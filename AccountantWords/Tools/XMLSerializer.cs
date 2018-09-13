using System.IO;
using System.Xml.Serialization;

namespace AccountantWords.Tools
{
    public class XMLSerializer
    {
        public static T Deserialize<T>(string xmlText)
        {
            if (string.IsNullOrWhiteSpace(xmlText)) return default(T);
            using (StringReader stringReader = new StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }
}
