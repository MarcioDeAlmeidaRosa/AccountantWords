using System;
using System.Linq;
using AccountantWords.Tools;
using AccountantWords.Entities;
using AccountantWords.Extensions;

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
            var separador = System.Configuration.ConfigurationSettings.AppSettings["SEPARATOR"].ToCharArray();
            var articlesList = System.Configuration.ConfigurationSettings.AppSettings["ARTICLES_LIST"].ToString().Split(separador);
            var prepositionList = System.Configuration.ConfigurationSettings.AppSettings["PREPOSITIONS_LIST"].ToString().Split(separador);
            articlesList = articlesList.Join(prepositionList);
            var xml = ContentDownload.GetXml(url);
            RSS channel = XMLSerializer.Deserialize<RSS>(xml);
            try
            {
                var retorno = CheckVerifiable.GetPhrasesByToic(channel.Channel.Item.OrderByDescending(o => o.PubDate).ToList<object>(), articlesList);
                foreach(var item in retorno)
                {
                    Console.WriteLine("###############{0}###############", item);
                    foreach(var pala in item.Value)
                    {
                        Console.WriteLine(pala);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error occurred {0}", e.Message);
            }
            Console.ReadKey();
        }



    }
}
