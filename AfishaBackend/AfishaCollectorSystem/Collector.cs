using AfishaBackend.Databases;
using AfishaBackend.RuntimeDatabase;
using SimpleHTTPServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AfishaBackend.AfishaCollectorSystem
{
	public interface AfishaGetter
	{
		public List<Afisha> Get();
	}
	public class Collector
	{
		public Collector(IDatabase<AfishesDatabase> database)
		{
			this.database = database;
			database.Load();
		}

		public Collector(string databasePath) : this(RuntimeJsonDatabase<AfishesDatabase>.CreateDefault(databasePath)) { }

		public List<AfishaGetter> afishaGetters = new();
		public class AfishesDatabase
		{
			public Dictionary<string, Afisha> Afishas { get; set; } = new();
			public DateTime? LastUpdate { get; set; }
		}

		IDatabase<AfishesDatabase> database;

		object locker = new();

		public void RequestNewest()
		{
			lock (locker)
			{
				if (database.data.LastUpdate is not null && (DateTime.Now - database.data.LastUpdate) < new TimeSpan(0, 30, 0))
					return;

				database.data.Afishas.Clear();

				foreach (var fetcher in afishaGetters)
				{
					var newList = fetcher.Get();
					foreach (var afisha in newList)
						database.data.Afishas[afisha.Id] = afisha;
				}
				database.data.LastUpdate = DateTime.Now;
				database.Update();
			}
		}

		public Afisha Get(string key)
		{
			RequestNewest();
			return database.data.Afishas[key];
		}

		public IEnumerable<Afisha> Get()
		{
			RequestNewest();
			return database.data.Afishas.Values;
        }
		
	}
}
