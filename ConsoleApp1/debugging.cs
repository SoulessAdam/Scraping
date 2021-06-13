using System;
using System.Net.Http;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Linq;

namespace StockInfo
{
    class StockInfo
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
           while(true){
                Console.Write("Enter Symbol: ");
                await getInfo(Console.ReadLine());
            }
        }

        static async Task getInfo(string Symbol)
        {
            HttpResponseMessage r = await client.GetAsync($"https://finance.yahoo.com/quote/{Symbol}");
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(await r.Content.ReadAsStringAsync());
            var Currency = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[2]/div[1]/div[2]/span").InnerText;

            Console.Write(htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[2]/div[1]/div[1]/h1").InnerText); // NAME
            Console.WriteLine(" "+Currency.Split("-")[0]); // MARKET

            Console.Write(htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"quote-header-info\"]/div[3]/div[1]/div/span[1]").InnerText); // PRICE
            Console.Write(" "+new string(Currency.Skip(Currency.Length - 3).Take(3).ToArray())); // CURRENCY
            Console.WriteLine("  "+htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[3]/div[1]/div/span[2]").InnerText); // PERCENTAGE CHANGE

            Console.WriteLine(htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div[1]/table/tbody/tr[5]/td[2]").InnerText); // TODAYS RANGE

            Console.WriteLine();
        }
    }
}
