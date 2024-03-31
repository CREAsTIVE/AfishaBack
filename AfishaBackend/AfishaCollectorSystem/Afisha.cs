using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaBackend.AfishaCollectorSystem
{
	public class Afisha
	{
		public string Id { get; set; } = "";
		public string? Name { get; set; }
		public string? ImageUrl { get; set; }
		public string? Description { get; set; }
		public float? Rating { get; set; }
		public int? RatingAmount { get;set; }
		public string? Date { get; set; }
		public string? Time { get; set; }
		public string? Location { get; set; } 
		public int? TicketPrice { get; set; } // в рублях
	}
}
