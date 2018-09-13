using System;
using System.Linq;
using AccountantWords.Tools;
using AccountantWords.Entities;

namespace AccountantWords
{
    class Program
    {
        static void Main(string[] args)
        {
            Executa();
        }

        private static void Executa()
        {
            var url = System.Configuration.ConfigurationSettings.AppSettings["URL_MAPPING"].ToString();
            var xml = ContentDownload.GetXml(url);
            RSS channel = XMLSerializer.Deserialize<RSS>(xml);
            try
            {
                CheckVerifiable.GetPhrasesByToic(channel.Channel.Item.OrderByDescending(o => o.PubDate).ToList<object>());
            }
            catch (Exception e)
            {
                Console.WriteLine("error occurred {0}", e.Message);
            }
            Console.ReadKey();
        }



    }
}
