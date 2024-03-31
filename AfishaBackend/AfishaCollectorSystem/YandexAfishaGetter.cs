using AfishaBackend.Databases;
using AfishaBackend.RuntimeDatabase;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using SimpleHTTPServer.HTTPServerLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AfishaBackend.AfishaCollectorSystem
{
	
	public class YandexAfishaGetter : AfishaGetter
	{
		public ILogger? logger;

		public YandexAfishaGetter(ILogger? logger)
		{
			this.logger = logger;
		}

		public static string Request(HttpClient httpClient)
		{
			HttpRequestMessage request = new()
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://afisha.yandex.ru/perm/selections/cinema")
			};
			request.Headers.Accept.Clear();
			request.Headers.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
		
			request.Headers.AcceptEncoding.Clear();
			//request.Headers.AcceptEncoding.ParseAdd("gzip, deflate, br, zstd");

			//request.Headers.AcceptLanguage.Clear();
			//request.Headers.AcceptLanguage.ParseAdd("ru,en;q=0.9,en-GB;q=0.8,en-US;q=0.7");

			request.Headers.CacheControl = new();
			request.Headers.CacheControl.MaxAge = TimeSpan.Zero;

			request.Headers.Connection.Clear();
			request.Headers.Connection.ParseAdd("keep-alive");

			request.Headers.Add("Cookie", "gdpr=0; _ym_uid=1692121420481280371; AMP_332f8b000f=JTdCJTIyb3B0T3V0JTIyJTNBZmFsc2UlMkMlMjJkZXZpY2VJZCUyMiUzQSUyMjBmOWU5MzhjLWJjNWItNDIzNy04OTMxLThjMzliZTE4ZjAzMiUyMiUyQyUyMmxhc3RFdmVudFRpbWUlMjIlM0ExNjkyODYzNTA5MTczJTJDJTIyc2Vzc2lvbklkJTIyJTNBMTY5Mjg2MzUwOTA4OSUyQyUyMnVzZXJJZCUyMiUzQSUyMnVuZGVmaW5lZCUyMiU3RA==; is_gdpr=0; yandexuid=3964351981680539119; yuidss=3964351981680539119; ymex=1696800666.oyu.1785960561691743662#2007103662.yrts.1691743662#2007103662.yrtsi.1691743662; skid=1020372211694708247; yashr=1081329181696333003; amcuid=6380949051704613745; is_gdpr_b=CNabARCN5AEoAg==; yp=1705314785.szm.1%3A1920x1080%3A1865x979#2020070039.pcs.0#1736246039.nsnpprm.0#2020070039.nsnp.0#1736246039.p_sw.1704710039#1707388440.hdrc.0; _ym_d=1707938934; receive-cookie-deprecation=1; ys=c_chck.3744158932; afisha.sid=s%3AkUVDcXQjsfETkGA73FnAqw7Ad18_Z8yv.TwVg%2Brl%2BZBPp673RGF4jkzm63Fbe5UAT948RBiPhGj4; _ym_isad=2; i=qyPzTfE8cHlYyTUhPd3DVwEadlC3Ibbt3qs9pDA7ApwfhtniJoFGtjvMbmWFErVsESgLqkYPIcPLgZGbNUBgLqQyRMc=; bltsr=1; KIykI=1; coockoos=1; yabs-vdrf=A0; bh=Ej8iTWljcm9zb2Z0IEVkZ2UiO3Y9IjEyMyIsIk5vdDpBLUJyYW5kIjt2PSI4IiwiQ2hyb21pdW0iO3Y9IjEyMyIaBSJ4ODYiIg8iMTIzLjAuMjQyMC42NSIqAj8wOgkiV2luZG93cyJCCCIxMC4wLjAiSgQiNjQiUloiTWljcm9zb2Z0IEVkZ2UiO3Y9IjEyMy4wLjI0MjAuNjUiLCJOb3Q6QS1CcmFuZCI7dj0iOC4wLjAuMCIsIkNocm9taXVtIjt2PSIxMjMuMC42MzEyLjg3IiI=; spravka=dD0xNzExODIxNDE3O2k9NzcuNDMuMjE4LjExNjtEPUI1QUY3Qjk3NTRDOTQyOTQ2RDQ5MzE1QjkyMDJGQTBCRDk4RjAwMjdEMjJCNDU3MjA5RDlBOEQ1N0NBNUFGNDVDNDlEQjM3QzJDNTgxMTkxM0JERkUyQTM5QkNGMjk0NkQwMzQ2OUI0MzQxRDYxRjgzREY1NUM2MDg5QjI5NTZFNTk2MTRFOEMyOUI2MDU4NDA2MkY5MjM2NzE7dT0xNzExODIxNDE3OTYzNTgwMTAyO2g9M2NiYjNhMjc3YzViYzMzZWJmYmE3Njg0OTgzNjQ4MTU=; _ym_visorc=b; _yasc=u8wQG1hVOfR4A0dS0dmIm7MoFbATQv1OiyyDqAVjy7B6lATCY8/ylo5C1malxZAec72DsD3IsBqz");

			request.Headers.Host = "afisha.yandex.ru";
				
			request.Headers.Add("Sec-Fetch-Dest", "document");
			request.Headers.Add("Sec-Fetch-Mode", "navigate");
			request.Headers.Add("Sec-Fetch-Site", "same-origin");
			request.Headers.Add("Sec-Fetch-User", "?1");
			request.Headers.Add("Upgrade-Insecure-Requests", "1");

			request.Headers.UserAgent.Clear();
			request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36 Edg/123.0.0.0");

			request.Headers.Add("Sec-Ch-Ua", "\"Microsoft Edge\";v=\"123\", \"Not:A-Brand\";v=\"8\",\"Chromium\";v=\"123\"");
			request.Headers.Add("sec-ch-ua-mobile", "?0");
			request.Headers.Add("Sec-Ch-Ua-Platform", "\"Windows\"");

				

			var res = httpClient.Send(request).Content.ReadAsStringAsync().Result;
			return res;
			
		}

		public List<Afisha> Get()
		{
			var httpClient = new HttpClient();

			var res = Request(httpClient);

			HtmlDocument document = new();
			document.LoadHtml(res);

			string inner;

			try
			{
				inner = document.DocumentNode.SelectSingleNode("/html/body/script[10]").InnerHtml;
			} 
			catch (NullReferenceException ex)
			{
				return new();
			}
			var data = JArray.Parse(inner);

			var afishes = new List<Afisha>();

			foreach (var current in data)
			{
				var afisha = new Afisha()
				{
					Id = new Uri(((string?)current["url"]) ?? throw new()).Segments.LastOrDefault(),
					Name = ((string?)current["name"]),
					Date = ((string?)current["startDate"]),
					Description = ((string?)current["description"]),
					ImageUrl = ((string?)current["image"]),
					Rating = (float?)current["aggregateRating"]?["ratingValue"],
					RatingAmount = (int?)current["aggregateRating"]?["ratingCount"],
					TicketPrice = ((int?)current["offers"]?["price"]),
				};
				afishes.Add(afisha);
			}

			return afishes;

		}

		public Afisha? Get(string index)
		{
			throw new NotImplementedException();
		}
	}
}
