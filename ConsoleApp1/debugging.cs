using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace StockInfo
{
    class StockInfo
    {
        static readonly HttpClient client = new HttpClient();

        static Dictionary<string, Dictionary<string, string>> Stocks = new Dictionary<string, Dictionary<string, string>>
            {
                { "Stock1", new Dictionary<string,string>() },
                { "Stock2", new Dictionary<string, string>() },
                { "Stock3", new Dictionary<string, string>() },
                { "Stock4", new Dictionary<string, string>() },
                { "Stock5", new Dictionary<string, string>() },
            };
        static async Task Main(string[] args)
        {
            // USE THIS AS A LIB FOR SHIT!!
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
            Console.Write("Price: " + htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"quote-header-info\"]/div[3]/div[1]/div/span[1]").InnerText + " " + Currency); // PRICE
            Console.WriteLine("  " + htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[3]/div[1]/div/span[2]").InnerText); // PERCENTAGE CHANGE
            Console.WriteLine("Range: " + htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div[1]/table/tbody/tr[5]/td[2]").InnerText + " " + Currency); // TODAYS RANGE
            Console.WriteLine("Open: " + htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div[1]/table/tbody/tr[2]/td[2]/span").InnerText + " " + Currency); // OPEN
            Console.WriteLine("Previous Close: " + htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div[1]/table/tbody/tr[1]/td[2]/span").InnerText + " " + Currency);

            Console.WriteLine();
        }

        public static async Task LookupSymbols(string name = "Bitcoin")
        {
            HttpResponseMessage r = await client.GetAsync($"https://finance.yahoo.com/lookup?s={name}");
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(await r.Content.ReadAsStringAsync());
            var SymbolAmount = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/section/ul/li[1]/a/span").InnerText.Split('(')[1].Replace("(", "").Replace(")", "");
            Console.WriteLine();
            Console.WriteLine(SymbolAmount + " Tickers Found");
            switch (int.Parse(SymbolAmount))
            {
                case 0:
                    Console.WriteLine("No Symbols Found. Make sure your spelling is correct!");
                    break;
                case 1:
                    AddOne();
                    await PrintList(1);
                    break;
                case 2:
                    AddTwo();
                    await PrintList(2);
                    break;
                case 3:
                    AddThree();
                    await PrintList(3);
                    break;
                case 4:
                    AddFour();
                    await PrintList(4);
                    break;
                case 5:
                    AddFive();
                    await PrintList(5);
                    break;
                default:
                    AddFive();
                    await PrintList(5);
                    break;
            }

            void AddOne()
            {
                Stocks["Stock1"].Add("Name", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div[1]/table/tbody/tr[1]/td[2]").InnerText);
                Stocks["Stock1"].Add("Symbol", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div[1]/table/tbody/tr[1]/td[1]/a").InnerText);
                Stocks["Stock1"].Add("Exchange", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div[1]/table/tbody/tr[1]/td[6]").InnerText);
            };
            void AddTwo()
            {
                AddOne();
                Stocks["Stock2"].Add("Name", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div/table/tbody/tr[2]/td[2]").InnerText);
                Stocks["Stock2"].Add("Symbol", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div/table/tbody/tr[2]/td[1]/a").InnerText);// NAME 
                Stocks["Stock2"].Add("Exchange", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div/table/tbody/tr[2]/td[6]").InnerText);// EXCHANGE 
            };
            void AddThree() {
                AddTwo();
                Stocks["Stock3"].Add("Name", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div/table/tbody/tr[3]/td[2]").InnerText);
                Stocks["Stock3"].Add("Symbol", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div[1]/table/tbody/tr[3]/td[1]/a").InnerText);
                Stocks["Stock3"].Add("Exchange", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div/table/tbody/tr[3]/td[6]").InnerText);

            };
            void AddFour() {
                AddThree();
                Stocks["Stock4"].Add("Name", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div[1]/table/tbody/tr[4]/td[2]").InnerText);
                Stocks["Stock4"].Add("Symbol", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div/table/tbody/tr[4]/td[1]/a").InnerText);
                Stocks["Stock4"].Add("Exchange", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div/table/tbody/tr[4]/td[6]").InnerText);
            };
            void AddFive() {
                AddFour();
                Stocks["Stock5"].Add("Name", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div/table/tbody/tr[5]/td[2]").InnerText);
                Stocks["Stock5"].Add("Symbol", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div[1]/table/tbody/tr[6]/td[1]/a").InnerText);
                Stocks["Stock5"].Add("Exchange", htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/section/section/div/div/div/div/table/tbody/tr[5]/td[6]").InnerText);
            };

        }
        static async Task PrintList(int Entries)
        {
            for (var i = 1; i < Entries+1; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Name: "+Stocks[$"Stock{i}"]["Name"]);
                Console.WriteLine("Symbol: " + Stocks[$"Stock{i}"]["Symbol"]);
                Console.WriteLine("Exchange: " + Stocks[$"Stock{i}"]["Exchange"]);
                Console.WriteLine();
                
            }
            await ClearLists();
        }
        
        static async Task ClearLists()
        {
            Stocks["Stock1"].Clear();
            Stocks["Stock2"].Clear();
            Stocks["Stock3"].Clear();
            Stocks["Stock4"].Clear();
            Stocks["Stock5"].Clear();
        }
    }
}
