using System;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string Symbol = "LLOY.L";
            WebRequest webRequest1 = WebRequest.Create($"https://finance.yahoo.com/quote/{Symbol}");
            WebResponse response = webRequest1.GetResponse();
            Console.WriteLine(response);
        }
    }
}
