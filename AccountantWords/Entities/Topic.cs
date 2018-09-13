using System;
using System.Xml.Serialization;
using AccountantWords.Attribute;
using AccountantWords.Extensions;

namespace AccountantWords.Entities
{
    public class Topic
    {
        [Verifiable]
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "comments")]
        public string Comments { get; set; }

        [XmlElement(ElementName = "pubDate")]
        public string _PubDate { get; set; }

        //TODO: VERIFICAR
        public DateTime PubDate { get { return _PubDate.StringToDateTime(); } }

        [Verifiable]
        [XmlElement(ElementName = "creator", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string[] Creator { get; set; }

        [Verifiable]
        [XmlElement(ElementName = "category")]
        public string[] Category { get; set; }

        [XmlElement(ElementName = "guid")]
        public string Guid { get; set; }

        [Verifiable]
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "encoded", Namespace = "http://purl.org/rss/1.0/modules/content/")]
        public string Encoded { get; set; }

        [XmlElement(ElementName = "commentRss", Namespace = "http://wellformedweb.org/CommentAPI/")]
        public string CommentRss { get; set; }

        //[XmlElement(ElementName = "slash:comments")]
        //public string Comments { get; set; }

    }
}
