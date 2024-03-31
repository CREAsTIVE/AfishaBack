using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHTTPServer.HTTPServerLogger
{
	public interface ILogger
	{
		public void Info(string message);
		public void Warn(string message);
		public void Error(string message);
	}

	public class ConsoleLogger : ILogger
	{
		public void Display(string message, ConsoleColor color)
		{
			var prevColor = Console.ForegroundColor;
			Console.ForegroundColor = color;

			Console.WriteLine(message);

			Console.ForegroundColor = prevColor;
		}
		public void Error(string message) => Display(message, ConsoleColor.Red);

		public void Info(string message) => Display(message, ConsoleColor.White);

		public void Warn(string message) => Display(message, ConsoleColor.Yellow);
	}
}
