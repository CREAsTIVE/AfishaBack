using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHTTPServer
{
	public interface IResponse
	{
		public void OnRequest(HttpListenerRequest request, HttpListenerResponse response);
	}

	public abstract class StringResponse : IResponse
	{
		public void OnRequest(HttpListenerRequest request, HttpListenerResponse response)
		{
			using StreamReader inputStream = new StreamReader(request.InputStream);
			using StreamWriter outputStream = new StreamWriter(response.OutputStream);
			outputStream.Write(getResponse(inputStream.ReadToEnd()));
		}

		public abstract string getResponse(string request);
	}

	public abstract class JsonResponse<OUT> : IResponse
	{
		public void OnRequest(HttpListenerRequest request, HttpListenerResponse response)
		{
			using StreamReader inputStream = new StreamReader(request.InputStream);
			OUT outputObj = getResponse(request); // Получаем ответ на запрос C#
			string outputData = JsonConvert.SerializeObject(outputObj); // Преобразуем ответ из C# объекта в строку (json)
			response.OutputStream.Write(Encoding.UTF8.GetBytes(outputData)); // Отправляем данные
		}

		public abstract OUT getResponse(HttpListenerRequest request);
	}

    public abstract class JsonResponse<IN, OUT> : IResponse
    {
		public void OnRequest(HttpListenerRequest request, HttpListenerResponse response)
		{
			using StreamReader inputStream = new StreamReader(request.InputStream);
			using StreamWriter outputStream = new StreamWriter(response.OutputStream);
			string inputData = inputStream.ReadToEnd(); // Читаем данные
			IN? inputObj = JsonConvert.DeserializeObject<IN>(inputData); // Преобразуем строковые (json) данные в C# объект
			OUT outputObj = getResponse(inputObj); // Получаем ответ на запрос C#
			string outputData = JsonConvert.SerializeObject(outputObj); // Преобразуем ответ из C# объекта в строку (json)
			outputStream.Write(outputData); // Отправляем данные
		}

		public abstract OUT getResponse(IN? data);
    }

    public abstract class ByteResponse : IResponse
	{
		public void OnRequest(HttpListenerRequest request, HttpListenerResponse response)
		{
			using Stream outputStream = response.OutputStream;
			outputStream.Write(getResponse());
		}

		public abstract byte[] getResponse();
	}

	public class PreloadedFileResponse : ByteResponse
	{
		byte[] fileContent;
		public PreloadedFileResponse(string filename)
		{
			fileContent = File.ReadAllBytes(filename);
		}

		public override byte[] getResponse() => fileContent;
	}
}
