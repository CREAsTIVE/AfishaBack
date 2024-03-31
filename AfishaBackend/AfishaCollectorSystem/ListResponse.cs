using SimpleHTTPServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AfishaBackend.AfishaCollectorSystem
{
	public class ListResponse : JsonResponse<List<Afisha>>
	{
		public ListResponse(Collector collector) { this.collector = collector; }
		Collector collector;
		public override List<Afisha> getResponse(HttpListenerRequest request)
		{
			return collector.Get().ToList();
		}
	}

	public class SingleResponse : JsonResponse<Afisha>
	{
		Collector collector;
		public SingleResponse(Collector collector)
		{
			this.collector = collector;
		}

		public override Afisha getResponse(HttpListenerRequest request)
		{
			var queryDictionary = System.Web.HttpUtility.ParseQueryString(request.Url?.Query ?? "");
			return collector.Get(queryDictionary["id"]?.Replace("/", "") ?? "");
		}
	}
}
