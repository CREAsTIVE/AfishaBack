using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AfishaBackend.AfishaCollectorSystem
{
	internal class PermCassaAfishaGetter : AfishaGetter
	{
		static Regex regex = new(@" <div[^>]*> <a[^>]*> <img src=""([^""]*)""[^>]*> <[^>]*> <[^>]*> <[^>]*> <[^>]*> <[^>]*>\d+.<[^>]*> <[^>]*> <[^>]*> <[^>]*>([^<]*)<[^>]*> <[^>]*> <[^>]*> <[^>]*>([^<]*)<[^>]*>,\s*<[^>]*>[^<]*<[^>]*>,\s*([^<]*) <[^>]*>([^<]*)<[^>]*> .*\s*<[^>]*> <[^>]*>([^<]*)<[^>]*> <[^>]*> <[^>]*>[^<]*<[^>]*>[^<]*<[^>]*><[^>]*>\s*<[^>]*>[^<]*<[^>]*> <[^>]*> <[^>]*>");
		public List<Afisha> Get()
		{
			HttpClient httpClient = new HttpClient();

			var content = httpClient.GetAsync("https://perm.kassy.ru/events/koncerty-i-shou/").Result.Content.ReadAsStringAsync().Result;
			HtmlDocument document = new();
			document.LoadHtml(content);

			var listNode = document.DocumentNode.SelectSingleNode("//*[@id=\"page\"]/ul[2]");

			List<Afisha> afishas = new List<Afisha>();

			foreach ( var item in listNode.ChildNodes )
			{
				if (item is HtmlTextNode) continue;

				var m = regex.Match(item.InnerHtml);
				if (!m.Success)
					continue;

				var imageUrl = $"https://perm.kassy.ru{m.Groups[1]}".Replace("_thumb", "");
				var label = m.Groups[2].Value;
				var location = m.Groups[3].Value;
				var date = $"{m.Groups[4]}, {m.Groups[5]}";
				var description = m.Groups[6].Value;	

				afishas.Add(new Afisha()
				{
					Id=Random.Shared.Next(int.MinValue, int.MaxValue).ToString(),
					Name=label,
					ImageUrl=imageUrl,
					Description=description,
					Location = location,
					Date = date,
				});
			}

			return afishas;
		}
	}
}
