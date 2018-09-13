using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using AccountantWords.Attribute;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AccountantWords.Tools
{
    public class CheckVerifiable
    {
        public static Dictionary<object, IList<string>> GetPhrasesByToic(IList<object> obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Topic list not sent");
            var TopicPhrases = new Dictionary<object, IList<string>>();
            ((List<object>)obj).ForEach(o => TopicPhrases.Add(o, GetTopicPhrases(o)));
            return TopicPhrases;
        }

        private static IList<string> GetTopicPhrases(object obj)
        {
            if (obj == null) return null;
            List<string> phrases = new List<string>();
            var elems1 = obj as IList;
            Type objType = obj.GetType();
            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    var Verifiable = IsVerifiedPProperty(property);
                    object propValue = property.GetValue(obj, null);
                    var elems = propValue as IList;
                    if (elems != null)
                    {
                        foreach (var item in elems)
                        {
                            if (item is string)
                                if (Verifiable)
                                    phrases.Add(RemoveNoText(item.ToString()));
                                else
                                {
                                    var ret = GetTopicPhrases(item);
                                    if ((Verifiable) && (ret.Count > 0))
                                        phrases.AddRange(GetTopicPhrases(item));
                                }
                        }
                    }
                    else
                    {
                        if (property.PropertyType.Assembly == objType.Assembly)
                        {
                            var ret = GetTopicPhrases(propValue);
                            if ((Verifiable) && (ret.Count > 0))
                                phrases.AddRange(ret);
                        }
                        else
                            if (Verifiable)
                            phrases.Add(RemoveNoText(propValue.ToString()));
                    }
                }
                catch { }
            }
            return phrases;
        }

        private static bool IsVerifiedPProperty(PropertyInfo property)
        {
            return (property.GetCustomAttributes(true).Where(a => a.GetType() == typeof(Verifiable)).FirstOrDefault() != null);
        }

        private static string RemoveNoText(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text to replace not sent");
            string input = text;
            string pattern = @"<(.*?)>";
            string replacement = string.Empty;
            string result = Regex.Replace(input, pattern, replacement);
            return result;
        }
    }
}
