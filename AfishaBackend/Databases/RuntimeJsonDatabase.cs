using AfishaBackend.Databases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaBackend.RuntimeDatabase
{
	public class RuntimeJsonDatabase<T> : IDatabase<T> where T : new()
	{
		public static RuntimeJsonDatabase<T> CreateDefault(string path)
		{
			var db = new RuntimeJsonDatabase<T>();
			db.Init(path);
			return db;
		}
		string filePath = "";
		public T data { get; set; } = new();

		public void Init(string filePath) { this.filePath = filePath; }

		public void Load()
		{
			if (!File.Exists(filePath))
			{
				data = new();
				return;
			}

			data = JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath)) ?? new();
		}

		public void Save()
		{
			Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new());
			File.WriteAllText(filePath, JsonConvert.SerializeObject(data));
		}

		public void Update() => Save();
	}
}
