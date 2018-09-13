using System;
using System.Xml.Serialization;
using AccountantWords.Extensions;

namespace AccountantWords.Entities
{
    public class Channel
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        //[XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        //public string Link { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "lastBuildDate")]
        public string _LastBuildDate { get; set; }

        //TODO: VERIFICAR
        public DateTime LastBuildDate { get { return _LastBuildDate.StringToDateTime(); } }

        [XmlElement(ElementName = "language")]
        public string Language { get; set; }

        [XmlElement(ElementName = "updatePeriod", Namespace = "http://purl.org/rss/1.0/modules/syndication/")]
        public string UpdatePeriod { get; set; }

        [XmlElement(ElementName = "updateFrequency", Namespace = "http://purl.org/rss/1.0/modules/syndication/")]
        public string UpdateFrequency { get; set; }

        [XmlElement(ElementName = "generator")]
        public string Generator { get; set; }

        [XmlElement(ElementName = "item")]
        public Topic[] Item { get; set; }
    }
}
