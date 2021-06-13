using System;
using System.Net;


namespace ImgurRandom
{
    class Program
    {
      public string Symbol = "LLOY.L";
      //public string Market = "LSE";

      static Task Main(){
        WebRequest webRequest1 = WebRequest.Create($"https://finance.yahoo.com/quote/{Symbol}");
        WebResponse response = webRequest1.GetResponse();
        Console.Write(response);}
    }
 }
