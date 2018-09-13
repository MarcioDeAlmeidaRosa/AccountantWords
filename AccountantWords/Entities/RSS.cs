using System;
using System.Xml.Serialization;

namespace AccountantWords.Entities
{
    [Serializable, XmlRoot("rss")]
    public class RSS
    {
        [XmlElement(ElementName = "channel")]
        public Channel Channel { get; set; }
    }
}
