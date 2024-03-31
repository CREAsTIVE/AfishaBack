using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaBackend.Databases
{
	public interface IDatabase<T>
	{
		public void Init(string filePath);
		public void Load();
		public void Save();
		T data { get; set; }
		public void Update();
	}
}
