using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockInfo
{
    class StockInfo
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter Symbol: ");
                await getInfo(Console.ReadLine());
            }
        }

        static async Task getInfo(string Symbol)
        {
            HttpResponseMessage r = await client.GetAsync($"https://finance.yahoo.com/quote/{Symbol}");
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(await r.Content.ReadAsStringAsync());
            var CurrencyRef = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[2]/div[1]/div[2]/span").InnerText;
            var Currency = new String(CurrencyRef.Skip(CurrencyRef.Length - 3).Take(3).ToArray());

            Console.WriteLine();
            Console.Write(htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[2]/div[1]/div[1]/h1").InnerText); // NAME
            Console.WriteLine(" " + CurrencyRef.Split("-")[0]); // MARKET
            Console.WriteLine();
            Console.Write("Price: "+htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"quote-header-info\"]/div[3]/div[1]/div/span[1]").InnerText + " "+ Currency); // PRICE
            Console.WriteLine("  " + htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[3]/div[1]/div/span[2]").InnerText); // PERCENTAGE CHANGE
            Console.WriteLine("Range: "+htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div[1]/table/tbody/tr[5]/td[2]").InnerText+ " " + Currency); // TODAYS RANGE
            Console.WriteLine("Open: " + htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div[1]/table/tbody/tr[2]/td[2]/span").InnerText + " " + Currency); // OPEN
            Console.WriteLine("Previous Close: " + htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div[1]/table/tbody/tr[1]/td[2]/span").InnerText + " " + Currency);

            Console.WriteLine();
        }
    }
}
