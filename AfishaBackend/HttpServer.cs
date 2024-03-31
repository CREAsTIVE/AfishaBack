using SimpleHTTPServer.HTTPServerLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHTTPServer
{
	public class HttpServer
	{
		HttpListener listener;
		int port;
		bool enabled = false;
		public ILogger? logger;
		public HttpServer(int port = 80, ILogger? logger = null)
		{
			listener = new HttpListener();
			this.port = port;
			this.logger = logger;
		}
		public Dictionary<string, IResponse> binds = new();
		public void BindPath(string webPath, IResponse response)
		{
			var path = $"http://+:{port}{webPath}{(webPath != "/" ? "/" : "")}";
			listener.Prefixes.Add(path);
			binds[webPath] = response;
			logger?.Info($"Binding path \"{path}\"");
		}

		public void Start()
		{
			listener.Start();
			enabled = true;
			while (enabled)
			{
				var context = listener.GetContext();

				var request = context.Request;
				var response = context.Response;
				context.Response.AppendHeader("Access-Control-Allow-Origin", "*");

				logger?.Info($"Getted response from {request.UserHostAddress}");

				if (request.RawUrl == null)
				{
					logger?.Warn("No RawUrl provided");
					continue;
				}
				var responsable = binds[request.RawUrl.Split("?")[0]];
				responsable.OnRequest(request, response);
				response.Close();
			}
			listener.Close();
		}

		public void Close() => enabled = false;
	}
}
