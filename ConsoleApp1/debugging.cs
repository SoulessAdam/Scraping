using System;
using System.Net.Http;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Linq;

namespace ConsoleApp1
{
    class debugging
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            string Symbol = "TSLA";
            HttpResponseMessage r = await client.GetAsync($"https://finance.yahoo.com/quote/{Symbol}");
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(await r.Content.ReadAsStringAsync());
            Console.WriteLine(htmlDocument.DocumentNode.SelectNodes("//*[@id=\"quote-header-info\"]/div[3]/div[1]/div/span[1]").First().InnerText);
        }
    }
}
