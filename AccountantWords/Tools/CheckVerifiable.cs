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
        public static Dictionary<object, IList<string>> GetPhrasesByToic(IList<object> obj, string[] disregardWords)
        {
            if (obj == null)
                throw new ArgumentNullException("Topic list not sent");
            var TopicPhrases = new Dictionary<object, IList<string>>();
            foreach(var ob in obj)
            {
                var phrases = GetTopicPhrases(ob);
                phrases = DisregardWords(phrases, disregardWords);
                TopicPhrases.Add(ob, phrases);
            }
            return TopicPhrases;
        }

        private static string[] GetTopicPhrases(object obj)
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
                                    phrases.Add(RemoveHtmlNotation(item.ToString()));
                                else
                                {
                                    var ret = GetTopicPhrases(item);
                                    if ((Verifiable) && (ret.Length > 0))
                                        phrases.AddRange(GetTopicPhrases(item));
                                }
                        }
                    }
                    else
                    {
                        if (property.PropertyType.Assembly == objType.Assembly)
                        {
                            var ret = GetTopicPhrases(propValue);
                            if ((Verifiable) && (ret.Length > 0))
                                phrases.AddRange(ret);
                        }
                        else
                            if (Verifiable)
                            phrases.Add(RemoveHtmlNotation(propValue.ToString()));
                    }
                }
                catch { }
            }
            return phrases.ToArray();
        }

        private static bool IsVerifiedPProperty(PropertyInfo property)
        {
            return (property.GetCustomAttributes(true).Where(a => a.GetType() == typeof(Verifiable)).FirstOrDefault() != null);
        }

        private static string RemoveHtmlNotation(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text to replace not sent");
            string input = text;
            string pattern = @"<(.*?)>";
            string replacement = string.Empty;
            string result = Regex.Replace(input, pattern, replacement);
            return result;
        }

        private static string[] DisregardWords(string[] phrase, string[] disregardWords)
        {
            List<string> _words = new List<string>();
            phrase.ToList().ForEach(p => _words.AddRange(p.Split(' ')));
            disregardWords.ToList().ForEach(d => {
                var regex = new Regex(d, RegexOptions.IgnoreCase);
                var listaRemover = _words.FindAll(pala => pala.ToUpper().Equals(d));
                listaRemover.ToList().ForEach(r => _words.Remove(r));
            });
            return _words.ToArray();
        }
    }
}
