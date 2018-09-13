using System.Collections.Generic;
using System.Linq;

namespace AccountantWords.Extensions
{
    public static class JoinCollection
    {
        public static string[] Join (this string[] value, string[] co)
        {
            var _lista = new List<string>(value);
            co.ToList().ForEach(i => _lista.Add(i));
            return _lista.ToArray();
        }
    }
}
