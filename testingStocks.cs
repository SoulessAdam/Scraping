using System;
using System.Net;

public string Symbol = "LLOY.L";
//public string Market = "LSE";

webRequest webRequest1 = new webRequest($"finance.yahoo.com/quote/{Symbol}")
