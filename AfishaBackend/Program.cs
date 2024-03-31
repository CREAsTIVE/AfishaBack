using AfishaBackend;
using AfishaBackend.AfishaCollectorSystem;
using SimpleHTTPServer;
using SimpleHTTPServer.HTTPServerLogger;
using System.Diagnostics;
using System.Text.RegularExpressions;

var logger = new ConsoleLogger();
// Process.Start(new ProcessStartInfo("http://localhost:80") { UseShellExecute = true });
HttpServer server = new(8010, logger);

string currentDirectory = Directory.GetCurrentDirectory() ?? throw new Exception("Текущая дирректория неизвестна");

void parseFiles(string directory)
{
	foreach (var file in Directory.GetFiles(directory))
		server.BindPath("/" + Path.GetRelativePath(currentDirectory, file).Replace("\\", "/"), new PreloadedFileResponse(file));
	foreach (var newDirectory in Directory.GetDirectories(directory))
		parseFiles(newDirectory);
}

//parseFiles(currentDirectory);
//if (server.binds.TryGetValue("/index.html", out var val)) // replica for /index.html in a root
//	server.BindPath("/", val);

var collector = new Collector("databases/afishas.json");

collector.afishaGetters.Add(new YandexAfishaGetter(logger));

server.BindPath("/getAfishes", new ListResponse(collector));
server.BindPath("/getAfisha", new SingleResponse(collector));

server.Start();
